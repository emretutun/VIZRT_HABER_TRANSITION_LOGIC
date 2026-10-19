using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SHOW_TV_ANAHABER.Config;
using SHOW_TV_ANAHABER.Data;
using SHOW_TV_ANAHABER.Ui;
using SHOW_TV_ANAHABER.Viz;

namespace SHOW_TV_ANAHABER
{
    /// <summary>
    /// Show TV Reji (ANA HABER) – yeni sürüm.
    /// Ekranın yerleşimi ve davranışı:
    ///  - Haberler: tarih → akış → haber → habere bağlı sahne satırları (veritabanından, salt okunur).
    ///  - Sahne satırları iki yolla oynar: veritabanı tanımlı tip (GenericScenePublisher) ya da elle seçilen
    ///    KJ tipi (ShowHaberController, logla doğrulanmış sabit diziler).
    ///  - Listeler: playlist'ler; Kelebekler: iki Viz hedefi.
    /// Veritabanı bağlantısı boşsa listeler boş kalır, hiçbir şey yazılmaz.
    /// </summary>
    public partial class Form1 : Form
    {
        private VizEngineClient _viz;
        private ShowHaberController _show;
        private GenericScenePublisher _mainPublisher;

        private AppSettings _settings;
        private IShowRepository _repo = new NullShowRepository();
        private KelebekPanel _kelebek1;
        private KelebekPanel _kelebek2;
        private KelebekPanel _listePanel;
        private ComboBox _cmbPlaylist;

        private List<NewsStory> _stories = new List<NewsStory>();
        private List<SceneType> _sceneTypes = new List<SceneType>();

        /// <summary>Program modları: görünen ad → kontrolcü adı.</summary>
        private static readonly KeyValuePair<string, string>[] ProgramModes =
        {
            new KeyValuePair<string, string>("Show Ana Haber", "ShowTVRejiAnaHaber"),
            new KeyValuePair<string, string>("Gece Hattı", "ShowTVRejiGeceHatti"),
            new KeyValuePair<string, string>("15 Temmuz", "ShowTVReji15Temmuz"),
            new KeyValuePair<string, string>("Seçim 2019", "ShowTVRejiYerelSecim2019"),
        };

        public Form1()
        {
            InitializeComponent();
            tscProgramMode.Items.Clear();
            foreach (var m in ProgramModes) tscProgramMode.Items.Add(m.Key);
            tscProgramMode.SelectedIndexChanged += tscProgramMode_SelectedIndexChanged;
            cmbLogoGrubu.SelectedIndex = 0;
            FillSceneTypeTree();
            FillGraphicLists();
            BuildKelebekTab();
            BuildListelerTab();
            ApplySettings(AppSettings.Load());
            UpdateConnectionUi();
            UpdateKjUi();
        }

        // =====================================================================
        //  Ayarlar ve veri katmanı
        // =====================================================================

        /// <summary>Ayarları uygular: Engine adresi, veritabanı (varsa), kelebek ve liste panelleri, listeler.</summary>
        private void ApplySettings(AppSettings settings)
        {
            _settings = settings;
            txtHost.Text = settings.EngineHost;
            numPort.Value = settings.EnginePort;

            int modeIndex = Array.FindIndex(ProgramModes, m => m.Value == settings.ControllerName);
            tscProgramMode.SelectedIndex = modeIndex >= 0 ? modeIndex : 0;

            // Veri kaynağı: JSON klasörü (varsayılan) > MySQL (bağlantı dizesi varsa) > boş.
            Log("** Ayar dosyası: " + AppSettings.FilePath);
            if (AppSettings.LastLoadError != null)
                Log("** AYAR DOSYASI OKUNAMADI (varsayılanlar kullanılıyor): " + AppSettings.LastLoadError);
            if (settings.UseJsonData)
            {
                _repo = new JsonShowRepository(settings.DataFolderPath);
                Log("** Veri kaynağı: JSON klasörü " + settings.DataFolderPath);
            }
            else if (settings.HasVizrtDb)
            {
                _repo = new MySqlShowRepository(settings.VizrtDbConnectionString, settings.NewsDbConnectionString);
                Log("** Veri kaynağı: MySQL (salt okunur).");
            }
            else
            {
                _repo = new NullShowRepository();
                Log("** Veri kaynağı yok; listeler boş.");
            }

            _kelebek1.Configure("KELEBEK LİSTESİ – Viz #1", _repo, settings.KelebekPlaylist1, settings.KelebekGuiDirId, settings.Kelebek1Host, settings.Kelebek1Port);
            _kelebek2.Configure("KELEBEK LİSTESİ – Viz #2", _repo, settings.KelebekPlaylist2, settings.KelebekGuiDirId, settings.Kelebek2Host, settings.Kelebek2Port);
            _listePanel.Configure("LİSTE", _repo, 0, 0, settings.EngineHost, settings.EnginePort);

            LoadSceneTypes();
            LoadControllerLists();
            LoadPlaylists();
            LoadRundowns();
        }

        /// <summary>Kelebekler sekmesi: iki hedef yan yana.</summary>
        private void BuildKelebekTab()
        {
            var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Padding = new Padding(6) };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _kelebek1 = new KelebekPanel { Dock = DockStyle.Fill, Margin = new Padding(4) };
            _kelebek2 = new KelebekPanel { Dock = DockStyle.Fill, Margin = new Padding(4) };
            _kelebek1.Log += OnShowInfo;
            _kelebek2.Log += OnShowInfo;
            table.Controls.Add(_kelebek1, 0, 0);
            table.Controls.Add(_kelebek2, 1, 0);
            tabKelebekler.Controls.Add(table);
        }

