using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SHOW_TV_ANAHABER.Data;
using SHOW_TV_ANAHABER.Viz;

namespace SHOW_TV_ANAHABER.Ui
{
    /// <summary>
    /// Bir kelebek hedefi (Viz #1 ya da Viz #2): sabit bir playlist'in kayıtlı sahneleri, seçili sahnenin alanları,
    /// VER / GÜNCELLE / AL ve TÜMÜNÜ HAZIRLA.    /// Veritabanı yoksa liste boş kalır; tip listesi de boş olur.
    /// </summary>
    public partial class KelebekPanel : UserControl
    {
        private IShowRepository _repo = new NullShowRepository();
        private VizEngineClient _viz;
        private GenericScenePublisher _publisher;
        private int _playlistId;
        private int _guiDirId;
        private List<SceneType> _types = new List<SceneType>();
        private SavedSceneItem _current;

        public event Action<string> Log;

        public KelebekPanel()
        {
            InitializeComponent();
        }

        /// <summary>Paneli yapılandırır. host/port: bu hedefin Viz makinesi.</summary>
        public void Configure(string title, IShowRepository repo, int playlistId, int guiDirId, string host, int port)
        {
            lblHeader.Text = title + "   (" + host + ":" + port + ")";
            _repo = repo ?? new NullShowRepository();
            _playlistId = playlistId;
            _guiDirId = guiDirId;

            Disconnect();
            _viz = new VizEngineClient(host, port);
            _viz.CommandLogged += (c, r) => Say(">> " + c + "   << " + r);
            _publisher = new GenericScenePublisher(_viz) { UnpublishPrevious = true };
            _publisher.Info += m => Say("** " + m);

            RefreshLists();
            UpdateUi();
        }

        public void Disconnect()
        {
            if (_viz != null) _viz.Dispose();
            _viz = null;
            _publisher = null;
        }

        private bool EnsureConnected()
        {
            if (_viz == null) return false;
            if (_viz.IsConnected) return true;
            try
            {
                _viz.Connect();
                Say("Bağlandı: " + _viz.Host + ":" + _viz.Port);
                return true;
            }
            catch (Exception ex)
            {
                Say("HATA: " + ex.Message);
                return false;
            }
        }

        // ---- Listeler -------------------------------------------------------------------

        /// <summary>Panelin playlist'ini değiştirir (Listeler sekmesi için). Engine bağlantısına dokunmaz.</summary>
        public void SetPlaylist(int playlistId, string title)
        {
            _playlistId = playlistId;
            if (title != null && _viz != null) lblHeader.Text = title + "   (" + _viz.Host + ":" + _viz.Port + ")";
            RefreshLists();
            UpdateUi();
        }

        public void RefreshLists()
        {
            try
            {
                // Kategori 0 = kısıt yok, tüm tipler (Listeler sekmesi).
                _types = (_guiDirId > 0 ? _repo.GetSceneTypesByCategory(_guiDirId) : _repo.GetSceneTypes()).ToList();
                cmbNewType.DataSource = null;
                cmbNewType.DataSource = _types;
                cmbNewType.DisplayMember = "GuiName";

                var scenes = _repo.GetPlaylistSavedScenes(_playlistId).ToList();
                lstScenes.DataSource = null;
                lstScenes.DataSource = scenes;
                lstScenes.DisplayMember = "Description";

                if (!_repo.IsAvailable)
                    Say("Veritabanı ayarlanmadı; kelebek listesi boş. (Ayarlar > bağlantı dizesi)");
                else
                    Say("Kelebek listesi yenilendi: playlist " + _playlistId + " -> " + scenes.Count + " sahne, kategori " + _guiDirId + " -> " + _types.Count + " tip.");
            }
            catch (Exception ex)
            {
                Say("Liste okunamadı: " + ex.Message);
            }
            ShowScene(lstScenes.SelectedItem as SavedSceneItem);
        }

