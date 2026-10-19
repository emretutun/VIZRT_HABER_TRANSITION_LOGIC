using System;
using System.Collections.Generic;
using System.Globalization;
using SHOW_TV_ANAHABER.Data;

namespace SHOW_TV_ANAHABER.Viz
{
    /// <summary>
    /// Veritabanı tanımlı sahne tiplerini (SceneType) genel kurallarla oynatır.
    /// Bir Engine + bir katman için çalışır (Kelebekler: her panel kendi Engine'ine, tipin target_layer'ına).
    ///
    /// Kurallar:
    ///  - Katmanda doğru sahne yoksa LOAD + SET_OBJECT + STAGE SHOW 0.0; scene_reset_enabled ise STAGE START.
    ///  - Alan değerleri: TREE*$element_name*GEOM*TEXT SET değer.
    ///  - Aynı tip zaten ekrandaysa ve intra director varsa: intra oynat, intra_director_delay bekle, değerleri yaz.
    ///  - Ekrandaki tip yeni tiple çakışıyorsa (tipin çakışan tip listesi) ya da aynı katmanda başka sahne dosyası ise:
    ///    eskinin out'u, süresi beklenir, yeninin in'i. Çakışmayanlar yan yana kalır (KJ Metin + KJ Yer + KJ İsim).
    ///  - Her director'ın END_TIME'ı okunur; en uzunu kadar meşgul kalınır.
    /// </summary>
    public sealed class GenericScenePublisher
    {
        private readonly VizEngineClient _viz;

        public GenericScenePublisher(VizEngineClient viz)
        {
            if (viz == null) throw new ArgumentNullException(nameof(viz));
            _viz = viz;
        }

        /// <summary>
        /// Ekranda olan kayıtlı sahneler, gui_id başına bir tane. Çakışmayan tipler yan yana durur
        /// (örneğin KJ Metin + KJ Yer + KJ İsim aynı anda ekranda olabilir).
        /// </summary>
        private readonly Dictionary<int, SavedSceneItem> _onAir = new Dictionary<int, SavedSceneItem>();

        /// <summary>En son verilen ve hâlâ ekranda olan sahne; yoksa null.</summary>
        public SavedSceneItem Current { get; private set; }

        /// <summary>Ekrandaki tüm sahneler.</summary>
        public IEnumerable<SavedSceneItem> OnAir { get { return _onAir.Values; } }

        public bool IsOnAir(SceneType type)
        {
            return type != null && _onAir.ContainsKey(type.GuiId);
        }

        public DateTime BusyUntil { get; private set; } = DateTime.MinValue;
        public bool IsBusy { get { return DateTime.Now < BusyUntil; } }

        /// <summary>
        /// Yeni sahne verilirken ekrandaki sahnelerden çakışanlar out director'u ile kaldırılsın mı.
        /// Çakışma: sahne tipinin çakışan tip listesi, ya da aynı katmanda başka bir sahne dosyası
        /// (bir katman tek sahne tutar; kelebek pencereleri böyle değişir).
        /// </summary>
        public bool UnpublishPrevious { get; set; } = true;

        private static bool MustRemove(SceneType onAir, SceneType incoming)
        {
            if (onAir == null || incoming == null || onAir.GuiId == incoming.GuiId) return false;
            if (onAir.ConflictsWith(incoming)) return true;
            return onAir.TargetLayer == incoming.TargetLayer &&
                   !string.Equals(onAir.SceneFullPath, incoming.SceneFullPath, StringComparison.OrdinalIgnoreCase);
        }

        public event Action<string> Info;

        private static string LayerName(int targetLayer)
        {
            switch (targetLayer)
            {
                case 0: return "RENDERER*FRONT_LAYER";
                case 2: return VizCommands.BackLayer;
                default: return VizCommands.MainLayer;
            }
        }