        /// <summary>Listeler sekmesi: playlist seçimi + o playlist'in sahneleri (ana reji Engine'ine gider).</summary>
        private void BuildListelerTab()
        {
            var top = new Panel { Dock = DockStyle.Top, Height = 44, Padding = new Padding(8, 8, 8, 4) };
            var lbl = new Label { Text = "Playlist", AutoSize = true, Location = new Point(10, 14), ForeColor = Color.DimGray };
            _cmbPlaylist = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(80, 10), Width = 400 };
            _cmbPlaylist.SelectedIndexChanged += (s, e) =>
            {
                var pl = _cmbPlaylist.SelectedItem as Playlist;
                if (pl != null) _listePanel.SetPlaylist(pl.GroupId, "LİSTE – " + pl.Name);
            };
            var btnRefresh = new Button { Text = "Yenile", Location = new Point(490, 8), Size = new Size(90, 29) };
            btnRefresh.Click += (s, e) => LoadPlaylists();
            top.Controls.Add(lbl);
            top.Controls.Add(_cmbPlaylist);
            top.Controls.Add(btnRefresh);

            _listePanel = new KelebekPanel { Dock = DockStyle.Fill };
            _listePanel.Log += OnShowInfo;

            tabListeler.Controls.Add(_listePanel);
            tabListeler.Controls.Add(top);
        }

        private void LoadPlaylists()
        {
            try
            {
                var lists = _repo.GetPlaylists().ToList();
                _cmbPlaylist.DataSource = null;
                _cmbPlaylist.DataSource = lists;
                _cmbPlaylist.DisplayMember = "Name";
                if (lists.Count > 0) Log("** " + lists.Count + " playlist yüklendi.");
            }
            catch (Exception ex)
            {
                Log("** Playlist listesi okunamadı: " + ex.Message);
            }
        }

        /// <summary>Sahne tipleri → sağdaki liste. Veritabanı yoksa tasarımcıdaki örnek satırlar kalır.</summary>
        private void LoadSceneTypes()
        {
            try
            {
                _sceneTypes = _repo.GetSceneTypes().ToList();
            }
            catch (Exception ex)
            {
                Log("** Sahne tipleri okunamadı: " + ex.Message);
                _sceneTypes = new List<SceneType>();
            }
            if (_sceneTypes.Count == 0) return;

            lstSahneTipleri.Items.Clear();
            foreach (SceneType t in _sceneTypes.OrderBy(t => t.GuiName))
                lstSahneTipleri.Items.Add(t);
            Log("** " + _sceneTypes.Count + " sahne tipi yüklendi.");
        }

        /// <summary>Kontrolcü listeleri: HaberLogo* satırları sağ üst logo listesine eklenir.</summary>
        private void LoadControllerLists()
        {
            if (!_repo.IsAvailable) return;
            try
            {
                var items = _repo.GetControllerItems(_settings.ControllerName);
                var logos = items.Where(i => (i.Identifier ?? "").StartsWith("HaberLogo", StringComparison.OrdinalIgnoreCase))
                                 .OrderBy(i => i.Description).ToList();
                if (logos.Count == 0) return;

                lstLogoSahneler.Items.Clear();
                foreach (ControllerItem ci in logos)
                {
                    SceneType t = _sceneTypes.FirstOrDefault(x => x.GuiId == ci.GuiId);
                    if (t != null) lstLogoSahneler.Items.Add(new SceneTypeListItem(t, ci.Description));
                }
                Log("** Kontrolcü '" + _settings.ControllerName + "': " + logos.Count + " logo tanımı yüklendi.");
            }
            catch (Exception ex)
            {
                Log("** Kontrolcü listesi okunamadı: " + ex.Message);
            }
        }

        /// <summary>Listelerde veritabanı tipini açıklamasıyla göstermek için küçük sarmalayıcı.</summary>
        private sealed class SceneTypeListItem
        {
            public SceneType Type { get; }
            public string Text { get; }
            public SceneTypeListItem(SceneType type, string text) { Type = type; Text = text; }
            public override string ToString() { return Text; }
        }

        private void tscProgramMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_settings == null || tscProgramMode.SelectedIndex < 0) return;
            string name = ProgramModes[tscProgramMode.SelectedIndex].Value;
            if (name == _settings.ControllerName) return;
            _settings.ControllerName = name;
            try { _settings.Save(); } catch { }
            Log("** Program modu: " + tscProgramMode.Text + " (" + name + ")");
            LoadControllerLists();
        }

        // =====================================================================
        //  Bağlantı
        // =====================================================================

        private bool IsConnected
        {
            get { return _viz != null && _viz.IsConnected && _show != null; }
        }

        private void Connect()
        {
            Disconnect();
            _viz = new VizEngineClient(txtHost.Text.Trim(), (int)numPort.Value);
            _viz.CommandLogged += OnVizCommandLogged;
            _viz.Connect();
            Log("Bağlandı: " + _viz.Host + ":" + _viz.Port);

            _show = new ShowHaberController(_viz);
            _show.Info += OnShowInfo;
            _mainPublisher = new GenericScenePublisher(_viz) { UnpublishPrevious = true };
            _mainPublisher.Info += OnShowInfo;

            _viz.TrySend("VERSION");
            _show.EnsureSceneLoaded(rewindStage: true);   // "Tekrar Bağlan": stage başa sarılır
            timerKeepAlive.Enabled = true;
            RefreshVizMemory();
        }

        private void Disconnect()
        {
            timerKeepAlive.Enabled = false;
            if (_viz != null) _viz.Dispose();
            _viz = null;
            _show = null;
            _mainPublisher = null;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsConnected)
                {
                    Disconnect();
                    Log("Bağlantı kapatıldı.");
                }
                else
                {
                    Connect();
                }
            }
            catch (Exception ex)
            {
                Log("HATA: " + ex.Message);
                Disconnect();
            }
            UpdateConnectionUi();
            UpdateKjUi();
        }

        private void tsbReconnect_Click(object sender, EventArgs e)
        {
            Guard(Connect);
            UpdateConnectionUi();
            UpdateKjUi();
        }

        private void tsbLoadScenes_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.EnsureSceneLoaded());
        }

        private void tsbClearMemory_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            if (MessageBox.Show("Sahne ana katmandan kaldırılacak (STAGE STOP + SET_OBJECT). Devam?",
                                "Belleği Temizle", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            Guard(() =>
            {
                _show.UnloadScene();
                _mainPublisher.Reset();   // katman boşaldı, ekrandaki sahne kayıtları geçersiz
            });
        }

        /// <summary>
        /// Araç çubuğu "Tümünü Al": onay sorulur (varsayılan Hayır), sonra animasyon
        /// oynatmadan arka, ana ve ön katman boşaltılır. Bağlı kelebek hedefleri de boşaltılır (eskisi kontrolcünün
        /// bütün makinelerinde yapıyor). Animasyonlu çıkış için Ctrl+E (Tümünü Animasyonla Al).
        /// </summary>
        private void tsbTakeAllOut_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            if (MessageBox.Show(this,
                    "Yayında olan TÜM sahneler animasyonsuz alınacak (arka, ana ve ön katman boşaltılır).\n\nEmin misiniz?",
                    "Tümünü Al", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            Log("** Tüm sahneler alınıyor...");
            Guard(() =>
            {
                _show.ClearAllLayers();
                _mainPublisher.Reset();
            });
            _kelebek1.ClearAllLayers();
            _kelebek2.ClearAllLayers();
            _listePanel.ClearAllLayers();
            Log("** Tüm sahneler alındı!");
        }

        /// <summary>Ctrl+E: ekranda bilinen her grafiğin out director'u birlikte (animasyonlu, onaysız).</summary>
        private void TakeAllOutAnimated()
        {
            if (!EnsureConnected()) return;
            Guard(() =>
            {
                _show.OutAll();
                _mainPublisher.UnpublishAll();
            });
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingsForm(_settings))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ApplySettings(dlg.Settings);
                    Log("** Ayarlar kaydedildi: " + AppSettings.FilePath);
                }
            }
        }

        private void timerKeepAlive_Tick(object sender, EventArgs e)
        {
            RefreshVizMemory();
        }

        /// <summary>Nabız sorgusu; cevaptan doluluk yüzdesini durum çubuğuna yazar.</summary>
        private void RefreshVizMemory()
        {
            if (!IsConnected) return;
            try
            {
                string raw = _viz.TrySend(VizCommands.KeepAlive());   // { TOTAL x PIXEL y ALLOCATED z SIZE w }
                if (raw == null) return;
                Match m = Regex.Match(raw, @"TOTAL\s+(\d+).*ALLOCATED\s+(\d+)");
                if (!m.Success) return;
                double total = double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                double used = double.Parse(m.Groups[2].Value, CultureInfo.InvariantCulture);
                int pct = total > 0 ? (int)Math.Round(used * 100.0 / total) : 0;
                tspbViz.Value = Math.Max(0, Math.Min(100, pct));
                tsslVizPercent.Text = "%" + (used * 100.0 / total).ToString("0.0");
            }
            catch (Exception ex)
            {
                Log("Nabız hatası: " + ex.Message);
                Disconnect();
                UpdateConnectionUi();
                UpdateKjUi();
            }
        }

        // =====================================================================
        //  Haberler: tarih → akış → haber → sahne satırları
        // =====================================================================

        private void LoadRundowns()
        {
            try
            {
                var list = _repo.GetRundowns(dtpTarih.Value.Date).ToList();
                cmbAkis.DataSource = null;
                cmbAkis.DataSource = list;
                cmbAkis.DisplayMember = "ToString";
                if (_repo.IsAvailable) Log("** " + dtpTarih.Value.ToShortDateString() + ": " + list.Count + " akış.");
            }
            catch (Exception ex)
            {
                Log("** Akış listesi okunamadı: " + ex.Message);
            }
            LoadStories();
        }

        private void LoadStories()
        {
            _stories = new List<NewsStory>();
            var rundown = cmbAkis.SelectedItem as Rundown;
            if (rundown != null)
            {
                try
                {
                    _stories = _repo.GetRundownStories(rundown.Date, rundown.DirId, rundown.SaatId).ToList();
                    Log("** Akış " + rundown + ": " + _stories.Count + " haber.");
                }
                catch (Exception ex)
                {
                    Log("** Haber listesi okunamadı: " + ex.Message);
                }
            }
            ApplyStoryFilter();
        }

        private void ApplyStoryFilter()
        {
            string q = txtAra.Text.Trim();
            var filtered = string.IsNullOrEmpty(q)
                ? _stories
                : _stories.Where(s => (s.Baslik ?? "").IndexOf(q, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
            lstHaberler.DataSource = null;
            lstHaberler.DataSource = filtered;
            lstHaberler.DisplayMember = "Baslik";
        }

        private void dtpTarih_ValueChanged(object sender, EventArgs e)
        {
            LoadRundowns();
        }

        private void cmbAkis_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStories();
        }

        private void btnAkisYenile_Click(object sender, EventArgs e)
        {
            LoadRundowns();
        }

        private void btnHaberYenile_Click(object sender, EventArgs e)
        {
            LoadStories();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            ApplyStoryFilter();
        }

        private void btnAraTemizle_Click(object sender, EventArgs e)
        {
            txtAra.Clear();
        }

        /// <summary>Seçilen haberin kayıtlı sahne satırları tabloya yüklenir.</summary>
        private void lstHaberler_SelectedIndexChanged(object sender, EventArgs e)
        {
            var story = lstHaberler.SelectedItem as NewsStory;
            dgvSahne.Rows.Clear();
            if (story == null) return;
            try
            {
                var scenes = _repo.GetNewsSavedScenes(story.AutoId);
                foreach (SavedSceneItem item in scenes) AddSceneRow(item);
                Log("** Haber '" + story.Baslik + "': " + scenes.Count + " sahne satırı.");
            }
            catch (Exception ex)
            {
                Log("** Sahne satırları okunamadı: " + ex.Message);
            }
        }

        /// <summary>Tabloya veritabanı tanımlı bir sahne satırı ekler. Değer #1/#2 = tipin ilk iki alanı.</summary>
        private void AddSceneRow(SavedSceneItem item)
        {
            int idx = dgvSahne.Rows.Add();
            DataGridViewRow row = dgvSahne.Rows[idx];
            row.Tag = item;
            row.Cells[colSira.Index].Value = item.SiraNo > 0 ? item.SiraNo : idx + 1;

            string tipName = item.Type != null ? item.Type.GuiName : "(tip " + item.SceneGuiId + ")";
            if (!colTip.Items.Contains(tipName)) colTip.Items.Add(tipName);
            row.Cells[colTip.Index].Value = tipName;

            if (item.Type != null)
            {
                var elements = item.Type.Elements.OrderBy(el => el.SiraNo).ToList();
                if (elements.Count > 0) row.Cells[colDeger1.Index].Value = item.GetValue(elements[0]);
                if (elements.Count > 1) row.Cells[colDeger2.Index].Value = item.GetValue(elements[1]);
            }
            else
            {
                row.Cells[colDeger1.Index].Value = item.Description;
            }
        }

        /// <summary>Tablodaki Değer #1/#2 hücrelerini tipin ilk iki alanına eşler.</summary>
        private static Dictionary<int, string> ValuesFromRow(DataGridViewRow row, SavedSceneItem item, int deger1Index, int deger2Index)
        {
            var dict = new Dictionary<int, string>(item.Values);
            if (item.Type == null) return dict;
            var elements = item.Type.Elements.OrderBy(el => el.SiraNo).ToList();
            if (elements.Count > 0) dict[elements[0].GuiElementId] = Convert.ToString(row.Cells[deger1Index].Value);
            if (elements.Count > 1) dict[elements[1].GuiElementId] = Convert.ToString(row.Cells[deger2Index].Value);
            return dict;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvSahne.CurrentRow != null && !dgvSahne.CurrentRow.IsNewRow)
                dgvSahne.Rows.Remove(dgvSahne.CurrentRow);   // sadece ekrandan; veritabanına dokunmaz
        }

        private void btnSahneYenile_Click(object sender, EventArgs e)
        {
            lstHaberler_SelectedIndexChanged(sender, e);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Log("** KAYDET: hesabın yazma yetkisi yok; değişiklikler sadece bu oturumda, ekranda geçerli.");
        }

        private void btnKaydetVer_Click(object sender, EventArgs e)
        {
            btnVer_Click(sender, e);
        }

        // =====================================================================
        //  Sahne listesi – VER / AL
        // =====================================================================

        /// <summary>Seçili satırın tipini, Değer #1 ve Değer #2 metnini okur.</summary>
        private bool TryGetCurrentRow(out string tip, out string deger1, out string deger2)
        {
            tip = null;
            deger1 = null;
            deger2 = null;
            DataGridViewRow row = dgvSahne.CurrentRow;
            if (row == null || row.IsNewRow) return false;
            tip = Convert.ToString(row.Cells[colTip.Index].Value);
            deger1 = Convert.ToString(row.Cells[colDeger1.Index].Value);
            deger2 = Convert.ToString(row.Cells[colDeger2.Index].Value);
            return !string.IsNullOrWhiteSpace(deger1);
        }

        private static bool TryMapTip(string tip, out KjType type)
        {
            switch (tip)
            {
                case "KJ Metin": type = KjType.Haber; return true;
                case "Özel Haber Kapak": type = KjType.OzelHaber; return true;
                case "Sıcak Haber Kapak": type = KjType.SicakHaber; return true;
                case "Siyah Metin": type = KjType.SiyahHaber; return true;
                case "Son Dakika": type = KjType.SonDakika; return true;
                case "Az Sonra Kapak": type = KjType.AzSonra; return true;
                default: type = KjType.Haber; return false;
            }
        }

        /// <summary>
        /// VARSAYIM: "KJ İsim" satırında Değer #2 görevi belirtir (MUHABİR / KAMERA / diğer = spiker).
        /// Görev bilgisinin sahne satırından nasıl okunacağı henüz kesinleşmedi.
        /// </summary>
        private static GraphicDefinition MapIsimKj(string deger2)
        {
            string role = (deger2 ?? string.Empty).ToUpperInvariant();
            if (role.Contains("MUHAB")) return GraphicCatalog.Get("muhabirkj");
            if (role.Contains("KAMERA")) return GraphicCatalog.Get("kamerakj");
            return GraphicCatalog.Get("spikerkj");
        }

        /// <summary>
        /// VER (Ctrl+Q).
        ///  - Tek satır seçiliyse onu verir. Birden çok satır seçiliyse (Ctrl/Shift+tık) hepsini aynı anda verir:
        ///    örneğin KJ Metin + KJ İsim + KJ Yer. Birbiriyle çakışan satırlar (iki alt bant) birlikte verilemez.
        ///  - Veriş başarılıysa seçim son verilen satırın altındaki satıra geçer; reji arka arkaya Ctrl+Q ile
        ///    alt alta dizilmiş metinleri sırayla verebilir.
        /// </summary>
        private void btnVer_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            dgvSahne.EndEdit();   // hücre düzenlenirken Ctrl+Q'ya basıldıysa yazılan metin de gitsin

            List<DataGridViewRow> rows = SelectedSceneRows();
            if (rows.Count == 0)
            {
                Log("** Sahne listesinde bir satır seçin.");
                return;
            }

            if (rows.Count == 1)
            {
                if (PublishRow(rows[0])) SelectNextRow(rows[0].Index);
                return;
            }

            // Toplu VER
            string problem = CheckBatch(rows);
            if (problem != null)
            {
                Log("** Toplu VER yapılmadı: " + problem);
                return;
            }
            if (_show.IsBusy || _mainPublisher.IsBusy)
            {
                Log("HATA: Önceki animasyon bitmedi, bekleyin.");
                return;
            }

            int ok = 0;
            _show.BatchMode = true;
            _mainPublisher.BatchMode = true;
            try
            {
                foreach (DataGridViewRow row in rows)
                    if (PublishRow(row)) ok++;
            }
            finally
            {
                _show.BatchMode = false;
                _mainPublisher.BatchMode = false;
            }
            Log("** Toplu VER: " + ok + " / " + rows.Count + " satır verildi.");
            if (ok > 0) SelectNextRow(rows[rows.Count - 1].Index);
        }

        /// <summary>Seçili sahne satırları, tablodaki sırasıyla. Seçim yoksa imleçteki satır.</summary>
        private List<DataGridViewRow> SelectedSceneRows()
        {
            var rows = dgvSahne.SelectedRows.Cast<DataGridViewRow>()
                               .Where(r => !r.IsNewRow)
                               .OrderBy(r => r.Index)
                               .ToList();
            if (rows.Count == 0 && dgvSahne.CurrentRow != null && !dgvSahne.CurrentRow.IsNewRow)
                rows.Add(dgvSahne.CurrentRow);
            return rows;
        }

        /// <summary>Tek bir satırı verir; başarılıysa true. Hata loga düşer.</summary>
        private bool PublishRow(DataGridViewRow row)
        {
            // 1) Veritabanı tanımlı satır.
            var dbItem = row.Tag as SavedSceneItem;
            if (dbItem != null && dbItem.Type != null)
            {
                var values = ValuesFromRow(row, dbItem, colDeger1.Index, colDeger2.Index);
                // Alt bant, akan yazı, yer, canlı, logo, isim KJ: ana sahne kontrolcüsü (logolar + kulakçık onunla gelir).
                if (ShowHaberController.Owns(dbItem.Type.InDirector))
                {
                    string text = FirstValue(dbItem.Type, values);
                    return TryGuard(() => _show.TakeByDirector(dbItem.Type.InDirector, text));
                }
                // Diğerleri: genel motor (tip tanımındaki director'lar ve alanlar).
                return TryGuard(() => _mainPublisher.Publish(dbItem, values));
            }

            // 2) Elle girilen satır: logla doğrulanmış sabit diziler.
            string tip = Convert.ToString(row.Cells[colTip.Index].Value);
            string metin = Convert.ToString(row.Cells[colDeger1.Index].Value);
            string deger2 = Convert.ToString(row.Cells[colDeger2.Index].Value);
            if (string.IsNullOrWhiteSpace(metin))
            {
                Log("** Satır " + (row.Index + 1) + ": Değer #1 boş.");
                return false;
            }

            if (tip == "KJ İsim")
            {
                GraphicDefinition g = MapIsimKj(deger2);
                return TryGuard(() => _show.TakeGraphic(g, metin));
            }
            if (tip == "KJ Yer")
                return TryGuard(() => _show.TakeYer(metin));

            KjType type;
            if (!TryMapTip(tip, out type))
            {
                Log("** '" + tip + "' tipi için komut dizisi henüz loglanmadı.");
                return false;
            }
            return TryGuard(() => _show.TakeKj(type, metin));
        }

        /// <summary>
        /// Toplu VER öncesi kontrol: aynı tipten iki satır ya da birbiriyle çakışan tipler (iki farklı alt bant)
        /// birlikte verilemez; ikincisi ilkini ekrandan indirirdi. Sorun yoksa null.
        /// </summary>
        private string CheckBatch(List<DataGridViewRow> rows)
        {
            var dbTypes = new List<SceneType>();
            var manualTips = new List<string>();
            foreach (DataGridViewRow row in rows)
            {
                var item = row.Tag as SavedSceneItem;
                if (item != null && item.Type != null)
                {
                    SceneType t = item.Type;
                    if (dbTypes.Any(x => x.GuiId == t.GuiId))
                        return "'" + t.GuiName + "' tipinden iki satır seçili; aynı anda yalnızca biri ekranda olabilir.";
                    SceneType clash = dbTypes.FirstOrDefault(x => x.ConflictsWith(t));
                    if (clash != null)
                        return "'" + clash.GuiName + "' ile '" + t.GuiName + "' çakışıyor; birlikte verilemez.";
                    dbTypes.Add(t);
                }
                else
                {
                    string tip = Convert.ToString(row.Cells[colTip.Index].Value);
                    KjType kj;
                    // Elle girilen satırlarda alt bant türleri (az sonra hariç) birbiriyle çakışır.
                    string key = TryMapTip(tip, out kj) && kj != KjType.AzSonra ? "ALT BANT" : tip;
                    if (manualTips.Contains(key))
                        return "'" + tip + "' ile aynı yeri kullanan ikinci bir satır seçili; birlikte verilemez.";
                    manualTips.Add(key);
                }
            }
            return null;
        }

        /// <summary>Tipin ilk alanının (sira_no'ya göre) değeri: önce verilen değerler, yoksa varsayılan.</summary>
        private static string FirstValue(SceneType type, IDictionary<int, string> values)
        {
            SceneTypeElement el = type.Elements.OrderBy(x => x.SiraNo).FirstOrDefault();
            if (el == null) return null;
            string v;
            if (values != null && values.TryGetValue(el.GuiElementId, out v)) return v;
            return el.DefaultValue;
        }

        /// <summary>Veritabanı tipini ekrandan alır: ana sahne grafiği ise kontrolcüyle, değilse genel motorla.</summary>
        private void UnpublishType(SceneType type)
        {
            if (ShowHaberController.Owns(type.InDirector))
                Guard(() => _show.OutByDirector(type.InDirector));
            else
                Guard(() => _mainPublisher.Unpublish(type));
        }

        /// <summary>Verilen satırın altındaki satırı seçer (son satırdaysa yerinde kalır).</summary>
        private void SelectNextRow(int index)
        {
            int next = index + 1;
            if (next >= dgvSahne.Rows.Count || dgvSahne.Rows[next].IsNewRow) return;
            dgvSahne.ClearSelection();
            dgvSahne.CurrentCell = dgvSahne.Rows[next].Cells[colDeger1.Index];
            dgvSahne.Rows[next].Selected = true;
        }

        private void btnAl_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            DataGridViewRow row = dgvSahne.CurrentRow;
            var dbItem = row != null ? row.Tag as SavedSceneItem : null;
            if (dbItem != null && dbItem.Type != null)
            {
                UnpublishType(dbItem.Type);   // yalnızca bu satırın tipi iner
                return;
            }

            string tip, metin, deger2;
            TryGetCurrentRow(out tip, out metin, out deger2);
            Guard(() =>
            {
                if (tip == "Az Sonra Kapak") _show.OutTicker();
                else if (tip == "KJ İsim") _show.OutGraphic(MapIsimKj(deger2));
                else if (tip == "KJ Yer") _show.OutYer();
                else _show.OutKj();
            });
        }

        private void btnTumunuAnimasyonlaAl_Click(object sender, EventArgs e)
        {
            TakeAllOutAnimated();
        }

        // =====================================================================
        //  Farklı KJ tipinde ver
        // =====================================================================

        private void TakeSelectedAs(KjType type)
        {
            if (!EnsureConnected()) return;
            string tip, metin, deger2;
            if (!TryGetCurrentRow(out tip, out metin, out deger2))
            {
                Log("** Sahne listesinde Değer #1 dolu bir satır seçin.");
                return;
            }
            TakeKj(type, metin);
        }

        private void TakeKj(KjType type, string text)
        {
            Guard(() => _show.TakeKj(type, text));
        }

        private void btnKjNormal_Click(object sender, EventArgs e) { TakeSelectedAs(KjType.Haber); }
        private void btnKjOzel_Click(object sender, EventArgs e) { TakeSelectedAs(KjType.OzelHaber); }
        private void btnKjSicak_Click(object sender, EventArgs e) { TakeSelectedAs(KjType.SicakHaber); }
        private void btnKjSiyah_Click(object sender, EventArgs e) { TakeSelectedAs(KjType.SiyahHaber); }
        private void btnKjSonDakika_Click(object sender, EventArgs e) { TakeSelectedAs(KjType.SonDakika); }

        // =====================================================================
        //  Logolar ve diğer basit grafikler
        // =====================================================================

        private void TakeGraphicFrom(ListBox list)
        {
            if (!EnsureConnected()) return;

            // Veritabanından gelen tip (SceneTypeListItem): genel motor.
            var dbType = list.SelectedItem as SceneTypeListItem;
            if (dbType != null)
            {
                if (ShowHaberController.Owns(dbType.Type.InDirector))
                {
                    string defaultText = FirstValue(dbType.Type, null);
                    Guard(() => _show.TakeByDirector(dbType.Type.InDirector, defaultText));
                    return;
                }
                var item = new SavedSceneItem { SavedSceneId = -1, SceneGuiId = dbType.Type.GuiId, Type = dbType.Type, Description = dbType.Text };
                Guard(() => _mainPublisher.Publish(item));
                return;
            }

            var g = list.SelectedItem as GraphicDefinition;
            if (g == null)
            {
                Log("** Listeden bir grafik seçin.");
                return;
            }

            string text = null;
            if (g.NeedsText)
            {
                text = Prompt.Show(this, g.DisplayName, "Metin:", g.DefaultText);
                if (text == null) return;   // iptal
            }
            Guard(() => _show.TakeGraphic(g, text));
        }

        private void OutGraphicFrom(ListBox list)
        {
            if (!EnsureConnected()) return;
            var dbType = list.SelectedItem as SceneTypeListItem;
            if (dbType != null)
            {
                UnpublishType(dbType.Type);
                return;
            }
            var g = list.SelectedItem as GraphicDefinition;
            if (g == null)
            {
                Log("** Listeden bir grafik seçin.");
                return;
            }
            Guard(() => _show.OutGraphic(g));
        }

        private void btnLogoVer_Click(object sender, EventArgs e) { TakeGraphicFrom(lstLogolar); }
        private void btnLogoAl_Click(object sender, EventArgs e) { OutGraphicFrom(lstLogolar); }
        private void btnSagVer_Click(object sender, EventArgs e) { TakeGraphicFrom(lstLogoSahneler); }
        private void btnSagAl_Click(object sender, EventArgs e) { OutGraphicFrom(lstLogoSahneler); }

        /// <summary>"Tümünü Yükle": tüm sahne tiplerinin sahnelerini Engine belleğine alır.</summary>
        private void btnTumunuYukle_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            if (_sceneTypes.Count == 0)
            {
                Log("** Tümünü Yükle: sahne tipi listesi boş (veritabanı yok). Ana sahne yükleniyor.");
                Guard(() => _show.EnsureSceneLoaded());
                return;
            }
            if (MessageBox.Show(_sceneTypes.Count + " tipin sahneleri Engine belleğine yüklenecek, biraz sürer. Devam?",
                                "Tümünü Yükle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            Guard(() =>
            {
                var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (SceneType t in _sceneTypes)
                    if (!string.IsNullOrEmpty(t.SceneFullPath) && paths.Add(t.SceneFullPath)) _mainPublisher.Preload(t);
                Log("** Tümünü Yükle bitti: " + paths.Count + " sahne.");
            });
        }

        // =====================================================================
        //  Canlı / Yer
        // =====================================================================

        private void btnCanliVer_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.TakeCanli());
        }

        private void btnCanliAl_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.OutCanli());
        }

        private void btnYerVer_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.TakeYer(txtYer.Text));
        }

        private void btnYerAl_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.OutYer());
        }

        private void btnCanliYerVer_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.TakeCanliYer(txtYer.Text));
        }

        private void btnCanliYerAl_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.OutCanliYer());
        }

        // =====================================================================
        //  Flot (arka katman)
        // =====================================================================

        private void btnFlotVer_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            string name = txtFlotAd.Text.Trim();
            if (name.Length == 0 && lstFlot.SelectedItem != null)
                name = lstFlot.SelectedItem.ToString();
            if (name.Length == 0)
            {
                Log("** Listeden bir flot seçin ya da sahne adını yazın (örn. FLOT-07).");
                return;
            }
            Guard(() => _show.TakeFlot(name));
        }

        private void btnFlotAl_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _show.OutFlot());
        }

        // =====================================================================
        //  Sahne tipleri
        // =====================================================================

        private void txtAraTip_TextChanged(object sender, EventArgs e)
        {
            if (_sceneTypes.Count == 0) return;   // örnek satırlarda arama yok
            string q = txtAraTip.Text.Trim();
            lstSahneTipleri.Items.Clear();
            foreach (SceneType t in _sceneTypes.Where(t => string.IsNullOrEmpty(q) || t.GuiName.IndexOf(q, StringComparison.CurrentCultureIgnoreCase) >= 0).OrderBy(t => t.GuiName))
                lstSahneTipleri.Items.Add(t);
        }

        /// <summary>Çift tıklanan tipi sahne listesine yeni satır olarak ekler (kullanıcı metni yazar).</summary>
        private void lstSahneTipleri_DoubleClick(object sender, EventArgs e)
        {
            if (lstSahneTipleri.SelectedItem == null) return;

            var dbType = lstSahneTipleri.SelectedItem as SceneType;
            if (dbType != null)
            {
                var item = new SavedSceneItem { SavedSceneId = -1, SceneGuiId = dbType.GuiId, Type = dbType, Description = dbType.GuiName, SiraNo = dgvSahne.Rows.Count + 1 };
                foreach (var el in dbType.Elements) item.Values[el.GuiElementId] = el.DefaultValue;
                AddSceneRow(item);
                dgvSahne.CurrentCell = dgvSahne.Rows[dgvSahne.Rows.Count - 1].Cells[colDeger1.Index];
                dgvSahne.BeginEdit(true);
                return;
            }

            string name = Regex.Replace(lstSahneTipleri.SelectedItem.ToString(), @"\s*\(.*\)\s*$", "");
            int idx = dgvSahne.Rows.Add();
            dgvSahne.Rows[idx].Cells[colSira.Index].Value = idx + 1;
            if (colTip.Items.Contains(name))
                dgvSahne.Rows[idx].Cells[colTip.Index].Value = name;
            dgvSahne.CurrentCell = dgvSahne.Rows[idx].Cells[colDeger1.Index];
            dgvSahne.BeginEdit(true);
        }

        private void FillSceneTypeTree()
        {
            TreeNode root = tvSahneTipleri.Nodes.Add("Tüm Kategoriler");
            root.Nodes.Add("15 Temmuz");
            root.Nodes.Add("2023 Genel Seçim");
            root.Nodes.Add("Gece Hattı");
            root.Nodes.Add("Seçim Ortak");
            root.Nodes.Add("Show Haber");
            root.Nodes.Add("Diğer");
            tvSahneTipleri.Nodes.Add("Sınıflandırılmamış");
            root.Expand();
        }

        /// <summary>Logo listelerini kataloğa göre doldurur (tasarımcıdaki örnek satırların yerine).</summary>
        private void FillGraphicLists()
        {
            lstLogoSahneler.Items.Clear();
            foreach (GraphicDefinition g in GraphicCatalog.All)
                lstLogoSahneler.Items.Add(g);

            lstLogolar.Items.Clear();
            foreach (string key in new[] { "bih", "bhd", "hbk", "neler", "ozelhaberlogo" })
                lstLogolar.Items.Add(GraphicCatalog.Get(key));

            // Flot sahneleri: FLOT-01 .. FLOT-20 (Graphic Hub klasöründe isimli olanlar da var, onlar kutuya yazılır).
            lstFlot.Items.Clear();
            for (int i = 1; i <= 20; i++)
                lstFlot.Items.Add("FLOT-" + i.ToString("00"));
        }

        // =====================================================================
        //  Kısayollar
        // =====================================================================

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Bu kısayollar yalnızca Haberler sekmesinde. Kelebekler ve Listeler sekmesindeki paneller kendi
            // kısayollarını kullanır; burada yakalanırsa Haberler'deki satır yanlışlıkla yayına giderdi.
            if (tabMain.SelectedTab != tabHaberler) return;

            if (e.Control && e.KeyCode == Keys.Q) { e.Handled = true; btnVer_Click(sender, e); }
            else if (e.Control && e.KeyCode == Keys.W) { e.Handled = true; btnAl_Click(sender, e); }
            else if (e.Control && e.KeyCode == Keys.E) { e.Handled = true; btnTumunuAnimasyonlaAl_Click(sender, e); }
            else if (e.Control && e.KeyCode == Keys.Delete) { e.Handled = true; btnSil_Click(sender, e); }
            else if (e.KeyCode == Keys.F5) { e.Handled = true; btnHaberYenile_Click(sender, e); }
        }

        // =====================================================================
        //  Ham komut konsolu
        // =====================================================================

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendFromBox();
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendFromBox();
            }
        }

        private void SendFromBox()
        {
            string cmd = txtCommand.Text.Trim();
            if (cmd.Length == 0 || _viz == null) return;

            try
            {
                _viz.Send(cmd);
            }
            catch (VizCommandException)
            {
                // Cevap zaten CommandLogged ile loglandı ("ERROR ..." satırı).
            }
            catch (Exception ex)
            {
                Log("HATA: " + ex.Message);
                UpdateConnectionUi();
            }
            txtCommand.SelectAll();
            txtCommand.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        // =====================================================================
        //  Log ve arayüz durumu
        // =====================================================================

        private void OnVizCommandLogged(string command, string response)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, string>(OnVizCommandLogged), command, response);
                return;
            }
            Log(">> " + command);
            Log("<< " + response);
        }

        private void OnShowInfo(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(OnShowInfo), message);
                return;
            }
            Log("** " + message);
            tsslStatus.Text = message;
        }

        private static readonly string LogFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        private void Log(string line)
        {
            string stamped = DateTime.Now.ToString("HH:mm:ss.fff") + "  " + line;
            txtLog.AppendText(stamped + Environment.NewLine);
            try { System.IO.File.AppendAllText(LogFilePath, stamped + Environment.NewLine); } catch { /* log dosyası yazılamazsa sessiz geç */ }
        }

        private bool EnsureConnected()
        {
            if (IsConnected) return true;
            Log("** Önce Engine'e bağlanın (Tekrar Bağlan ya da Viz Konsolu sekmesi).");
            return false;
        }

        /// <summary>Viz işlemlerini tek yerde yakalar: hata mesajı loga düşer, uygulama düşmez.</summary>
        private void Guard(Action action)
        {
            TryGuard(action);
        }

        /// <summary>Guard ile aynı; işlem hatasız bittiyse true döner.</summary>
        private bool TryGuard(Action action)
        {
            bool ok = true;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Log("HATA: " + ex.Message);
                ok = false;
            }
            UpdateKjUi();
            return ok;
        }

        private void timerUi_Tick(object sender, EventArgs e)
        {
            UpdateKjUi();
        }

        private void UpdateConnectionUi()
        {
            bool on = IsConnected;
            btnConnect.Text = on ? "Bağlantıyı Kes" : "Bağlan";
            lblStatus.Text = on ? "BAĞLI" : "BAĞLI DEĞİL";
            lblStatus.ForeColor = on ? Color.Green : Color.Firebrick;
            btnSend.Enabled = on;
            txtCommand.Enabled = on;
            txtHost.Enabled = !on;
            numPort.Enabled = !on;
            tsbReconnect.ForeColor = on ? Color.DarkGreen : Color.Firebrick;
            if (!on)
            {
                tspbViz.Value = 0;
                tsslVizPercent.Text = "%0";
            }
        }

        private void UpdateKjUi()
        {
            bool connected = IsConnected;
            bool busy = connected && (_show.IsBusy || (_mainPublisher != null && _mainPublisher.IsBusy));
            bool ready = connected && !busy;

            btnVer.Enabled = ready;
            btnKaydetVer.Enabled = ready;
            btnAl.Enabled = ready;
            btnTumunuAnimasyonlaAl.Enabled = ready;
            tsbTakeAllOut.Enabled = ready;
            btnKjNormal.Enabled = ready;
            btnKjOzel.Enabled = ready;
            btnKjSicak.Enabled = ready;
            btnKjSiyah.Enabled = ready;
            btnKjSonDakika.Enabled = ready;
            btnLogoVer.Enabled = ready;
            btnLogoAl.Enabled = ready;
            btnSagVer.Enabled = ready;
            btnSagAl.Enabled = ready;
            btnTumunuYukle.Enabled = connected;
            btnCanliVer.Enabled = ready;
            btnCanliAl.Enabled = ready;
            btnYerVer.Enabled = ready;
            btnYerAl.Enabled = ready;
            btnCanliYerVer.Enabled = ready;
            btnCanliYerAl.Enabled = ready;
            btnFlotVer.Enabled = ready;
            btnFlotAl.Enabled = ready;

            // Kırmızı düğmeler devre dışıyken gri görünsün; kırmızı zeminde gri yazı okunmuyor.
            tsbTakeAllOut.BackColor = ready ? Color.FromArgb(204, 51, 51) : Color.FromArgb(225, 225, 230);
            btnTumunuAnimasyonlaAl.BackColor = ready ? Color.FromArgb(204, 51, 51) : Color.FromArgb(225, 225, 230);

            if (!connected)
            {
                tsslStatus.Text = "Engine'e bağlı değil";
                return;
            }

            var parts = new List<string>();
            parts.Add(_show.CurrentKj == null
                ? "KJ yok"
                : KjCatalog.Get(_show.CurrentKj.Value).DisplayName + " \"" + _show.CurrentText + "\"");
            if (_show.TickerSent) parts.Add("Akan yazı");
            if (_show.YerOnAir) parts.Add("Yer: " + _show.YerText);
            if (_show.CanliYerOnAir) parts.Add("Canlı/Yer: " + _show.CanliYerText);
            else if (_show.CanliOnAir) parts.Add("Canlı");
            foreach (GraphicDefinition g in _show.OnAirGraphics) parts.Add(g.DisplayName);
            if (_show.CurrentFlot != null) parts.Add("Flot: " + _show.CurrentFlot);
            if (_mainPublisher != null)
                foreach (SavedSceneItem s in _mainPublisher.OnAir)
                    parts.Add(s.Type.GuiName + ": " + s.Description);

            string status = "Ekranda: " + string.Join(" | ", parts);
            if (busy)
            {
                DateTime until = _show.BusyUntil;
                if (_mainPublisher != null && _mainPublisher.BusyUntil > until) until = _mainPublisher.BusyUntil;
                status += "   ANİMASYON " + (until - DateTime.Now).TotalSeconds.ToString("0.0") + " sn";
            }
            tsslStatus.Text = status;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Disconnect();
            if (_kelebek1 != null) _kelebek1.Disconnect();
            if (_kelebek2 != null) _kelebek2.Disconnect();
            if (_listePanel != null) _listePanel.Disconnect();
            base.OnFormClosed(e);
        }
    }
}