        private void ShowScene(SavedSceneItem item)
        {
            _current = item;
            dgvValues.Rows.Clear();
            if (item == null || item.Type == null)
            {
                lblSceneInfo.Text = item == null ? "Sahne seçilmedi" : "Tip tanımı bulunamadı (gui_id " + item.SceneGuiId + ")";
                return;
            }
            lblSceneInfo.Text = item.Type.GuiName + "  –  " + item.Type.SceneFullPath + "  (katman " + item.Type.TargetLayer + ")";
            foreach (SceneTypeElement el in item.Type.Elements.OrderBy(e => e.SiraNo))
            {
                int idx = dgvValues.Rows.Add();
                dgvValues.Rows[idx].Tag = el;
                dgvValues.Rows[idx].Cells[0].Value = el.Title;
                dgvValues.Rows[idx].Cells[1].Value = item.GetValue(el);
            }
            FillDirectorList(item.Type);
        }

        /// <summary>Sahnenin director'larını Engine'den okuyup elle oynatma listesine koyar (sahne bellekteyse).</summary>
        private void FillDirectorList(SceneType type)
        {
            cmbDirector.Items.Clear();
            if (_publisher == null || _viz == null || !_viz.IsConnected) return;
            try
            {
                foreach (string d in _publisher.GetDirectorNames(type)) cmbDirector.Items.Add(d);
                if (cmbDirector.Items.Count > 0) cmbDirector.SelectedIndex = 0;
            }
            catch { /* sahne bellekte değilse liste boş kalır */ }
        }

        private void btnDirStart_Click(object sender, EventArgs e)
        {
            if (_current == null || _current.Type == null || cmbDirector.SelectedItem == null) { Say("Önce sahne ve director seçin."); return; }
            if (!EnsureConnected()) return;
            Guard(() => _publisher.RunDirector(_current.Type, cmbDirector.SelectedItem.ToString(), rewind: true));
        }

        private void btnDirContinue_Click(object sender, EventArgs e)
        {
            if (_current == null || _current.Type == null || cmbDirector.SelectedItem == null) { Say("Önce sahne ve director seçin."); return; }
            if (!EnsureConnected()) return;
            Guard(() => _publisher.RunDirector(_current.Type, cmbDirector.SelectedItem.ToString(), rewind: false));
        }

        /// <summary>Tablodaki (düzenlenmiş olabilecek) değerleri gui_element_id -> değer olarak toplar.</summary>
        private Dictionary<int, string> CollectValues()
        {
            dgvValues.EndEdit();   // hücre düzenlenirken kısayola basıldıysa yazılan metin de alınsın
            var dict = new Dictionary<int, string>();
            foreach (DataGridViewRow row in dgvValues.Rows)
            {
                var el = row.Tag as SceneTypeElement;
                if (el == null) continue;
                dict[el.GuiElementId] = Convert.ToString(row.Cells[1].Value);
            }
            return dict;
        }

        // ---- Düğmeler --------------------------------------------------------------------

        private void lstScenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowScene(lstScenes.SelectedItem as SavedSceneItem);
            UpdateUi();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // Yeni, kaydedilmeyen bir sahne (veritabanına yazma yetkimiz yok; sadece bu oturumda, ekranda).
            var type = cmbNewType.SelectedItem as SceneType;
            if (type == null) { Say("Önce tip seçin."); return; }
            var item = new SavedSceneItem { SavedSceneId = -1, SceneGuiId = type.GuiId, Type = type, Description = "(yeni) " + type.GuiName };
            foreach (var el in type.Elements) item.Values[el.GuiElementId] = el.DefaultValue;