        /// <summary>Tipin sahnesini belleğe alır ("Tümünü Hazırla" / ön yükleme).</summary>
        public void Preload(SceneType type)
        {
            if (type == null || string.IsNullOrEmpty(type.SceneFullPath)) return;
            _viz.Send(VizCommands.PreloadScene(type.SceneFullPath), 60000);
            Say("Belleğe alındı: " + type.SceneFullPath);
        }

        /// <summary>Kayıtlı sahneyi verir (VER). values null ise item.Values kullanılır.</summary>
        public void Publish(SavedSceneItem item, IDictionary<int, string> values = null)
        {
            if (item == null || item.Type == null) throw new ArgumentException("Sahne tipi tanımsız.");
            if (IsBusy && !BatchMode) throw new InvalidOperationException("Önceki animasyon bitmedi, bekleyin.");

            SceneType type = item.Type;
            string layer = LayerName(type.TargetLayer);
            double duration = 0;

            // 1) Ekranda yeni tiple çakışan sahneler varsa kaldır (out director'lar birlikte, en uzunu beklenir).
            //    Çakışmayanlar (KJ Metin açıkken KJ Yer, KJ İsim, logolar) ekranda kalır.
            if (UnpublishPrevious)
            {
                var toRemove = new List<SavedSceneItem>();
                foreach (SavedSceneItem s in _onAir.Values)
                    if (MustRemove(s.Type, type)) toRemove.Add(s);

                double outDur = 0;
                foreach (SavedSceneItem s in toRemove)
                {
                    outDur = Math.Max(outDur, PlayOut(s));
                    Forget(s.Type.GuiId);
                    Say(s.Type.GuiName + " kaldırıldı (" + type.GuiName + " ile çakışıyor).");
                }
                if (outDur > 0) System.Threading.Thread.Sleep((int)(outDur * 1000));
            }

            // 2) Doğru sahne katmanda mı?
            bool sameTypeOnAir = _onAir.ContainsKey(type.GuiId);
            bool needsLoad = !sameTypeOnAir && LayerNeedsScene(layer, type.SceneFullPath);
            if (needsLoad)
            {
                // Katmana yeni sahne gelince o katmandaki eski kayıtlar geçersizleşir.
                foreach (int id in new List<int>(_onAir.Keys))
                    if (_onAir[id].Type.TargetLayer == type.TargetLayer) Forget(id);

                _viz.Send(VizCommands.PreloadScene(type.SceneFullPath), 60000);
                _viz.SendNoReply(VizCommands.LoadLayerScene(layer, type.SceneFullPath));
                _viz.SendNoReply(VizCommands.LayerStageShow(layer, 0.0));
            }

            // 3) Aynı tip ekranda ve intra varsa: intra + gecikme + değerler.
            if (sameTypeOnAir && !string.IsNullOrEmpty(type.IntraDirector))
            {
                duration = EndTime(layer, type.IntraDirector);
                Play(layer, type.IntraDirector);
                if (type.IntraDirectorDelay > 0)
                    System.Threading.Thread.Sleep((int)(type.IntraDirectorDelay * 1000));
                SetValues(layer, item, values);
                Say(type.GuiName + " güncellendi (intra): " + item.Description);
            }
            else
            {
                // 4) Değerleri yaz, stage'i başlat, in director (varsa).
                SetValues(layer, item, values);
                if (string.IsNullOrEmpty(type.InDirector))
                {
                    // In director yok (kelebek pencereleri): sadece STAGE SHOW 0.0 + STAGE START gönderilir,
                    // sahnenin kendi stage'i girişi oynatıp durakta bekliyor.
                    if (!needsLoad) _viz.SendNoReply(VizCommands.LayerStageShow(layer, 0.0));
                    _viz.SendNoReply(VizCommands.LayerStageStart(layer));
                }
                else
                {
                    if (type.SceneResetEnabled && needsLoad)
                        _viz.SendNoReply(VizCommands.LayerStageStart(layer));
                    duration = EndTime(layer, type.InDirector);
                    Play(layer, type.InDirector);
                }
                Say(type.GuiName + " verildi: " + item.Description);
            }

            _onAir[type.GuiId] = item;
            Current = item;
            DateTime until = DateTime.Now.AddSeconds(duration);
            if (!BatchMode || until > BusyUntil) BusyUntil = until;   // toplu verişte en uzun animasyon kazanır
        }

