using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SHOW_TV_ANAHABER.Viz
{
    /// <summary>
    /// Show Haber sahnesinin yayın mantığı: alt bant (KJ), akan yazı, yer/canlı, logolar ve diğer tek director'lu grafikler.
    ///
    /// Komut dizileri Engine konsolunda denenerek oluşturuldu
    /// (bkz. docs/viz-komut-notlari.md). Sahnenin kendi içindeki küp (tobleron) animasyonu
    /// metin kopyalama ve döndürme işini kendisi yapar; burada sadece
    ///   - hangi grafiklerin ekranda olduğu,
    ///   - animasyon bitimine kadar "meşgul" süresi
    /// takip edilir.
    /// </summary>
    public sealed class ShowHaberController
    {
        public const string ScenePath = "SHOW_HABER/HABER2016/ShowHaber2016-2";

        private readonly VizEngineClient _viz;
        private readonly Dictionary<string, double> _endTimes = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _onAirGraphics = new HashSet<string>();

        public ShowHaberController(VizEngineClient viz)
        {
            if (viz == null) throw new ArgumentNullException(nameof(viz));
            _viz = viz;
        }

        // ---- Durum ------------------------------------------------------------

        /// <summary>Ekrandaki alt bant türü; yoksa null. (Akan yazı burada tutulmaz.)</summary>
        public KjType? CurrentKj { get; private set; }

        /// <summary>Ekrandaki başlık (bizim gönderdiğimiz).</summary>
        public string CurrentText { get; private set; }

        /// <summary>Akan yazı gönderildi ve OUT ile kesilmedi. Metin bitince sahne kendisi kaldırır, bunu bilemeyiz.</summary>
        public bool TickerSent { get; private set; }
        public string TickerText { get; private set; }

        public bool YerOnAir { get; private set; }
        public string YerText { get; private set; }

        public bool CanliOnAir { get; private set; }
        public bool CanliYerOnAir { get; private set; }
        public string CanliYerText { get; private set; }

        /// <summary>Ekranda olan basit grafiklerin (logo, sosyal medya, isim KJ) anahtarları.</summary>
        public IEnumerable<GraphicDefinition> OnAirGraphics
        {
            get { return _onAirGraphics.Select(GraphicCatalog.Get).Where(g => g != null); }
        }

        public bool IsGraphicOnAir(string key)
        {
            return _onAirGraphics.Contains(key);
        }

        public bool HaberLogoOnAir
        {
            get { return _onAirGraphics.Contains("haberlogo"); }
        }

        public bool SonDakikaLogoOnAir
        {
            get { return _onAirGraphics.Contains("sondakikalogo"); }
        }

        /// <summary>STAGE START bir kez gönderildi mi (sahne yüklendikten sonra).</summary>
        public bool StageStarted { get; private set; }

        /// <summary>Son animasyonun bitmesi beklenen an. Bu ana kadar yeni komut gönderilmemeli.</summary>
        public DateTime BusyUntil { get; private set; } = DateTime.MinValue;

        public bool IsBusy
        {
            get { return DateTime.Now < BusyUntil; }
        }

        /// <summary>
        /// KJ çıkarken logolar da indirilir. Az sonra OUT logunda doğrulandı (dir_out_haberlogo birlikte gidiyor);
        /// alt bant OUT logu henüz alınmadı, aynı kabul ediliyor.
        /// </summary>
        public bool OutAlsoHidesLogos { get; set; } = true;

        /// <summary>Bilgi mesajları (arayüzde loglamak için).</summary>
        public event Action<string> Info;

        // ---- Sahne --------------------------------------------------------------

        /// <summary>
        /// Her işlemden önce yapılan kontrol: ana katmanda bizim sahne var mı?
        /// Yoksa yükler ve stage'i başa sarar. rewindStage=true ise (bağlanma anı) sahne yüklü olsa da
        /// stage başa sarılır ("Tekrar Bağlan").
        /// Yüklediyse true döner.
        /// </summary>
        public bool EnsureSceneLoaded(bool rewindStage = false)
        {
            string objectId = _viz.Send(VizCommands.GetMainLayerObject()).Trim();      // "#76188" ya da boş
            string path = string.IsNullOrEmpty(objectId)
                ? string.Empty
                : _viz.Send(VizCommands.GetScenePath(objectId)).Trim();                // "SCENE*SHOW_HABER/..." ya da "SCENE*#76188"

            if (string.Equals(path, "SCENE*" + ScenePath, StringComparison.OrdinalIgnoreCase))
            {
                if (rewindStage)
                {
                    _viz.SendNoReply(VizCommands.StageShow(0.0));
                    ResetState();
                    Say("Sahne yüklü, stage başa sarıldı: " + ScenePath);
                }
                else
                {
                    Say("Sahne zaten yüklü: " + ScenePath);
                }
                return false;
            }

            Say("Sahne yüklü değil (" + (path.Length == 0 ? "ana katman boş" : path) + "), yükleniyor...");
            _viz.Send(VizCommands.PreloadScene(ScenePath), 60000);   // uzun sürer, cevabı bekle
            _viz.SendNoReply(VizCommands.LoadMainScene(ScenePath));
            _viz.SendNoReply(VizCommands.StageShow(0.0));
            ResetState();
            Say("Sahne yüklendi: " + ScenePath);
            return true;
        }

        /// <summary>Kapanış/boşaltma dizisi: STAGE STOP + boş SET_OBJECT.</summary>
        public void UnloadScene()
        {
            _viz.Send(VizCommands.StageStop());
            _viz.Send(VizCommands.ClearMainLayer());
            ResetState();
            Say("Sahne ana katmandan kaldırıldı.");
        }

        /// <summary>
        /// Araç çubuğu "Tümünü Al": animasyon oynatmadan arka, ana ve ön katmanda
        /// STAGE STOP + boş SET_OBJECT. Ana sahne de katmandan kalkar; sonraki VER sahneyi yeniden yükler.
        /// Meşgul kilidine bakmaz (acil durum düğmesi).
        /// </summary>
        public void ClearAllLayers()
        {
            CancelFlotClear();
            foreach (string layer in VizCommands.AllLayersReverse)
            {
                _viz.TrySend(VizCommands.LayerStageStop(layer));
                _viz.TrySend(VizCommands.ClearLayer(layer));
            }
            ResetState();
            Say("Tümünü Al: arka, ana ve ön katman boşaltıldı.");
        }

        // ---- Veritabanı tipleriyle köprü --------------------------------------------
        //
        // Ana sahnedeki alt bant, akan yazı, yer, canlı, logo ve isim KJ'leri tek yerden (bu sınıf) yönetilir.
        // Böylece sahne satırından (veritabanı tipi) verilen son dakika da haber logosu + kulakçıkla gelir ve
        // ekranda ne olduğu tek bir durumda tutulur. Tip, in director adından tanınır.

        private static KjDefinition FindKj(string inDirector)
        {
            return KjCatalog.All.FirstOrDefault(d => string.Equals(d.InDirector, inDirector, StringComparison.OrdinalIgnoreCase));
        }

        private static GraphicDefinition FindGraphic(string inDirector)
        {
            return GraphicCatalog.All.FirstOrDefault(g => string.Equals(g.InDirector, inDirector, StringComparison.OrdinalIgnoreCase));
        }

        private static bool Is(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Bu in director'lu tip bu sınıfın yönettiği ana sahne grafiklerinden mi?</summary>
        public static bool Owns(string inDirector)
        {
            if (string.IsNullOrEmpty(inDirector)) return false;
            return FindKj(inDirector) != null || FindGraphic(inDirector) != null
                || Is(inDirector, YerInDirector) || Is(inDirector, CanliInDirector);
        }

        /// <summary>In director adıyla tanınan tipi verir. text: tipin ilk alanının değeri.</summary>
        public void TakeByDirector(string inDirector, string text)
        {
            KjDefinition kj = FindKj(inDirector);
            if (kj != null) { TakeKj(kj.Type, text); return; }
            if (Is(inDirector, YerInDirector)) { TakeYer(text); return; }
            if (Is(inDirector, CanliInDirector)) { TakeCanli(); return; }
            GraphicDefinition g = FindGraphic(inDirector);
            if (g != null) { TakeGraphic(g, string.IsNullOrWhiteSpace(text) ? null : text); return; }
            throw new InvalidOperationException("Tanınmayan director: " + inDirector);
        }

        /// <summary>In director adıyla tanınan tipi ekrandan alır (AL).</summary>
        public void OutByDirector(string inDirector)
        {
            KjDefinition kj = FindKj(inDirector);
            if (kj != null)
            {
                if (kj.SelfTerminating) OutTicker();
                else if (CurrentKj == kj.Type) OutKj();
                else Say(kj.DisplayName + " ekranda değil" + (CurrentKj != null ? " (ekrandaki: " + KjCatalog.Get(CurrentKj.Value).DisplayName + ")." : "."));
                return;
            }
            if (Is(inDirector, YerInDirector)) { OutYer(); return; }
            if (Is(inDirector, CanliInDirector)) { OutCanli(); return; }
            GraphicDefinition g = FindGraphic(inDirector);
            if (g != null) { OutGraphic(g); return; }
            throw new InvalidOperationException("Tanınmayan director: " + inDirector);
        }

        /// <summary>
        /// Toplu VER: true iken meşgul kilidi kontrol edilmez ve meşgul süresi en uzun animasyona göre uzatılır.
        /// Birden çok satır (metin + isim + yer) aynı anda verilirken kullanılır.
        /// </summary>
        public bool BatchMode { get; set; }

        private void ResetState()
        {
            CurrentKj = null;
            CurrentText = null;
            TickerSent = false;
            TickerText = null;
            YerOnAir = false;
            YerText = null;
            CanliOnAir = false;
            CanliYerOnAir = false;
            CanliYerText = null;
            _onAirGraphics.Clear();
            CurrentFlot = null;
            StageStarted = false;
            _endTimes.Clear();
            BusyUntil = DateTime.MinValue;
        }

        // ---- KJ ---------------------------------------------------------------

        /// <summary>
        /// Alt bantı ya da akan yazıyı verir. Ekranda KJ yoksa giriş animasyonu,
        /// aynı tür ekrandaysa küp dönüşü (intra). Akan yazı her zaman giriş animasyonu.
        /// </summary>
        public void TakeKj(KjType type, string text)
        {
            EnsureNotBusy();

            KjDefinition def = KjCatalog.Get(type);
            text = (text ?? string.Empty).Trim();

            EnsureSceneLoaded();

            // 0) Farklı alt bant türü ekrandaysa (akan yazı hariç): önce eskinin out'u, bitmesi beklenir.
            //    Çakışma kuralı: eskinin out director'u, sonra yeninin in'i.
            bool replaced = false;
            if (!def.SelfTerminating && CurrentKj != null && CurrentKj != type)
            {
                KjDefinition old = KjCatalog.Get(CurrentKj.Value);
                double outDur = GetEndTime(old.OutDirector);
                Play(old.OutDirector);
                Say(old.DisplayName + " çıktı (" + def.DisplayName + " geliyor).");
                CurrentKj = null;
                CurrentText = null;
                if (outDur > 0) System.Threading.Thread.Sleep((int)(outDur * 1000));
                replaced = true;
            }

            // 1) Gizli metni yaz, grubu varsa aç.
            _viz.SendNoReply(VizCommands.SetText(VizCommands.ByName(def.TextContainer), text));
            if (def.TextGroup != null)
                _viz.SendNoReply(VizCommands.SetActive(VizCommands.ByName(def.TextGroup), true));

            double duration;

            if (def.SelfTerminating)
            {
                // 2) Akan yazı: her zaman giriş director'u. Metin bitince sahne kendisi kaldırır.
                duration = GetEndTime(def.InDirector);
                StartStageOnce();
                Play(def.InDirector);
                duration = Math.Max(duration, PlayExtras(def, switchEar: false));
                TickerSent = true;
                TickerText = text;
                Say(def.DisplayName + " akıyor, bitince kendi kalkar: " + text);
                SetBusy(duration);
                return;
            }

            if (CurrentKj == type && def.IntraDirector != null)
            {
                // 2b) Aynı tür ekranda: küp döner, metin değişir.
                duration = GetEndTime(def.IntraDirector);
                Play(def.IntraDirector);
                duration = Math.Max(duration, PlayExtras(def, switchEar: true));   // logolar kapatılmışsa geri gelsin
                Say(def.DisplayName + " başlık değişti: " + text);
            }
            else
            {
                // 2a) Ekranda alt bant yok (ya da az önce eski tür indirildi): in director + logolar.
                //     Tür değiştiyse PlayExtras eski kulakçığı indirir, yenisini getirir; haber logosu yerinde kalır.
                duration = GetEndTime(def.InDirector);
                StartStageOnce();
                Play(def.InDirector);
                duration = Math.Max(duration, PlayExtras(def, switchEar: true));
                Say(def.DisplayName + (replaced ? " girdi (tür değişti): " : " girdi: ") + text);
            }

            CurrentKj = type;
            CurrentText = text;
            SetBusy(duration);
        }

        /// <summary>Ekrandaki alt bantı kaldırır.</summary>
        public void OutKj()
        {
            if (CurrentKj == null)
            {
                Say("Ekranda alt bant yok.");
                return;
            }
            EnsureNotBusy();

            KjDefinition def = KjCatalog.Get(CurrentKj.Value);
            double duration = GetEndTime(def.OutDirector);
            Play(def.OutDirector);
            if (OutAlsoHidesLogos)
                duration = Math.Max(duration, HideKjLogos());

            Say(def.DisplayName + " çıktı.");
            CurrentKj = null;
            CurrentText = null;
            SetBusy(duration);
        }

        /// <summary>Akan yazıyı bitmesini beklemeden keser ("AL").</summary>
        public void OutTicker()
        {
            EnsureNotBusy();

            KjDefinition def = KjCatalog.Get(KjType.AzSonra);
            double duration = GetEndTime(def.OutDirector);
            Play(def.OutDirector);
            if (OutAlsoHidesLogos)
                duration = Math.Max(duration, HideKjLogos());

            TickerSent = false;
            TickerText = null;
            Say(def.DisplayName + " kesildi.");
            SetBusy(duration);
        }

        // ---- Yer KJ ---------------------------------------------------------------

        public const string YerTextContainer = "edt_txt_haber_yer";
        public const string YerInDirector = "dir_in_yerkj";
        public const string YerOutDirector = "dir_out_yerkj";

        /// <summary>Yer KJ'sini verir. Test Engine'de doğrulandı (2026-10-14): metin + STAGE START + dir_in_yerkj.</summary>
        public void TakeYer(string text)
        {
            EnsureNotBusy();

            text = (text ?? string.Empty).Trim();
            EnsureSceneLoaded();

            _viz.SendNoReply(VizCommands.SetText(VizCommands.ByName(YerTextContainer), text));
            double duration = GetEndTime(YerInDirector);
            StartStageOnce();
            Play(YerInDirector);

            YerOnAir = true;
            YerText = text;
            Say("Yer KJ girdi: " + text);
            SetBusy(duration);
        }

        public void OutYer()
        {
            EnsureNotBusy();

            double duration = GetEndTime(YerOutDirector);
            Play(YerOutDirector);
            YerOnAir = false;
            YerText = null;
            Say("Yer KJ çıktı.");
            SetBusy(duration);
        }

        // ---- Canlı / Canlı+Yer ------------------------------------------------------

        public const string CanliInDirector = "dir_in_canli";
        public const string CanliOutDirector = "dir_out_canli";
        public const string CanliYerTextContainer = "edt_txt_canli_yer";
        public const string CanliYerInDirector = "dir_in_canli_yerkj";
        public const string CanliYerOutDirector = "dir_out_canli_yerkj";

        /// <summary>CANLI etiketini verir. Test Engine'de doğrulandı.</summary>
        public void TakeCanli()
        {
            EnsureNotBusy();

            EnsureSceneLoaded();
            double duration = GetEndTime(CanliInDirector);
            StartStageOnce();
            Play(CanliInDirector);
            CanliOnAir = true;
            Say("Canlı girdi.");
            SetBusy(duration);
        }

        public void OutCanli()
        {
            EnsureNotBusy();

            double duration = GetEndTime(CanliOutDirector);
            Play(CanliOutDirector);
            CanliOnAir = false;
            Say("Canlı çıktı.");
            SetBusy(duration);
        }

        /// <summary>
        /// CANLI etiketi + yer metni birlikte. Engine denemesi (2026-10-14):
        /// metin SET, STAGE START, dir_in_canli, dir_in_canli_yerkj.
        /// </summary>
        public void TakeCanliYer(string text)
        {
            EnsureNotBusy();

            text = (text ?? string.Empty).Trim();
            EnsureSceneLoaded();

            _viz.SendNoReply(VizCommands.SetText(VizCommands.ByName(CanliYerTextContainer), text));
            double duration = Math.Max(GetEndTime(CanliInDirector), GetEndTime(CanliYerInDirector));
            StartStageOnce();
            Play(CanliInDirector);
            Play(CanliYerInDirector);

            CanliOnAir = true;
            CanliYerOnAir = true;
            CanliYerText = text;
            Say("Canlı/Yer girdi: " + text);
            SetBusy(duration);
        }

        /// <summary>VARSAYIM: çıkışta iki out director birlikte. Henüz denenmedi.</summary>
        public void OutCanliYer()
        {
            EnsureNotBusy();

            double duration = Math.Max(GetEndTime(CanliOutDirector), GetEndTime(CanliYerOutDirector));
            Play(CanliYerOutDirector);
            Play(CanliOutDirector);
            CanliOnAir = false;
            CanliYerOnAir = false;
            CanliYerText = null;
            Say("Canlı/Yer çıktı.");
            SetBusy(duration);
        }

        // ---- Basit grafikler: logolar, sosyal medya, isim KJ'leri -----------------------

        /// <summary>
        /// Tek director çiftiyle kontrol edilen grafik. Engine denemesi (2026-10-14):
        /// [metin SET] + STAGE START + dir_in_X SHOW 0.0 + CONTINUE. Zaten ekrandaysa yine in oynatılır
        /// (spiker adını değiştirmek için de böyle yapılır).
        /// </summary>
        public void TakeGraphic(GraphicDefinition g, string text = null)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            EnsureNotBusy();

            EnsureSceneLoaded();
            if (g.NeedsText)
                _viz.SendNoReply(VizCommands.SetText(VizCommands.ByName(g.TextContainer), (text ?? g.DefaultText ?? string.Empty).Trim()));

            double duration = GetEndTime(g.InDirector);
            StartStageOnce();
            Play(g.InDirector);
            _onAirGraphics.Add(g.Key);
            Say(g.DisplayName + " girdi" + (g.NeedsText ? ": " + text : "."));
            SetBusy(duration);
        }

        public void OutGraphic(GraphicDefinition g)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            EnsureNotBusy();

            double duration = GetEndTime(g.OutDirector);
            Play(g.OutDirector);
            _onAirGraphics.Remove(g.Key);
            Say(g.DisplayName + " çıktı.");
            SetBusy(duration);
        }

        // ---- Flot (arka katman) ------------------------------------------------------

        /// <summary>Flot sahnelerinin Graphic Hub klasörü.</summary>
        public const string FlotFolder = "SHOW_HABER/HABER2016/";
        public const string FlotInDirector = "dir_in_flot";
        public const string FlotOutDirector = "dir_out_flot";

        /// <summary>Arka katmanda açık flot sahnesinin adı (örn. FLOT-01); yoksa null.</summary>
        public string CurrentFlot { get; private set; }

        /// <summary>
        /// Flot sahnesini arka katmana yükleyip giriş animasyonunu oynatır. Engine denemesi (2026-10-16):
        /// BACK_LAYER GET_OBJECT, SCENE LOAD, BACK_LAYER SET_OBJECT, STAGE SHOW 0.0, END_TIME, STAGE START, dir_in_flot.
        /// Başka bir flot açıkken yeni flot doğrudan üstüne yüklenir (çıkış yapılmaz).
        /// </summary>
        public void TakeFlot(string sceneName)
        {
            sceneName = (sceneName ?? string.Empty).Trim();
            if (sceneName.Length == 0) throw new ArgumentException("Flot sahne adı boş.", nameof(sceneName));
            EnsureNotBusy();
            CancelFlotClear();   // önceki AL'ın boşaltması henüz çalışmadıysa iptal; yoksa yeni flotu siler

            string path = FlotFolder + sceneName;

            // Arka katmanda aynı sahne zaten varsa yeniden yüklenmez, sadece baştan oynatılır.
            string objectId = _viz.Send(VizCommands.GetLayerObject(VizCommands.BackLayer)).Trim();
            string current = objectId.Length == 0 ? string.Empty : _viz.Send(VizCommands.GetScenePath(objectId)).Trim();
            bool alreadyLoaded = string.Equals(current, "SCENE*" + path, StringComparison.OrdinalIgnoreCase);

            if (!alreadyLoaded)
            {
                _viz.Send(VizCommands.PreloadScene(path), 60000);                        // uzun sürer, cevabı bekle
                _viz.SendNoReply(VizCommands.LoadLayerScene(VizCommands.BackLayer, path));
            }
            _viz.SendNoReply(VizCommands.LayerStageShow(VizCommands.BackLayer, 0.0));

            double duration = ReadLayerEndTime(VizCommands.BackLayer, FlotInDirector);   // sahneye göre değişir, önbelleksiz
            _viz.SendNoReply(VizCommands.LayerStageStart(VizCommands.BackLayer));
            PlayOnLayer(VizCommands.BackLayer, FlotInDirector);

            CurrentFlot = sceneName;
            Say("Flot girdi: " + sceneName + (alreadyLoaded ? " (zaten yüklüydü)" : ""));
            SetBusy(duration);
        }

        /// <summary>
        /// Arka katmandaki flotu çıkarır: dir_out_flot; animasyon bitince arka katmanı boşaltır
        /// (STAGE STOP + boş SET_OBJECT). Engine denemesiyle doğrulandı (2026-10-16).
        /// </summary>
        public void OutFlot()
        {
            if (CurrentFlot == null)
            {
                Say("Arka katmanda flot yok.");
                return;
            }
            EnsureNotBusy();

            double duration = ReadLayerEndTime(VizCommands.BackLayer, FlotOutDirector);
            PlayOnLayer(VizCommands.BackLayer, FlotOutDirector);
            Say("Flot çıktı: " + CurrentFlot);
            CurrentFlot = null;
            SetBusy(duration);
            ScheduleFlotClear(duration);
        }

        private System.Threading.Timer _flotClearTimer;

        /// <summary>Çıkış animasyonu bitince arka katmanı boşaltır. Yeni flot gelirse iptal edilir.</summary>
        private void ScheduleFlotClear(double afterSeconds)
        {
            CancelFlotClear();
            int delayMs = (int)Math.Max(0, afterSeconds * 1000) + 100;
            _flotClearTimer = new System.Threading.Timer(_ =>
            {
                try
                {
                    _viz.Send(VizCommands.LayerStageStop(VizCommands.BackLayer));
                    _viz.Send(VizCommands.ClearLayer(VizCommands.BackLayer));
                    Say("Arka katman boşaltıldı.");
                }
                catch (Exception ex)
                {
                    Say("Arka katman boşaltılamadı: " + ex.Message);
                }
            }, null, delayMs, System.Threading.Timeout.Infinite);
        }

        private void CancelFlotClear()
        {
            if (_flotClearTimer == null) return;
            _flotClearTimer.Dispose();
            _flotClearTimer = null;
        }

        private void PlayOnLayer(string layer, string director)
        {
            _viz.SendNoReply(VizCommands.LayerDirectorShow(layer, director, 0.0));
            _viz.SendNoReply(VizCommands.LayerDirectorContinue(layer, director));
        }

        private double ReadLayerEndTime(string layer, string director)
        {
            string raw = _viz.TrySend(VizCommands.LayerDirectorGetEndTime(layer, director));
            double value;
            if (!double.TryParse((raw ?? "").Trim().TrimEnd('.'), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                value = 2.0;
            return value;
        }

        // ---- Tümünü al --------------------------------------------------------------

        /// <summary>
        /// "Tümünü Al" davranışı (2026-10-14): ekranda olduğunu bildiği her grafiğin
        /// END_TIME'ını okur, sonra hepsinin out director'unu art arda oynatır.
        /// </summary>
        public void OutAll()
        {
            EnsureNotBusy();

            var outs = new List<string>();

            if (CurrentKj != null) outs.Add(KjCatalog.Get(CurrentKj.Value).OutDirector);
            if (TickerSent) outs.Add(KjCatalog.Get(KjType.AzSonra).OutDirector);
            if (YerOnAir) outs.Add(YerOutDirector);
            if (CanliYerOnAir) outs.Add(CanliYerOutDirector);
            if (CanliOnAir) outs.Add(CanliOutDirector);
            foreach (GraphicDefinition g in OnAirGraphics.ToList()) outs.Add(g.OutDirector);

            if (outs.Count == 0 && CurrentFlot == null)
            {
                Say("Tümünü Al: ekranda bildiğimiz grafik yok.");
                return;
            }

            double duration = 0;
            foreach (string d in outs) duration = Math.Max(duration, GetEndTime(d));   // önce süreler
            foreach (string d in outs) Play(d);

            if (CurrentFlot != null)
            {
                // Flot arka katmanda; onun da çıkışı birlikte gider (VARSAYIM: flot açıkken denenmedi).
                double flotDuration = ReadLayerEndTime(VizCommands.BackLayer, FlotOutDirector);
                duration = Math.Max(duration, flotDuration);
                PlayOnLayer(VizCommands.BackLayer, FlotOutDirector);
                outs.Add(FlotOutDirector);
                CurrentFlot = null;
                ScheduleFlotClear(flotDuration);
            }

            CurrentKj = null;
            CurrentText = null;
            TickerSent = false;
            TickerText = null;
            YerOnAir = false;
            YerText = null;
            CanliOnAir = false;
            CanliYerOnAir = false;
            CanliYerText = null;
            _onAirGraphics.Clear();

            Say("Tümünü Al: " + outs.Count + " grafik çıktı.");
            SetBusy(duration);
        }

        // ---- Yardımcılar -------------------------------------------------------

        private void EnsureNotBusy()
        {
            if (IsBusy && !BatchMode)
                throw new InvalidOperationException("Önceki animasyon bitmedi, bekleyin.");
        }

        /// <summary>Oynatma kalıbı: başa sar, devam et.</summary>
        private void Play(string director)
        {
            _viz.SendNoReply(VizCommands.DirectorShow(director, 0.0));
            _viz.SendNoReply(VizCommands.DirectorContinue(director));
        }

        private void StartStageOnce()
        {
            if (StageStarted) return;
            _viz.SendNoReply(VizCommands.StageStart());
            StageStarted = true;
        }

        /// <summary>
        /// KJ ile gelen logolar: haber logosu her türle gelir (zaten açıksa dokunulmaz).
        /// switchEar=true ise türün kulakçığı da yönetilir: başka türün kulakçığı açıksa iner, bu türünki yoksa gelir.
        /// En uzun animasyon süresini döndürür.
        /// </summary>
        private double PlayExtras(KjDefinition def, bool switchEar)
        {
            double duration = GraphicIn(KjCatalog.HaberLogoKey);

            if (!switchEar) return duration;

            foreach (string key in KjCatalog.EarLogoKeys)
            {
                if (key == def.EarLogoKey) continue;
                duration = Math.Max(duration, GraphicOut(key));
            }
            if (def.EarLogoKey != null)
                duration = Math.Max(duration, GraphicIn(def.EarLogoKey));

            return duration;
        }

        /// <summary>KJ ile gelen logoları (haber logosu + tüm kulakçıklar) indirir; en uzun animasyon süresini döndürür.</summary>
        private double HideKjLogos()
        {
            double duration = GraphicOut(KjCatalog.HaberLogoKey);
            foreach (string key in KjCatalog.EarLogoKeys)
                duration = Math.Max(duration, GraphicOut(key));
            return duration;
        }

        /// <summary>Katalogdaki grafik ekranda değilse getirir; süresini döndürür (zaten açıksa 0).</summary>
        private double GraphicIn(string key)
        {
            if (_onAirGraphics.Contains(key)) return 0;
            GraphicDefinition g = GraphicCatalog.Get(key);
            if (g == null) return 0;
            double duration = GetEndTime(g.InDirector);
            Play(g.InDirector);
            _onAirGraphics.Add(key);
            return duration;
        }

        /// <summary>Katalogdaki grafik ekrandaysa indirir; süresini döndürür (açık değilse 0).</summary>
        private double GraphicOut(string key)
        {
            if (!_onAirGraphics.Contains(key)) return 0;
            GraphicDefinition g = GraphicCatalog.Get(key);
            if (g == null) return 0;
            double duration = GetEndTime(g.OutDirector);
            Play(g.OutDirector);
            _onAirGraphics.Remove(key);
            return duration;
        }

        /// <summary>Director süresini Engine'den okur ve önbelleğe alır.</summary>
        private double GetEndTime(string director)
        {
            double value;
            if (_endTimes.TryGetValue(director, out value))
                return value;

            string raw = _viz.TrySend(VizCommands.DirectorGetEndTime(director));
            if (!double.TryParse((raw ?? "").Trim().TrimEnd('.'), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                value = 2.0; // okunamazsa güvenli tarafta kal

            _endTimes[director] = value;
            return value;
        }

        private void SetBusy(double seconds)
        {
            DateTime until = DateTime.Now.AddSeconds(seconds);
            if (BatchMode && BusyUntil > until) return;   // toplu verişte en uzun animasyon kazanır
            BusyUntil = until;
        }

        private void Say(string message)
        {
            Info?.Invoke(message);
        }
    }
}
