using System;
using System.Windows.Forms;
using SHOW_TV_ANAHABER.Config;

namespace SHOW_TV_ANAHABER.Ui
{
    /// <summary>Ayarlar penceresi: Engine adresleri, program modu, kelebek playlist'leri, veritabanı bağlantı dizeleri.</summary>
    public partial class SettingsForm : Form
    {
        public AppSettings Settings { get; private set; }

        public SettingsForm(AppSettings settings)
        {
            InitializeComponent();
            Settings = settings;
            lblFile.Text = "Dosya: " + AppSettings.FilePath;

            txtEngineHost.Text = settings.EngineHost;
            numEnginePort.Value = settings.EnginePort;
            txtKelebek1Host.Text = settings.Kelebek1Host;
            numKelebek1Port.Value = settings.Kelebek1Port;
            txtKelebek2Host.Text = settings.Kelebek2Host;
            numKelebek2Port.Value = settings.Kelebek2Port;
            txtControllerName.Text = settings.ControllerName;
            numKelebekPlaylist1.Value = settings.KelebekPlaylist1;
            numKelebekPlaylist2.Value = settings.KelebekPlaylist2;
            numKelebekGuiDir.Value = settings.KelebekGuiDirId;
            txtVizrtDb.Text = settings.VizrtDbConnectionString;
            txtNewsDb.Text = settings.NewsDbConnectionString;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.EngineHost = txtEngineHost.Text.Trim();
            Settings.EnginePort = (int)numEnginePort.Value;
            Settings.Kelebek1Host = txtKelebek1Host.Text.Trim();
            Settings.Kelebek1Port = (int)numKelebek1Port.Value;
            Settings.Kelebek2Host = txtKelebek2Host.Text.Trim();
            Settings.Kelebek2Port = (int)numKelebek2Port.Value;
            Settings.ControllerName = txtControllerName.Text.Trim();
            Settings.KelebekPlaylist1 = (int)numKelebekPlaylist1.Value;
            Settings.KelebekPlaylist2 = (int)numKelebekPlaylist2.Value;
            Settings.KelebekGuiDirId = (int)numKelebekGuiDir.Value;
            Settings.VizrtDbConnectionString = txtVizrtDb.Text.Trim();
            Settings.NewsDbConnectionString = txtNewsDb.Text.Trim();

            try
            {
                Settings.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ayarlar kaydedilemedi: " + ex.Message, "Ayarlar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