        /// <summary>
        /// Toplu VER: true iken Publish meşgul kilidine bakmaz ve meşgul süresini yalnızca uzatır.
        /// Birden çok satır (metin + isim + yer) aynı anda verilirken kullanılır.
        /// </summary>
        public bool BatchMode { get; set; }

        /// <summary>Ekrandaki sahnenin alanlarını yeniden yazar, animasyon oynatmaz (GÜNCELLE).</summary>
        public void Update(SavedSceneItem item, IDictionary<int, string> values = null)
        {
            if (item == null || item.Type == null) throw new ArgumentException("Sahne tipi tanımsız.");
            SetValues(LayerName(item.Type.TargetLayer), item, values);
            Say(item.Type.GuiName + " güncellendi: " + item.Description);
        }

        /// <summary>
        /// Sahneyi kaldırır (AL). type verilirse o tip, verilmezse en son verilen sahne.
        /// Böylece KJ Metin + KJ Yer ekrandayken KJ Yer satırında AL denince yalnızca yer iner.
        /// </summary>
        public void Unpublish(SceneType type = null)
        {
            SavedSceneItem target = null;
            if (type != null) _onAir.TryGetValue(type.GuiId, out target);
            else target = Current;

            if (target == null)
            {
                Say(type != null ? type.GuiName + " ekranda değil." : "Ekranda sahne yok.");
                return;
            }
            if (IsBusy) throw new InvalidOperationException("Önceki animasyon bitmedi, bekleyin.");
            double d = PlayOut(target);
            Say(target.Type.GuiName + " kaldırıldı.");
            Forget(target.Type.GuiId);
            BusyUntil = DateTime.Now.AddSeconds(d);
        }

        /// <summary>Ekrandaki tüm sahneleri birlikte kaldırır (Tümünü Al). Meşgul kilidine bakmaz.</summary>
        public void UnpublishAll()
        {
            double d = 0;
            foreach (SavedSceneItem s in new List<SavedSceneItem>(_onAir.Values))
            {
                d = Math.Max(d, PlayOut(s));
                Say(s.Type.GuiName + " kaldırıldı.");
            }
            _onAir.Clear();
            Current = null;
            BusyUntil = DateTime.Now.AddSeconds(d);
        }

        /// <summary>Kayıtları siler, Engine'e komut göndermez (sahne elle kaldırıldığında / katman boşaltıldığında).</summary>
        public void Reset()
        {
            _onAir.Clear();
            Current = null;
            BusyUntil = DateTime.MinValue;
        }

        // ---- yardımcılar ----------------------------------------------------------------

        private void Forget(int guiId)
        {
            _onAir.Remove(guiId);
            if (Current != null && Current.Type != null && Current.Type.GuiId == guiId)
            {
                // "En son verilen" bir öncekine düşer (ekranda kalanlardan herhangi biri).
                Current = null;
                foreach (SavedSceneItem s in _onAir.Values) Current = s;
            }
        }

