namespace SHOW_TV_ANAHABER
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Üst araç çubuğu
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton tsbReconnect;
        private System.Windows.Forms.ToolStripSeparator tssTop1;
        private System.Windows.Forms.ToolStripButton tsbLoadScenes;
        private System.Windows.Forms.ToolStripSeparator tssTop2;
        private System.Windows.Forms.ToolStripButton tsbTakeAllOut;
        private System.Windows.Forms.ToolStripSeparator tssTop3;
        private System.Windows.Forms.ToolStripButton tsbClearMemory;
        private System.Windows.Forms.ToolStripSeparator tssTop4;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripSeparator tssTop5;
        private System.Windows.Forms.ToolStripLabel tslProgramMode;
        private System.Windows.Forms.ToolStripComboBox tscProgramMode;

        // Durum çubuğu
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslVizLabel;
        private System.Windows.Forms.ToolStripProgressBar tspbViz;
        private System.Windows.Forms.ToolStripStatusLabel tsslVizPercent;
        private System.Windows.Forms.ToolStripStatusLabel tsslDetails;

        // Sekmeler
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabHaberler;
        private System.Windows.Forms.TabPage tabListeler;
        private System.Windows.Forms.TabPage tabKelebekler;
        private System.Windows.Forms.TabPage tabKonsol;

        // Haberler sekmesi – üst satır
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.DateTimePicker dtpTarih;
        private System.Windows.Forms.Label lblAkis;
        private System.Windows.Forms.ComboBox cmbAkis;
        private System.Windows.Forms.Button btnAkisYenile;

        // Sol: haberler
        private System.Windows.Forms.Panel pnlHaberler;
        private System.Windows.Forms.Label lblHaberlerHeader;
        private System.Windows.Forms.Button btnHaberYenile;
        private System.Windows.Forms.Label lblAra;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Button btnAraTemizle;
        private System.Windows.Forms.ListBox lstHaberler;

        // Sol alt: canlı / yer
        private System.Windows.Forms.Panel pnlCanliYer;
        private System.Windows.Forms.Label lblCanliYerHeader;
        private System.Windows.Forms.Button btnCanliVer;
        private System.Windows.Forms.Button btnCanliAl;
        private System.Windows.Forms.Label lblYer;
        private System.Windows.Forms.TextBox txtYer;
        private System.Windows.Forms.Button btnYerVer;
        private System.Windows.Forms.Button btnYerAl;
        private System.Windows.Forms.Button btnCanliYerVer;
        private System.Windows.Forms.Button btnCanliYerAl;

        // Orta: sahne listesi
        private System.Windows.Forms.Panel pnlSahne;
        private System.Windows.Forms.Label lblSahneHeader;
        private System.Windows.Forms.Panel pnlSahneUst;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Button btnSahneYenile;
        private System.Windows.Forms.CheckBox chkOtomatikYenile;
        private System.Windows.Forms.DataGridView dgvSahne;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSira;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeger1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeger2;
        private System.Windows.Forms.Panel pnlSahneAlt;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnKaydetVer;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnAl;
        private System.Windows.Forms.Button btnTumunuAnimasyonlaAl;

        // Orta alt: farklı KJ tipinde ver + logolar
        private System.Windows.Forms.Panel pnlFarkliKj;
        private System.Windows.Forms.Label lblFarkliKjHeader;
        private System.Windows.Forms.Button btnKjNormal;
        private System.Windows.Forms.Button btnKjOzel;
        private System.Windows.Forms.Button btnKjSicak;
        private System.Windows.Forms.Button btnKjSiyah;
        private System.Windows.Forms.Button btnKjSonDakika;
        private System.Windows.Forms.ListBox lstLogolar;
        private System.Windows.Forms.Button btnLogoVer;
        private System.Windows.Forms.Button btnLogoAl;

        // Sağ: logo sahneleri
        private System.Windows.Forms.TabControl tabLogoSahneler;
        private System.Windows.Forms.TabPage tabHaberGenel;
        private System.Windows.Forms.TabPage tabHaberFlot;
        private System.Windows.Forms.ListBox lstFlot;
        private System.Windows.Forms.Button btnFlotVer;
        private System.Windows.Forms.Button btnFlotAl;
        private System.Windows.Forms.Label lblFlotAd;
        private System.Windows.Forms.TextBox txtFlotAd;
        private System.Windows.Forms.ComboBox cmbLogoGrubu;
        private System.Windows.Forms.Button btnLogoGrubuYenile;
        private System.Windows.Forms.Button btnSagVer;
        private System.Windows.Forms.Button btnSagAl;
        private System.Windows.Forms.Button btnTumunuYukle;
        private System.Windows.Forms.ListBox lstLogoSahneler;

        // Sağ: sahne tipleri
        private System.Windows.Forms.Panel pnlSahneTipleri;
        private System.Windows.Forms.Label lblSahneTipleriHeader;
        private System.Windows.Forms.TreeView tvSahneTipleri;
        private System.Windows.Forms.Label lblAraTip;
        private System.Windows.Forms.TextBox txtAraTip;
        private System.Windows.Forms.ListBox lstSahneTipleri;

        // Konsol sekmesi
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtLog;

        private System.Windows.Forms.Timer timerUi;
        private System.Windows.Forms.Timer timerKeepAlive;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.tsbReconnect = new System.Windows.Forms.ToolStripButton();
            this.tssTop1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadScenes = new System.Windows.Forms.ToolStripButton();
            this.tssTop2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTakeAllOut = new System.Windows.Forms.ToolStripButton();
            this.tssTop3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClearMemory = new System.Windows.Forms.ToolStripButton();
            this.tssTop4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tssTop5 = new System.Windows.Forms.ToolStripSeparator();
            this.tslProgramMode = new System.Windows.Forms.ToolStripLabel();
            this.tscProgramMode = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslVizLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbViz = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslVizPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDetails = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabHaberler = new System.Windows.Forms.TabPage();
            this.lblTarih = new System.Windows.Forms.Label();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.lblAkis = new System.Windows.Forms.Label();
            this.cmbAkis = new System.Windows.Forms.ComboBox();
            this.btnAkisYenile = new System.Windows.Forms.Button();
            this.pnlHaberler = new System.Windows.Forms.Panel();
            this.lstHaberler = new System.Windows.Forms.ListBox();
            this.btnHaberYenile = new System.Windows.Forms.Button();
            this.lblAra = new System.Windows.Forms.Label();
            this.txtAra = new System.Windows.Forms.TextBox();
            this.btnAraTemizle = new System.Windows.Forms.Button();
            this.lblHaberlerHeader = new System.Windows.Forms.Label();
            this.pnlCanliYer = new System.Windows.Forms.Panel();
            this.btnCanliVer = new System.Windows.Forms.Button();
            this.btnCanliAl = new System.Windows.Forms.Button();
            this.lblYer = new System.Windows.Forms.Label();
            this.txtYer = new System.Windows.Forms.TextBox();
            this.btnYerVer = new System.Windows.Forms.Button();
            this.btnYerAl = new System.Windows.Forms.Button();
            this.btnCanliYerVer = new System.Windows.Forms.Button();
            this.btnCanliYerAl = new System.Windows.Forms.Button();
            this.lblCanliYerHeader = new System.Windows.Forms.Label();
            this.pnlSahne = new System.Windows.Forms.Panel();
            this.dgvSahne = new System.Windows.Forms.DataGridView();
            this.colSira = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTip = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDeger1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeger2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSahneAlt = new System.Windows.Forms.Panel();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnKaydetVer = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnAl = new System.Windows.Forms.Button();
            this.btnTumunuAnimasyonlaAl = new System.Windows.Forms.Button();
            this.pnlSahneUst = new System.Windows.Forms.Panel();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnSahneYenile = new System.Windows.Forms.Button();
            this.chkOtomatikYenile = new System.Windows.Forms.CheckBox();
            this.lblSahneHeader = new System.Windows.Forms.Label();
            this.pnlFarkliKj = new System.Windows.Forms.Panel();
            this.btnKjNormal = new System.Windows.Forms.Button();
            this.btnKjOzel = new System.Windows.Forms.Button();
            this.btnKjSicak = new System.Windows.Forms.Button();
            this.btnKjSiyah = new System.Windows.Forms.Button();
            this.btnKjSonDakika = new System.Windows.Forms.Button();
            this.lstLogolar = new System.Windows.Forms.ListBox();
            this.btnLogoVer = new System.Windows.Forms.Button();
            this.btnLogoAl = new System.Windows.Forms.Button();
            this.lblFarkliKjHeader = new System.Windows.Forms.Label();
            this.tabLogoSahneler = new System.Windows.Forms.TabControl();
            this.tabHaberGenel = new System.Windows.Forms.TabPage();
            this.cmbLogoGrubu = new System.Windows.Forms.ComboBox();
            this.btnLogoGrubuYenile = new System.Windows.Forms.Button();
            this.btnSagVer = new System.Windows.Forms.Button();
            this.btnSagAl = new System.Windows.Forms.Button();
            this.btnTumunuYukle = new System.Windows.Forms.Button();
            this.lstLogoSahneler = new System.Windows.Forms.ListBox();
            this.tabHaberFlot = new System.Windows.Forms.TabPage();
            this.lstFlot = new System.Windows.Forms.ListBox();
            this.btnFlotVer = new System.Windows.Forms.Button();
            this.btnFlotAl = new System.Windows.Forms.Button();
            this.lblFlotAd = new System.Windows.Forms.Label();
            this.txtFlotAd = new System.Windows.Forms.TextBox();
            this.pnlSahneTipleri = new System.Windows.Forms.Panel();
            this.tvSahneTipleri = new System.Windows.Forms.TreeView();
            this.lblSahneTipleriHeader = new System.Windows.Forms.Label();
            this.lblAraTip = new System.Windows.Forms.Label();
            this.txtAraTip = new System.Windows.Forms.TextBox();
            this.lstSahneTipleri = new System.Windows.Forms.ListBox();
            this.tabListeler = new System.Windows.Forms.TabPage();
            this.tabKelebekler = new System.Windows.Forms.TabPage();
            this.tabKonsol = new System.Windows.Forms.TabPage();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.timerUi = new System.Windows.Forms.Timer(this.components);
            this.timerKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.toolStripTop.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabHaberler.SuspendLayout();
            this.pnlHaberler.SuspendLayout();
            this.pnlCanliYer.SuspendLayout();
            this.pnlSahne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSahne)).BeginInit();
            this.pnlSahneAlt.SuspendLayout();
            this.pnlSahneUst.SuspendLayout();
            this.pnlFarkliKj.SuspendLayout();
            this.tabLogoSahneler.SuspendLayout();
            this.tabHaberGenel.SuspendLayout();
            this.tabHaberFlot.SuspendLayout();
            this.pnlSahneTipleri.SuspendLayout();
            this.tabKonsol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.Color.White;
            this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbReconnect,
            this.tssTop1,
            this.tsbLoadScenes,
            this.tssTop2,
            this.tsbTakeAllOut,
            this.tssTop3,
            this.tsbClearMemory,
            this.tssTop4,
            this.tsbSettings,
            this.tssTop5,
            this.tslProgramMode,
            this.tscProgramMode});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.toolStripTop.Size = new System.Drawing.Size(1600, 30);
            this.toolStripTop.TabIndex = 0;
            // 
            // tsbReconnect
            // 
            this.tsbReconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbReconnect.Margin = new System.Windows.Forms.Padding(2, 1, 6, 2);
            this.tsbReconnect.Name = "tsbReconnect";
            this.tsbReconnect.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tsbReconnect.Size = new System.Drawing.Size(93, 21);
            this.tsbReconnect.Text = "Tekrar Bağlan";
            this.tsbReconnect.Click += new System.EventHandler(this.tsbReconnect_Click);
            // 
            // tssTop1
            // 
            this.tssTop1.Name = "tssTop1";
            this.tssTop1.Size = new System.Drawing.Size(6, 24);
            // 
            // tsbLoadScenes
            // 
            this.tsbLoadScenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLoadScenes.Margin = new System.Windows.Forms.Padding(6, 1, 6, 2);
            this.tsbLoadScenes.Name = "tsbLoadScenes";
            this.tsbLoadScenes.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tsbLoadScenes.Size = new System.Drawing.Size(103, 21);
            this.tsbLoadScenes.Text = "Sahneleri Yükle";
            this.tsbLoadScenes.Click += new System.EventHandler(this.tsbLoadScenes_Click);
            // 
            // tssTop2
            // 
            this.tssTop2.Name = "tssTop2";
            this.tssTop2.Size = new System.Drawing.Size(6, 24);
            // 
            // tsbTakeAllOut
            // 
            this.tsbTakeAllOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tsbTakeAllOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbTakeAllOut.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.tsbTakeAllOut.ForeColor = System.Drawing.Color.White;
            this.tsbTakeAllOut.Margin = new System.Windows.Forms.Padding(6, 1, 6, 2);
            this.tsbTakeAllOut.Name = "tsbTakeAllOut";
            this.tsbTakeAllOut.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsbTakeAllOut.Size = new System.Drawing.Size(100, 21);
            this.tsbTakeAllOut.Text = "Tümünü Al";
            this.tsbTakeAllOut.Click += new System.EventHandler(this.tsbTakeAllOut_Click);
            // 
            // tssTop3
            // 
            this.tssTop3.Name = "tssTop3";
            this.tssTop3.Size = new System.Drawing.Size(6, 24);
            // 
            // tsbClearMemory
            // 
            this.tsbClearMemory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClearMemory.Margin = new System.Windows.Forms.Padding(6, 1, 6, 2);
            this.tsbClearMemory.Name = "tsbClearMemory";
            this.tsbClearMemory.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tsbClearMemory.Size = new System.Drawing.Size(100, 21);
            this.tsbClearMemory.Text = "Belleği Temizle";
            this.tsbClearMemory.Click += new System.EventHandler(this.tsbClearMemory_Click);
            // 
            // tssTop4
            // 
            this.tssTop4.Name = "tssTop4";
            this.tssTop4.Size = new System.Drawing.Size(6, 24);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSettings.Margin = new System.Windows.Forms.Padding(6, 1, 6, 2);
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tsbSettings.Size = new System.Drawing.Size(60, 21);
            this.tsbSettings.Text = "Ayarlar";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tssTop5
            // 
            this.tssTop5.Name = "tssTop5";
            this.tssTop5.Size = new System.Drawing.Size(6, 24);
            // 
            // tslProgramMode
            // 
            this.tslProgramMode.ForeColor = System.Drawing.Color.DimGray;
            this.tslProgramMode.Margin = new System.Windows.Forms.Padding(10, 1, 4, 2);
            this.tslProgramMode.Name = "tslProgramMode";
            this.tslProgramMode.Size = new System.Drawing.Size(88, 21);
            this.tslProgramMode.Text = "Program Modu";
            // 
            // tscProgramMode
            // 
            this.tscProgramMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscProgramMode.Items.AddRange(new object[] {
            "Show Ana Haber"});
            this.tscProgramMode.Name = "tscProgramMode";
            this.tscProgramMode.Size = new System.Drawing.Size(170, 24);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.White;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tsslVizLabel,
            this.tspbViz,
            this.tsslVizPercent,
            this.tsslDetails});
            this.statusStrip.Location = new System.Drawing.Point(0, 876);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(8, 0, 14, 0);
            this.statusStrip.Size = new System.Drawing.Size(1600, 24);
            this.statusStrip.TabIndex = 2;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(1307, 19);
            this.tsslStatus.Spring = true;
            this.tsslStatus.Text = "Hazır";
            this.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslVizLabel
            // 
            this.tsslVizLabel.ForeColor = System.Drawing.Color.DimGray;
            this.tsslVizLabel.Name = "tsslVizLabel";
            this.tsslVizLabel.Size = new System.Drawing.Size(34, 19);
            this.tsslVizLabel.Text = "VizRT";
            // 
            // tspbViz
            // 
            this.tspbViz.Name = "tspbViz";
            this.tspbViz.Size = new System.Drawing.Size(150, 18);
            // 
            // tsslVizPercent
            // 
            this.tsslVizPercent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsslVizPercent.Name = "tsslVizPercent";
            this.tsslVizPercent.Size = new System.Drawing.Size(24, 19);
            this.tsslVizPercent.Text = "%0";
            // 
            // tsslDetails
            // 
            this.tsslDetails.ForeColor = System.Drawing.Color.DimGray;
            this.tsslDetails.Name = "tsslDetails";
            this.tsslDetails.Size = new System.Drawing.Size(61, 19);
            this.tsslDetails.Text = "DETAYLAR";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabHaberler);
            this.tabMain.Controls.Add(this.tabListeler);
            this.tabMain.Controls.Add(this.tabKelebekler);
            this.tabMain.Controls.Add(this.tabKonsol);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(90, 26);
            this.tabMain.Location = new System.Drawing.Point(0, 30);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(12, 4);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1600, 846);
            this.tabMain.TabIndex = 1;
            // 
            // tabHaberler
            // 
            this.tabHaberler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabHaberler.Controls.Add(this.lblTarih);
            this.tabHaberler.Controls.Add(this.dtpTarih);
            this.tabHaberler.Controls.Add(this.lblAkis);
            this.tabHaberler.Controls.Add(this.cmbAkis);
            this.tabHaberler.Controls.Add(this.btnAkisYenile);
            this.tabHaberler.Controls.Add(this.pnlHaberler);
            this.tabHaberler.Controls.Add(this.pnlCanliYer);
            this.tabHaberler.Controls.Add(this.pnlSahne);
            this.tabHaberler.Controls.Add(this.pnlFarkliKj);
            this.tabHaberler.Controls.Add(this.tabLogoSahneler);
            this.tabHaberler.Controls.Add(this.pnlSahneTipleri);
            this.tabHaberler.Controls.Add(this.lblAraTip);
            this.tabHaberler.Controls.Add(this.txtAraTip);
            this.tabHaberler.Controls.Add(this.lstSahneTipleri);
            this.tabHaberler.Location = new System.Drawing.Point(4, 30);
            this.tabHaberler.Name = "tabHaberler";
            this.tabHaberler.Padding = new System.Windows.Forms.Padding(3);
            this.tabHaberler.Size = new System.Drawing.Size(1592, 812);
            this.tabHaberler.TabIndex = 0;
            this.tabHaberler.Text = "Haberler";
            // 
            // lblTarih
            // 
            this.lblTarih.AutoSize = true;
            this.lblTarih.ForeColor = System.Drawing.Color.DimGray;
            this.lblTarih.Location = new System.Drawing.Point(10, 14);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(36, 17);
            this.lblTarih.TabIndex = 0;
            this.lblTarih.Text = "Tarih";
            // 
            // dtpTarih
            // 
            this.dtpTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarih.Location = new System.Drawing.Point(52, 10);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(120, 25);
            this.dtpTarih.ValueChanged += new System.EventHandler(this.dtpTarih_ValueChanged);
            this.dtpTarih.TabIndex = 1;
            // 
            // lblAkis
            // 
            this.lblAkis.AutoSize = true;
            this.lblAkis.ForeColor = System.Drawing.Color.DimGray;
            this.lblAkis.Location = new System.Drawing.Point(192, 14);
            this.lblAkis.Name = "lblAkis";
            this.lblAkis.Size = new System.Drawing.Size(46, 17);
            this.lblAkis.TabIndex = 2;
            this.lblAkis.Text = "Akışlar";
            // 
            // cmbAkis
            // 
            this.cmbAkis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAkis.Location = new System.Drawing.Point(246, 10);
            this.cmbAkis.Name = "cmbAkis";
            this.cmbAkis.Size = new System.Drawing.Size(320, 25);
            this.cmbAkis.SelectedIndexChanged += new System.EventHandler(this.cmbAkis_SelectedIndexChanged);
            this.cmbAkis.TabIndex = 3;
            // 
            // btnAkisYenile
            // 
            this.btnAkisYenile.Location = new System.Drawing.Point(576, 8);
            this.btnAkisYenile.Name = "btnAkisYenile";
            this.btnAkisYenile.Size = new System.Drawing.Size(120, 29);
            this.btnAkisYenile.TabIndex = 4;
            this.btnAkisYenile.Text = "Akış Yenile";
            this.btnAkisYenile.UseVisualStyleBackColor = true;
            this.btnAkisYenile.Click += new System.EventHandler(this.btnAkisYenile_Click);
            // 
            // pnlHaberler
            // 
            this.pnlHaberler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlHaberler.BackColor = System.Drawing.Color.White;
            this.pnlHaberler.Controls.Add(this.lstHaberler);
            this.pnlHaberler.Controls.Add(this.btnHaberYenile);
            this.pnlHaberler.Controls.Add(this.lblAra);
            this.pnlHaberler.Controls.Add(this.txtAra);
            this.pnlHaberler.Controls.Add(this.btnAraTemizle);
            this.pnlHaberler.Controls.Add(this.lblHaberlerHeader);
            this.pnlHaberler.Location = new System.Drawing.Point(10, 46);
            this.pnlHaberler.Name = "pnlHaberler";
            this.pnlHaberler.Size = new System.Drawing.Size(360, 524);
            this.pnlHaberler.TabIndex = 5;
            // 
            // lstHaberler
            // 
            this.lstHaberler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHaberler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstHaberler.IntegralHeight = false;
            this.lstHaberler.ItemHeight = 17;
            this.lstHaberler.Location = new System.Drawing.Point(8, 74);
            this.lstHaberler.Name = "lstHaberler";
            this.lstHaberler.Size = new System.Drawing.Size(344, 442);
            this.lstHaberler.TabIndex = 0;
            this.lstHaberler.SelectedIndexChanged += new System.EventHandler(this.lstHaberler_SelectedIndexChanged);
            // 
            // btnHaberYenile
            // 
            this.btnHaberYenile.Location = new System.Drawing.Point(8, 36);
            this.btnHaberYenile.Name = "btnHaberYenile";
            this.btnHaberYenile.Size = new System.Drawing.Size(100, 30);
            this.btnHaberYenile.TabIndex = 1;
            this.btnHaberYenile.Text = "Yenile  (F5)";
            this.btnHaberYenile.UseVisualStyleBackColor = true;
            this.btnHaberYenile.Click += new System.EventHandler(this.btnHaberYenile_Click);
            // 
            // lblAra
            // 
            this.lblAra.AutoSize = true;
            this.lblAra.ForeColor = System.Drawing.Color.DimGray;
            this.lblAra.Location = new System.Drawing.Point(124, 42);
            this.lblAra.Name = "lblAra";
            this.lblAra.Size = new System.Drawing.Size(28, 17);
            this.lblAra.TabIndex = 2;
            this.lblAra.Text = "Ara";
            // 
            // txtAra
            // 
            this.txtAra.Location = new System.Drawing.Point(156, 39);
            this.txtAra.Name = "txtAra";
            this.txtAra.Size = new System.Drawing.Size(156, 25);
            this.txtAra.TabIndex = 3;
            this.txtAra.TextChanged += new System.EventHandler(this.txtAra_TextChanged);
            // 
            // btnAraTemizle
            // 
            this.btnAraTemizle.Location = new System.Drawing.Point(318, 37);
            this.btnAraTemizle.Name = "btnAraTemizle";
            this.btnAraTemizle.Size = new System.Drawing.Size(34, 29);
            this.btnAraTemizle.TabIndex = 4;
            this.btnAraTemizle.Text = "✕";
            this.btnAraTemizle.UseVisualStyleBackColor = true;
            this.btnAraTemizle.Click += new System.EventHandler(this.btnAraTemizle_Click);
            // 
            // lblHaberlerHeader
            // 
            this.lblHaberlerHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblHaberlerHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHaberlerHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHaberlerHeader.ForeColor = System.Drawing.Color.White;
            this.lblHaberlerHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHaberlerHeader.Name = "lblHaberlerHeader";
            this.lblHaberlerHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblHaberlerHeader.Size = new System.Drawing.Size(360, 28);
            this.lblHaberlerHeader.TabIndex = 5;
            this.lblHaberlerHeader.Text = "HABERLER";
            this.lblHaberlerHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCanliYer
            // 
            this.pnlCanliYer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlCanliYer.BackColor = System.Drawing.Color.White;
            this.pnlCanliYer.Controls.Add(this.btnCanliVer);
            this.pnlCanliYer.Controls.Add(this.btnCanliAl);
            this.pnlCanliYer.Controls.Add(this.lblYer);
            this.pnlCanliYer.Controls.Add(this.txtYer);
            this.pnlCanliYer.Controls.Add(this.btnYerVer);
            this.pnlCanliYer.Controls.Add(this.btnYerAl);
            this.pnlCanliYer.Controls.Add(this.btnCanliYerVer);
            this.pnlCanliYer.Controls.Add(this.btnCanliYerAl);
            this.pnlCanliYer.Controls.Add(this.lblCanliYerHeader);
            this.pnlCanliYer.Location = new System.Drawing.Point(10, 580);
            this.pnlCanliYer.Name = "pnlCanliYer";
            this.pnlCanliYer.Size = new System.Drawing.Size(360, 224);
            this.pnlCanliYer.TabIndex = 6;
            // 
            // btnCanliVer
            // 
            this.btnCanliVer.Location = new System.Drawing.Point(8, 38);
            this.btnCanliVer.Name = "btnCanliVer";
            this.btnCanliVer.Size = new System.Drawing.Size(168, 34);
            this.btnCanliVer.TabIndex = 0;
            this.btnCanliVer.Text = "Canlı Ver";
            this.btnCanliVer.UseVisualStyleBackColor = true;
            this.btnCanliVer.Click += new System.EventHandler(this.btnCanliVer_Click);
            // 
            // btnCanliAl
            // 
            this.btnCanliAl.Location = new System.Drawing.Point(184, 38);
            this.btnCanliAl.Name = "btnCanliAl";
            this.btnCanliAl.Size = new System.Drawing.Size(168, 34);
            this.btnCanliAl.TabIndex = 1;
            this.btnCanliAl.Text = "Canlı Al";
            this.btnCanliAl.UseVisualStyleBackColor = true;
            this.btnCanliAl.Click += new System.EventHandler(this.btnCanliAl_Click);
            // 
            // lblYer
            // 
            this.lblYer.AutoSize = true;
            this.lblYer.ForeColor = System.Drawing.Color.DimGray;
            this.lblYer.Location = new System.Drawing.Point(8, 88);
            this.lblYer.Name = "lblYer";
            this.lblYer.Size = new System.Drawing.Size(26, 17);
            this.lblYer.TabIndex = 2;
            this.lblYer.Text = "Yer";
            // 
            // txtYer
            // 
            this.txtYer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtYer.Location = new System.Drawing.Point(184, 84);
            this.txtYer.Name = "txtYer";
            this.txtYer.Size = new System.Drawing.Size(168, 25);
            this.txtYer.TabIndex = 3;
            this.txtYer.Text = "İSTANBUL";
            // 
            // btnYerVer
            // 
            this.btnYerVer.Location = new System.Drawing.Point(8, 124);
            this.btnYerVer.Name = "btnYerVer";
            this.btnYerVer.Size = new System.Drawing.Size(168, 34);
            this.btnYerVer.TabIndex = 4;
            this.btnYerVer.Text = "Yer Ver";
            this.btnYerVer.UseVisualStyleBackColor = true;
            this.btnYerVer.Click += new System.EventHandler(this.btnYerVer_Click);
            // 
            // btnYerAl
            // 
            this.btnYerAl.Location = new System.Drawing.Point(184, 124);
            this.btnYerAl.Name = "btnYerAl";
            this.btnYerAl.Size = new System.Drawing.Size(168, 34);
            this.btnYerAl.TabIndex = 5;
            this.btnYerAl.Text = "Yer Al";
            this.btnYerAl.UseVisualStyleBackColor = true;
            this.btnYerAl.Click += new System.EventHandler(this.btnYerAl_Click);
            // 
            // btnCanliYerVer
            // 
            this.btnCanliYerVer.Location = new System.Drawing.Point(8, 170);
            this.btnCanliYerVer.Name = "btnCanliYerVer";
            this.btnCanliYerVer.Size = new System.Drawing.Size(168, 34);
            this.btnCanliYerVer.TabIndex = 6;
            this.btnCanliYerVer.Text = "Canlı / Yer Ver";
            this.btnCanliYerVer.UseVisualStyleBackColor = true;
            this.btnCanliYerVer.Click += new System.EventHandler(this.btnCanliYerVer_Click);
            // 
            // btnCanliYerAl
            // 
            this.btnCanliYerAl.Location = new System.Drawing.Point(184, 170);
            this.btnCanliYerAl.Name = "btnCanliYerAl";
            this.btnCanliYerAl.Size = new System.Drawing.Size(168, 34);
            this.btnCanliYerAl.TabIndex = 7;
            this.btnCanliYerAl.Text = "Canlı / Yer Al";
            this.btnCanliYerAl.UseVisualStyleBackColor = true;
            this.btnCanliYerAl.Click += new System.EventHandler(this.btnCanliYerAl_Click);
            // 
            // lblCanliYerHeader
            // 
            this.lblCanliYerHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblCanliYerHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCanliYerHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCanliYerHeader.ForeColor = System.Drawing.Color.White;
            this.lblCanliYerHeader.Location = new System.Drawing.Point(0, 0);
            this.lblCanliYerHeader.Name = "lblCanliYerHeader";
            this.lblCanliYerHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblCanliYerHeader.Size = new System.Drawing.Size(360, 28);
            this.lblCanliYerHeader.TabIndex = 8;
            this.lblCanliYerHeader.Text = "CANLI / YER";
            this.lblCanliYerHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSahne
            // 
            this.pnlSahne.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSahne.BackColor = System.Drawing.Color.White;
            this.pnlSahne.Controls.Add(this.dgvSahne);
            this.pnlSahne.Controls.Add(this.pnlSahneAlt);
            this.pnlSahne.Controls.Add(this.pnlSahneUst);
            this.pnlSahne.Controls.Add(this.lblSahneHeader);
            this.pnlSahne.Location = new System.Drawing.Point(380, 46);
            this.pnlSahne.Name = "pnlSahne";
            this.pnlSahne.Size = new System.Drawing.Size(878, 474);
            this.pnlSahne.TabIndex = 7;
            // 
            // dgvSahne
            // 
            this.dgvSahne.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.dgvSahne.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSahne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSahne.BackgroundColor = System.Drawing.Color.White;
            this.dgvSahne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSahne.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSahne.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(237)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(237)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSahne.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSahne.ColumnHeadersHeight = 30;
            this.dgvSahne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSahne.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSira,
            this.colTip,
            this.colDeger1,
            this.colDeger2});
            this.dgvSahne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSahne.EnableHeadersVisualStyles = false;
            this.dgvSahne.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.dgvSahne.Location = new System.Drawing.Point(0, 70);
            this.dgvSahne.MultiSelect = true;
            this.dgvSahne.Name = "dgvSahne";
            this.dgvSahne.RowHeadersVisible = false;
            this.dgvSahne.RowTemplate.Height = 28;
            this.dgvSahne.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSahne.Size = new System.Drawing.Size(878, 354);
            this.dgvSahne.TabIndex = 0;
            // 
            // colSira
            // 
            this.colSira.FillWeight = 8F;
            this.colSira.HeaderText = "Sıra";
            this.colSira.Name = "colSira";
            // 
            // colTip
            // 
            this.colTip.FillWeight = 24F;
            this.colTip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colTip.HeaderText = "Tip";
            this.colTip.Items.AddRange(new object[] {
            "KJ Metin",
            "Özel Haber Kapak",
            "Sıcak Haber Kapak",
            "Siyah Metin",
            "Son Dakika",
            "Az Sonra Kapak",
            "KJ İsim",
            "KJ Yer",
            "Telefon Kapak"});
            this.colTip.Name = "colTip";
            // 
            // colDeger1
            // 
            this.colDeger1.FillWeight = 48F;
            this.colDeger1.HeaderText = "Değer #1";
            this.colDeger1.Name = "colDeger1";
            // 
            // colDeger2
            // 
            this.colDeger2.FillWeight = 20F;
            this.colDeger2.HeaderText = "Değer #2";
            this.colDeger2.Name = "colDeger2";
            // 
            // pnlSahneAlt
            // 
            this.pnlSahneAlt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.pnlSahneAlt.Controls.Add(this.btnKaydet);
            this.pnlSahneAlt.Controls.Add(this.btnKaydetVer);
            this.pnlSahneAlt.Controls.Add(this.btnVer);
            this.pnlSahneAlt.Controls.Add(this.btnAl);
            this.pnlSahneAlt.Controls.Add(this.btnTumunuAnimasyonlaAl);
            this.pnlSahneAlt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSahneAlt.Location = new System.Drawing.Point(0, 424);
            this.pnlSahneAlt.Name = "pnlSahneAlt";
            this.pnlSahneAlt.Size = new System.Drawing.Size(878, 50);
            this.pnlSahneAlt.TabIndex = 1;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(8, 8);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(100, 34);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnKaydetVer
            // 
            this.btnKaydetVer.Location = new System.Drawing.Point(116, 8);
            this.btnKaydetVer.Name = "btnKaydetVer";
            this.btnKaydetVer.Size = new System.Drawing.Size(160, 34);
            this.btnKaydetVer.TabIndex = 1;
            this.btnKaydetVer.Text = "Kaydet-Ver  (Ctrl+S)";
            this.btnKaydetVer.UseVisualStyleBackColor = true;
            this.btnKaydetVer.Click += new System.EventHandler(this.btnKaydetVer_Click);
            // 
            // btnVer
            // 
            this.btnVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(240)))), ((int)(((byte)(214)))));
            this.btnVer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(20)))));
            this.btnVer.Location = new System.Drawing.Point(296, 8);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(130, 34);
            this.btnVer.TabIndex = 2;
            this.btnVer.Text = "VER  (Ctrl+Q)";
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnAl
            // 
            this.btnAl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            this.btnAl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(110)))));
            this.btnAl.Location = new System.Drawing.Point(434, 8);
            this.btnAl.Name = "btnAl";
            this.btnAl.Size = new System.Drawing.Size(130, 34);
            this.btnAl.TabIndex = 3;
            this.btnAl.Text = "AL  (Ctrl+W)";
            this.btnAl.UseVisualStyleBackColor = false;
            this.btnAl.Click += new System.EventHandler(this.btnAl_Click);
            // 
            // btnTumunuAnimasyonlaAl
            // 
            this.btnTumunuAnimasyonlaAl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTumunuAnimasyonlaAl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnTumunuAnimasyonlaAl.FlatAppearance.BorderSize = 0;
            this.btnTumunuAnimasyonlaAl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTumunuAnimasyonlaAl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTumunuAnimasyonlaAl.ForeColor = System.Drawing.Color.White;
            this.btnTumunuAnimasyonlaAl.Location = new System.Drawing.Point(600, 8);
            this.btnTumunuAnimasyonlaAl.Name = "btnTumunuAnimasyonlaAl";
            this.btnTumunuAnimasyonlaAl.Size = new System.Drawing.Size(270, 34);
            this.btnTumunuAnimasyonlaAl.TabIndex = 4;
            this.btnTumunuAnimasyonlaAl.Text = "Tümünü Animasyonla Al  (Ctrl+E)";
            this.btnTumunuAnimasyonlaAl.UseVisualStyleBackColor = false;
            this.btnTumunuAnimasyonlaAl.Click += new System.EventHandler(this.btnTumunuAnimasyonlaAl_Click);
            // 
            // pnlSahneUst
            // 
            this.pnlSahneUst.Controls.Add(this.btnSil);
            this.pnlSahneUst.Controls.Add(this.btnSahneYenile);
            this.pnlSahneUst.Controls.Add(this.chkOtomatikYenile);
            this.pnlSahneUst.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSahneUst.Location = new System.Drawing.Point(0, 28);
            this.pnlSahneUst.Name = "pnlSahneUst";
            this.pnlSahneUst.Size = new System.Drawing.Size(878, 42);
            this.pnlSahneUst.TabIndex = 2;
            // 
            // btnSil
            // 
            this.btnSil.ForeColor = System.Drawing.Color.Firebrick;
            this.btnSil.Location = new System.Drawing.Point(8, 6);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(110, 30);
            this.btnSil.TabIndex = 0;
            this.btnSil.Text = "Sil  (Ctrl+Del)";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnSahneYenile
            // 
            this.btnSahneYenile.Location = new System.Drawing.Point(126, 6);
            this.btnSahneYenile.Name = "btnSahneYenile";
            this.btnSahneYenile.Size = new System.Drawing.Size(100, 30);
            this.btnSahneYenile.TabIndex = 1;
            this.btnSahneYenile.Text = "Yenile  (F5)";
            this.btnSahneYenile.UseVisualStyleBackColor = true;
            this.btnSahneYenile.Click += new System.EventHandler(this.btnSahneYenile_Click);
            // 
            // chkOtomatikYenile
            // 
            this.chkOtomatikYenile.AutoSize = true;
            this.chkOtomatikYenile.Location = new System.Drawing.Point(240, 11);
            this.chkOtomatikYenile.Name = "chkOtomatikYenile";
            this.chkOtomatikYenile.Size = new System.Drawing.Size(117, 21);
            this.chkOtomatikYenile.TabIndex = 2;
            this.chkOtomatikYenile.Text = "Otomatik Yenile";
            this.chkOtomatikYenile.UseVisualStyleBackColor = true;
            // 
            // lblSahneHeader
            // 
            this.lblSahneHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblSahneHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSahneHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSahneHeader.ForeColor = System.Drawing.Color.White;
            this.lblSahneHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSahneHeader.Name = "lblSahneHeader";
            this.lblSahneHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblSahneHeader.Size = new System.Drawing.Size(878, 28);
            this.lblSahneHeader.TabIndex = 3;
            this.lblSahneHeader.Text = "SAHNE LİSTESİ";
            this.lblSahneHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFarkliKj
            // 
            this.pnlFarkliKj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFarkliKj.BackColor = System.Drawing.Color.White;
            this.pnlFarkliKj.Controls.Add(this.btnKjNormal);
            this.pnlFarkliKj.Controls.Add(this.btnKjOzel);
            this.pnlFarkliKj.Controls.Add(this.btnKjSicak);
            this.pnlFarkliKj.Controls.Add(this.btnKjSiyah);
            this.pnlFarkliKj.Controls.Add(this.btnKjSonDakika);
            this.pnlFarkliKj.Controls.Add(this.lstLogolar);
            this.pnlFarkliKj.Controls.Add(this.btnLogoVer);
            this.pnlFarkliKj.Controls.Add(this.btnLogoAl);
            this.pnlFarkliKj.Controls.Add(this.lblFarkliKjHeader);
            this.pnlFarkliKj.Location = new System.Drawing.Point(918, 530);
            this.pnlFarkliKj.Name = "pnlFarkliKj";
            this.pnlFarkliKj.Size = new System.Drawing.Size(340, 274);
            this.pnlFarkliKj.TabIndex = 8;
            // 
            // btnKjNormal
            // 
            this.btnKjNormal.Location = new System.Drawing.Point(8, 36);
            this.btnKjNormal.Name = "btnKjNormal";
            this.btnKjNormal.Size = new System.Drawing.Size(160, 32);
            this.btnKjNormal.TabIndex = 0;
            this.btnKjNormal.Text = "Normal Metin";
            this.btnKjNormal.UseVisualStyleBackColor = true;
            this.btnKjNormal.Click += new System.EventHandler(this.btnKjNormal_Click);
            // 
            // btnKjOzel
            // 
            this.btnKjOzel.Location = new System.Drawing.Point(172, 36);
            this.btnKjOzel.Name = "btnKjOzel";
            this.btnKjOzel.Size = new System.Drawing.Size(160, 32);
            this.btnKjOzel.TabIndex = 1;
            this.btnKjOzel.Text = "Özel Haber";
            this.btnKjOzel.UseVisualStyleBackColor = true;
            this.btnKjOzel.Click += new System.EventHandler(this.btnKjOzel_Click);
            // 
            // btnKjSicak
            // 
            this.btnKjSicak.Location = new System.Drawing.Point(8, 74);
            this.btnKjSicak.Name = "btnKjSicak";
            this.btnKjSicak.Size = new System.Drawing.Size(160, 32);
            this.btnKjSicak.TabIndex = 2;
            this.btnKjSicak.Text = "Sıcak Haber";
            this.btnKjSicak.UseVisualStyleBackColor = true;
            this.btnKjSicak.Click += new System.EventHandler(this.btnKjSicak_Click);
            // 
            // btnKjSiyah
            // 
            this.btnKjSiyah.Location = new System.Drawing.Point(172, 74);
            this.btnKjSiyah.Name = "btnKjSiyah";
            this.btnKjSiyah.Size = new System.Drawing.Size(160, 32);
            this.btnKjSiyah.TabIndex = 3;
            this.btnKjSiyah.Text = "Siyah Metin";
            this.btnKjSiyah.UseVisualStyleBackColor = true;
            this.btnKjSiyah.Click += new System.EventHandler(this.btnKjSiyah_Click);
            // 
            // btnKjSonDakika
            // 
            this.btnKjSonDakika.Location = new System.Drawing.Point(8, 112);
            this.btnKjSonDakika.Name = "btnKjSonDakika";
            this.btnKjSonDakika.Size = new System.Drawing.Size(160, 32);
            this.btnKjSonDakika.TabIndex = 4;
            this.btnKjSonDakika.Text = "Son Dakika";
            this.btnKjSonDakika.UseVisualStyleBackColor = true;
            this.btnKjSonDakika.Click += new System.EventHandler(this.btnKjSonDakika_Click);
            // 
            // lstLogolar
            // 
            this.lstLogolar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLogolar.IntegralHeight = false;
            this.lstLogolar.ItemHeight = 17;
            this.lstLogolar.Items.AddRange(new object[] {
            "Bir İnsan Hikayesi",
            "Bu Habere Dikkat",
            "Herkes Bunu Konuşuyor",
            "Neler Oldu Neler",
            "Özel Haber"});
            this.lstLogolar.Location = new System.Drawing.Point(8, 154);
            this.lstLogolar.Name = "lstLogolar";
            this.lstLogolar.Size = new System.Drawing.Size(324, 72);
            this.lstLogolar.TabIndex = 5;
            // 
            // btnLogoVer
            // 
            this.btnLogoVer.Location = new System.Drawing.Point(8, 234);
            this.btnLogoVer.Name = "btnLogoVer";
            this.btnLogoVer.Size = new System.Drawing.Size(160, 32);
            this.btnLogoVer.TabIndex = 6;
            this.btnLogoVer.Text = "Logo Ver";
            this.btnLogoVer.UseVisualStyleBackColor = true;
            this.btnLogoVer.Click += new System.EventHandler(this.btnLogoVer_Click);
            // 
            // btnLogoAl
            // 
            this.btnLogoAl.Location = new System.Drawing.Point(172, 234);
            this.btnLogoAl.Name = "btnLogoAl";
            this.btnLogoAl.Size = new System.Drawing.Size(160, 32);
            this.btnLogoAl.TabIndex = 7;
            this.btnLogoAl.Text = "Logo Al";
            this.btnLogoAl.UseVisualStyleBackColor = true;
            this.btnLogoAl.Click += new System.EventHandler(this.btnLogoAl_Click);
            // 
            // lblFarkliKjHeader
            // 
            this.lblFarkliKjHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblFarkliKjHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFarkliKjHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFarkliKjHeader.ForeColor = System.Drawing.Color.White;
            this.lblFarkliKjHeader.Location = new System.Drawing.Point(0, 0);
            this.lblFarkliKjHeader.Name = "lblFarkliKjHeader";
            this.lblFarkliKjHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblFarkliKjHeader.Size = new System.Drawing.Size(340, 28);
            this.lblFarkliKjHeader.TabIndex = 8;
            this.lblFarkliKjHeader.Text = "FARKLI KJ TİPİNDE VER";
            this.lblFarkliKjHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabLogoSahneler
            // 
            this.tabLogoSahneler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLogoSahneler.Controls.Add(this.tabHaberGenel);
            this.tabLogoSahneler.Controls.Add(this.tabHaberFlot);
            this.tabLogoSahneler.Location = new System.Drawing.Point(1268, 8);
            this.tabLogoSahneler.Name = "tabLogoSahneler";
            this.tabLogoSahneler.SelectedIndex = 0;
            this.tabLogoSahneler.Size = new System.Drawing.Size(318, 290);
            this.tabLogoSahneler.TabIndex = 9;
            // 
            // tabHaberGenel
            // 
            this.tabHaberGenel.BackColor = System.Drawing.Color.White;
            this.tabHaberGenel.Controls.Add(this.cmbLogoGrubu);
            this.tabHaberGenel.Controls.Add(this.btnLogoGrubuYenile);
            this.tabHaberGenel.Controls.Add(this.btnSagVer);
            this.tabHaberGenel.Controls.Add(this.btnSagAl);
            this.tabHaberGenel.Controls.Add(this.btnTumunuYukle);
            this.tabHaberGenel.Controls.Add(this.lstLogoSahneler);
            this.tabHaberGenel.Location = new System.Drawing.Point(4, 26);
            this.tabHaberGenel.Name = "tabHaberGenel";
            this.tabHaberGenel.Padding = new System.Windows.Forms.Padding(3);
            this.tabHaberGenel.Size = new System.Drawing.Size(310, 260);
            this.tabHaberGenel.TabIndex = 0;
            this.tabHaberGenel.Text = "Haber Genel";
            // 
            // cmbLogoGrubu
            // 
            this.cmbLogoGrubu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogoGrubu.Items.AddRange(new object[] {
            "HABER GENEL"});
            this.cmbLogoGrubu.Location = new System.Drawing.Point(6, 8);
            this.cmbLogoGrubu.Name = "cmbLogoGrubu";
            this.cmbLogoGrubu.Size = new System.Drawing.Size(258, 25);
            this.cmbLogoGrubu.TabIndex = 0;
            // 
            // btnLogoGrubuYenile
            // 
            this.btnLogoGrubuYenile.Location = new System.Drawing.Point(270, 6);
            this.btnLogoGrubuYenile.Name = "btnLogoGrubuYenile";
            this.btnLogoGrubuYenile.Size = new System.Drawing.Size(34, 29);
            this.btnLogoGrubuYenile.TabIndex = 1;
            this.btnLogoGrubuYenile.Text = "↻";
            this.btnLogoGrubuYenile.UseVisualStyleBackColor = true;
            // 
            // btnSagVer
            // 
            this.btnSagVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(240)))), ((int)(((byte)(214)))));
            this.btnSagVer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSagVer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(20)))));
            this.btnSagVer.Location = new System.Drawing.Point(6, 40);
            this.btnSagVer.Name = "btnSagVer";
            this.btnSagVer.Size = new System.Drawing.Size(80, 32);
            this.btnSagVer.TabIndex = 2;
            this.btnSagVer.Text = "VER";
            this.btnSagVer.UseVisualStyleBackColor = false;
            this.btnSagVer.Click += new System.EventHandler(this.btnSagVer_Click);
            // 
            // btnSagAl
            // 
            this.btnSagAl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            this.btnSagAl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSagAl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(110)))));
            this.btnSagAl.Location = new System.Drawing.Point(92, 40);
            this.btnSagAl.Name = "btnSagAl";
            this.btnSagAl.Size = new System.Drawing.Size(80, 32);
            this.btnSagAl.TabIndex = 3;
            this.btnSagAl.Text = "AL";
            this.btnSagAl.UseVisualStyleBackColor = false;
            this.btnSagAl.Click += new System.EventHandler(this.btnSagAl_Click);
            // 
            // btnTumunuYukle
            // 
            this.btnTumunuYukle.Location = new System.Drawing.Point(178, 40);
            this.btnTumunuYukle.Name = "btnTumunuYukle";
            this.btnTumunuYukle.Size = new System.Drawing.Size(126, 32);
            this.btnTumunuYukle.TabIndex = 4;
            this.btnTumunuYukle.Text = "Tümünü Yükle";
            this.btnTumunuYukle.UseVisualStyleBackColor = true;
            this.btnTumunuYukle.Click += new System.EventHandler(this.btnTumunuYukle_Click);
            // 
            // lstLogoSahneler
            // 
            this.lstLogoSahneler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLogoSahneler.IntegralHeight = false;
            this.lstLogoSahneler.ItemHeight = 17;
            this.lstLogoSahneler.Items.AddRange(new object[] {
            "Show Haber Logo",
            "Özel Haber Logo",
            "Son Dakika Logo",
            "Sıcak Haber Logo",
            "Herkes Bunu Konuşuyor Logo",
            "Sokak Ne Diyor Logo",
            "EVDE KAL",
            "Bir İnsan Hikayesi Logo",
            "KISA KISA"});
            this.lstLogoSahneler.Location = new System.Drawing.Point(6, 80);
            this.lstLogoSahneler.Name = "lstLogoSahneler";
            this.lstLogoSahneler.Size = new System.Drawing.Size(298, 172);
            this.lstLogoSahneler.TabIndex = 5;
            // 
            // tabHaberFlot
            // 
            this.tabHaberFlot.BackColor = System.Drawing.Color.White;
            this.tabHaberFlot.Controls.Add(this.lblFlotAd);
            this.tabHaberFlot.Controls.Add(this.txtFlotAd);
            this.tabHaberFlot.Controls.Add(this.btnFlotVer);
            this.tabHaberFlot.Controls.Add(this.btnFlotAl);
            this.tabHaberFlot.Controls.Add(this.lstFlot);
            this.tabHaberFlot.Location = new System.Drawing.Point(4, 26);
            this.tabHaberFlot.Name = "tabHaberFlot";
            this.tabHaberFlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabHaberFlot.Size = new System.Drawing.Size(310, 260);
            this.tabHaberFlot.TabIndex = 1;
            this.tabHaberFlot.Text = "Haber Flot";
            //
            // lblFlotAd
            //
            this.lblFlotAd.AutoSize = true;
            this.lblFlotAd.ForeColor = System.Drawing.Color.DimGray;
            this.lblFlotAd.Location = new System.Drawing.Point(6, 12);
            this.lblFlotAd.Name = "lblFlotAd";
            this.lblFlotAd.Size = new System.Drawing.Size(40, 17);
            this.lblFlotAd.Text = "Sahne";
            //
            // txtFlotAd
            //
            this.txtFlotAd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFlotAd.Location = new System.Drawing.Point(56, 8);
            this.txtFlotAd.Name = "txtFlotAd";
            this.txtFlotAd.Size = new System.Drawing.Size(248, 25);
            //
            // btnFlotVer
            //
            this.btnFlotVer.BackColor = System.Drawing.Color.FromArgb(214, 240, 214);
            this.btnFlotVer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnFlotVer.ForeColor = System.Drawing.Color.FromArgb(20, 90, 20);
            this.btnFlotVer.Location = new System.Drawing.Point(6, 40);
            this.btnFlotVer.Name = "btnFlotVer";
            this.btnFlotVer.Size = new System.Drawing.Size(146, 32);
            this.btnFlotVer.Text = "FLOT VER";
            this.btnFlotVer.UseVisualStyleBackColor = false;
            this.btnFlotVer.Click += new System.EventHandler(this.btnFlotVer_Click);
            //
            // btnFlotAl
            //
            this.btnFlotAl.BackColor = System.Drawing.Color.FromArgb(218, 226, 244);
            this.btnFlotAl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnFlotAl.ForeColor = System.Drawing.Color.FromArgb(30, 50, 110);
            this.btnFlotAl.Location = new System.Drawing.Point(158, 40);
            this.btnFlotAl.Name = "btnFlotAl";
            this.btnFlotAl.Size = new System.Drawing.Size(146, 32);
            this.btnFlotAl.Text = "FLOT AL";
            this.btnFlotAl.UseVisualStyleBackColor = false;
            this.btnFlotAl.Click += new System.EventHandler(this.btnFlotAl_Click);
            //
            // lstFlot
            //
            this.lstFlot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstFlot.IntegralHeight = false;
            this.lstFlot.ItemHeight = 20;
            this.lstFlot.Location = new System.Drawing.Point(6, 80);
            this.lstFlot.Name = "lstFlot";
            this.lstFlot.Size = new System.Drawing.Size(298, 172);
            this.lstFlot.DoubleClick += new System.EventHandler(this.btnFlotVer_Click);
            // 
            // pnlSahneTipleri
            // 
            this.pnlSahneTipleri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSahneTipleri.BackColor = System.Drawing.Color.White;
            this.pnlSahneTipleri.Controls.Add(this.tvSahneTipleri);
            this.pnlSahneTipleri.Controls.Add(this.lblSahneTipleriHeader);
            this.pnlSahneTipleri.Location = new System.Drawing.Point(1268, 308);
            this.pnlSahneTipleri.Name = "pnlSahneTipleri";
            this.pnlSahneTipleri.Size = new System.Drawing.Size(318, 226);
            this.pnlSahneTipleri.TabIndex = 10;
            // 
            // tvSahneTipleri
            // 
            this.tvSahneTipleri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSahneTipleri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSahneTipleri.ItemHeight = 22;
            this.tvSahneTipleri.Location = new System.Drawing.Point(0, 28);
            this.tvSahneTipleri.Name = "tvSahneTipleri";
            this.tvSahneTipleri.Size = new System.Drawing.Size(318, 198);
            this.tvSahneTipleri.TabIndex = 0;
            // 
            // lblSahneTipleriHeader
            // 
            this.lblSahneTipleriHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(100)))), ((int)(((byte)(160)))));
            this.lblSahneTipleriHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSahneTipleriHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSahneTipleriHeader.ForeColor = System.Drawing.Color.White;
            this.lblSahneTipleriHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSahneTipleriHeader.Name = "lblSahneTipleriHeader";
            this.lblSahneTipleriHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblSahneTipleriHeader.Size = new System.Drawing.Size(318, 28);
            this.lblSahneTipleriHeader.TabIndex = 1;
            this.lblSahneTipleriHeader.Text = "SAHNE TİPLERİ";
            this.lblSahneTipleriHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAraTip
            // 
            this.lblAraTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAraTip.AutoSize = true;
            this.lblAraTip.ForeColor = System.Drawing.Color.DimGray;
            this.lblAraTip.Location = new System.Drawing.Point(1268, 548);
            this.lblAraTip.Name = "lblAraTip";
            this.lblAraTip.Size = new System.Drawing.Size(28, 17);
            this.lblAraTip.TabIndex = 11;
            this.lblAraTip.Text = "Ara";
            // 
            // txtAraTip
            // 
            this.txtAraTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAraTip.Location = new System.Drawing.Point(1302, 544);
            this.txtAraTip.Name = "txtAraTip";
            this.txtAraTip.Size = new System.Drawing.Size(284, 25);
            this.txtAraTip.TabIndex = 12;
            this.txtAraTip.TextChanged += new System.EventHandler(this.txtAraTip_TextChanged);
            // 
            // lstSahneTipleri
            // 
            this.lstSahneTipleri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSahneTipleri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSahneTipleri.IntegralHeight = false;
            this.lstSahneTipleri.ItemHeight = 17;
            this.lstSahneTipleri.Items.AddRange(new object[] {
            "KJ İsim (122)",
            "KJ Metin (151)",
            "Az Sonra Kapak (152)",
            "Son Dakika (153)",
            "KJ Yer (154)",
            "Logo - Herkes Bunu Konuşuyor (1..)",
            "Logo - Özel Haber (157)",
            "Telefon Kapak (158)",
            "Sıcak Haber Kapak (161)",
            "Özel Haber Kapak (162)",
            "Logo - Sokak Ne Diyor (163)"});
            this.lstSahneTipleri.Location = new System.Drawing.Point(1268, 578);
            this.lstSahneTipleri.Name = "lstSahneTipleri";
            this.lstSahneTipleri.Size = new System.Drawing.Size(318, 226);
            this.lstSahneTipleri.TabIndex = 13;
            this.lstSahneTipleri.DoubleClick += new System.EventHandler(this.lstSahneTipleri_DoubleClick);
            // 
            // tabListeler
            // 
            this.tabListeler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabListeler.Location = new System.Drawing.Point(4, 30);
            this.tabListeler.Name = "tabListeler";
            this.tabListeler.Padding = new System.Windows.Forms.Padding(3);
            this.tabListeler.Size = new System.Drawing.Size(1592, 808);
            this.tabListeler.TabIndex = 1;
            this.tabListeler.Text = "Listeler";
            // 
            // tabKelebekler
            // 
            this.tabKelebekler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabKelebekler.Location = new System.Drawing.Point(4, 30);
            this.tabKelebekler.Name = "tabKelebekler";
            this.tabKelebekler.Padding = new System.Windows.Forms.Padding(3);
            this.tabKelebekler.Size = new System.Drawing.Size(1592, 808);
            this.tabKelebekler.TabIndex = 2;
            this.tabKelebekler.Text = "Kelebekler";
            // 
            // tabKonsol
            // 
            this.tabKonsol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tabKonsol.Controls.Add(this.lblHost);
            this.tabKonsol.Controls.Add(this.txtHost);
            this.tabKonsol.Controls.Add(this.lblPort);
            this.tabKonsol.Controls.Add(this.numPort);
            this.tabKonsol.Controls.Add(this.btnConnect);
            this.tabKonsol.Controls.Add(this.lblStatus);
            this.tabKonsol.Controls.Add(this.btnClear);
            this.tabKonsol.Controls.Add(this.txtCommand);
            this.tabKonsol.Controls.Add(this.btnSend);
            this.tabKonsol.Controls.Add(this.txtLog);
            this.tabKonsol.Location = new System.Drawing.Point(4, 30);
            this.tabKonsol.Name = "tabKonsol";
            this.tabKonsol.Padding = new System.Windows.Forms.Padding(3);
            this.tabKonsol.Size = new System.Drawing.Size(1592, 808);
            this.tabKonsol.TabIndex = 3;
            this.tabKonsol.Text = "Viz Konsolu";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(10, 16);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(50, 17);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Engine:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(66, 12);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(140, 25);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(216, 16);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 17);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(256, 12);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(70, 25);
            this.numPort.TabIndex = 3;
            this.numPort.Value = new decimal(new int[] {
            6100,
            0,
            0,
            0});
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(336, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 29);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Bağlan";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(468, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 17);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "BAĞLI DEĞİL";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(1482, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 29);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Log Temizle";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCommand.Location = new System.Drawing.Point(10, 48);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(1464, 23);
            this.txtCommand.TabIndex = 7;
            this.txtCommand.Text = "RENDERER*MAIN_LAYER*TREE*$edt_txt_haber*GEOM*TEXT GET";
            this.txtCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCommand_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(1482, 45);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 29);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Gönder";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtLog.Location = new System.Drawing.Point(10, 82);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1572, 716);
            this.txtLog.TabIndex = 9;
            this.txtLog.WordWrap = false;
            // 
            // timerUi
            // 
            this.timerUi.Enabled = true;
            this.timerUi.Interval = 200;
            this.timerUi.Tick += new System.EventHandler(this.timerUi_Tick);
            // 
            // timerKeepAlive
            // 
            this.timerKeepAlive.Interval = 5000;
            this.timerKeepAlive.Tick += new System.EventHandler(this.timerKeepAlive_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1300, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show TV Reji (ANA HABER) - Yeni Sürüm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabHaberler.ResumeLayout(false);
            this.tabHaberler.PerformLayout();
            this.pnlHaberler.ResumeLayout(false);
            this.pnlHaberler.PerformLayout();
            this.pnlCanliYer.ResumeLayout(false);
            this.pnlCanliYer.PerformLayout();
            this.pnlSahne.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSahne)).EndInit();
            this.pnlSahneAlt.ResumeLayout(false);
            this.pnlSahneUst.ResumeLayout(false);
            this.pnlSahneUst.PerformLayout();
            this.pnlFarkliKj.ResumeLayout(false);
            this.tabLogoSahneler.ResumeLayout(false);
            this.tabHaberGenel.ResumeLayout(false);
            this.tabHaberFlot.ResumeLayout(false);
            this.tabHaberFlot.PerformLayout();
            this.pnlSahneTipleri.ResumeLayout(false);
            this.tabKonsol.ResumeLayout(false);
            this.tabKonsol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