            var list = (lstScenes.DataSource as List<SavedSceneItem>) ?? new List<SavedSceneItem>();
            list = list.ToList();
            list.Add(item);
            lstScenes.DataSource = null;
            lstScenes.DataSource = list;
            lstScenes.DisplayMember = "Description";
            lstScenes.SelectedItem = item;
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (_current == null) { Say("Listeden sahne seçin."); return; }
            if (!EnsureConnected()) return;
            Guard(() => _publisher.Publish(_current, CollectValues()));
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (_current == null) { Say("Listeden sahne seçin."); return; }
            if (!EnsureConnected()) return;
            Guard(() => _publisher.Update(_current, CollectValues()));
        }

        private void btnAl_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            Guard(() => _publisher.Unpublish());
        }

        private void btnPreloadAll_Click(object sender, EventArgs e)
        {
            if (!EnsureConnected()) return;
            if (MessageBox.Show("Listedeki tüm sahneler Engine belleğine yüklenecek. Devam?", "Tümünü Hazırla",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            Guard(() =>
            {
                var paths = new HashSet<string>();
                foreach (SavedSceneItem s in lstScenes.Items.OfType<SavedSceneItem>())
                    if (s.Type != null && paths.Add(s.Type.SceneFullPath)) _publisher.Preload(s.Type);
                Say("Ön yükleme bitti: " + paths.Count + " sahne.");
            });
        }

        /// <summary>
        /// Ana toolbar "Tümünü Al": bu hedefin Engine'inde tüm katmanlar animasyonsuz boşaltılır.
        /// Panel hiç bağlanmadıysa dokunulmaz (Tümünü Al için yeni bağlantı açılmaz).
        /// </summary>
        public void ClearAllLayers()
        {
            if (_viz == null || !_viz.IsConnected) return;
            Guard(() =>
            {
                foreach (string layer in VizCommands.AllLayersReverse)
                {
                    _viz.TrySend(VizCommands.LayerStageStop(layer));
                    _viz.TrySend(VizCommands.ClearLayer(layer));
                }
                _publisher.Reset();
                Say("Tümünü Al: " + _viz.Host + ":" + _viz.Port + " katmanları boşaltıldı.");
            });
        }

        /// <summary>Panelin kendi kısayolları (odak paneldeyken): Ctrl+Q VER, Ctrl+G GÜNCELLE, Ctrl+W AL, F5 YENİLE.</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Q: if (btnVer.Enabled) btnVer_Click(this, EventArgs.Empty); return true;
                case Keys.Control | Keys.G: if (btnGuncelle.Enabled) btnGuncelle_Click(this, EventArgs.Empty); return true;
                case Keys.Control | Keys.W: if (btnAl.Enabled) btnAl_Click(this, EventArgs.Empty); return true;
                case Keys.F5: btnRefresh_Click(this, EventArgs.Empty); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timerUi_Tick(object sender, EventArgs e)
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            bool ready = _publisher != null && !_publisher.IsBusy;
            btnVer.Enabled = ready && _current != null;
            btnGuncelle.Enabled = ready && _current != null;
            btnAl.Enabled = ready && _publisher != null && _publisher.Current != null;
            btnPreloadAll.Enabled = _publisher != null && lstScenes.Items.Count > 0;

            if (_publisher == null) { lblState.Text = "Yapılandırılmadı"; return; }
            string s = _publisher.Current == null ? "Ekranda sahne yok" : "Ekranda: " + _publisher.Current.Description;
            if (_publisher.IsBusy) s += "   ANİMASYON " + (_publisher.BusyUntil - DateTime.Now).TotalSeconds.ToString("0.0") + " sn";
            lblState.Text = s;
            lblState.ForeColor = _publisher.Current == null ? Color.DimGray : Color.Green;
        }

        private void Guard(Action a)
        {
            try { a(); }
            catch (Exception ex) { Say("HATA: " + ex.Message); }
            UpdateUi();
        }

        private void Say(string m)
        {
            if (Log != null)
            {
                if (InvokeRequired) BeginInvoke(new Action<string>(x => Log(x)), m);
                else Log(m);
            }
        }
    }
}