        /// <summary>
        /// Çıkış: ayrı bir out director varsa başa sarıp oynatır. Out director boşsa ya da in director ile aynıysa
        /// sahne "tek director: giriş → durak → çıkış" şeklinde kurulmuş demektir; o zaman başa SARMADAN sadece
        /// CONTINUE gönderilir (durak noktasından çıkış animasyonu devam eder).
        /// </summary>
        private double PlayOut(SavedSceneItem item)
        {
            if (item.Type == null) return 0;
            string layer = LayerName(item.Type.TargetLayer);
            string outDir = item.Type.OutDirector;

            if (string.IsNullOrEmpty(outDir))
            {
                // Out director yok (kelebek pencereleri): katman doğrudan boşaltılır: STAGE STOP + boş SET_OBJECT.
                _viz.Send(VizCommands.LayerStageStop(layer));
                _viz.Send(VizCommands.ClearLayer(layer));
                Say("Çıkış: katman boşaltıldı (" + layer + ").");
                return 0;
            }

            if (string.Equals(outDir, item.Type.InDirector, StringComparison.OrdinalIgnoreCase))
            {
                double total = EndTime(layer, item.Type.InDirector);
                _viz.SendNoReply(VizCommands.LayerDirectorContinue(layer, item.Type.InDirector));
                Say("Çıkış: '" + item.Type.InDirector + "' duraktan devam ettirildi.");
                return total;
            }

            double d = EndTime(layer, outDir);
            Play(layer, outDir);
            return d;
        }

        /// <summary>Elle director çalıştırma (director düğmeleri). rewind=true: SHOW 0.0 + CONTINUE; false: sadece CONTINUE.</summary>
        public void RunDirector(SceneType type, string director, bool rewind)
        {
            if (type == null || string.IsNullOrEmpty(director)) return;
            string layer = LayerName(type.TargetLayer);
            if (rewind) Play(layer, director);
            else _viz.SendNoReply(VizCommands.LayerDirectorContinue(layer, director));
            Say("Director " + director + (rewind ? " başlatıldı." : " devam ettirildi."));
        }

        /// <summary>Sahnenin director adlarını Engine'den okur (sahne bellekte olmalı; değilse boş döner).</summary>
        public IList<string> GetDirectorNames(SceneType type)
        {
            var list = new List<string>();
            if (type == null || string.IsNullOrEmpty(type.SceneFullPath)) return list;
            string raw = _viz.TrySend("SCENE*" + type.SceneFullPath + "*STAGE*DIRECTOR GET");
            if (string.IsNullOrWhiteSpace(raw)) return list;
            foreach (string n in raw.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (!list.Contains(n)) list.Add(n);
            return list;
        }

        private void SetValues(string layer, SavedSceneItem item, IDictionary<int, string> values)
        {
            foreach (SceneTypeElement el in item.Type.Elements)
            {
                if (string.IsNullOrEmpty(el.ElementName)) continue;
                string v = null;
                if (values != null && values.ContainsKey(el.GuiElementId)) v = values[el.GuiElementId];
                else v = item.GetValue(el);
                if (v == null) continue;
                _viz.SendNoReply(layer + "*TREE*$" + el.ElementName + "*GEOM*TEXT SET " + v.Replace("\0", "").Replace("\r\n", "\\n").Replace("\n", "\\n"));
            }
        }

        private bool LayerNeedsScene(string layer, string scenePath)
        {
            string objectId = (_viz.TrySend(VizCommands.GetLayerObject(layer)) ?? "").Trim();
            if (objectId.Length == 0) return true;
            string current = (_viz.TrySend(VizCommands.GetScenePath(objectId)) ?? "").Trim();
            return !string.Equals(current, "SCENE*" + scenePath, StringComparison.OrdinalIgnoreCase);
        }

        private void Play(string layer, string director)
        {
            _viz.SendNoReply(VizCommands.LayerDirectorShow(layer, director, 0.0));
            _viz.SendNoReply(VizCommands.LayerDirectorContinue(layer, director));
        }

        private double EndTime(string layer, string director)
        {
            string raw = _viz.TrySend(VizCommands.LayerDirectorGetEndTime(layer, director));
            double v;
            if (!double.TryParse((raw ?? "").Trim().TrimEnd('.'), NumberStyles.Float, CultureInfo.InvariantCulture, out v)) v = 2.0;
            return v;
        }

        private void Say(string m)
        {
            Info?.Invoke(m);
        }
    }
}
