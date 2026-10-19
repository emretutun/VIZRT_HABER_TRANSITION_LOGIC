namespace SHOW_TV_ANAHABER.Ui
{
    partial class KelebekPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnPreloadAll;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ComboBox cmbNewType;
        private System.Windows.Forms.ListBox lstScenes;
        private System.Windows.Forms.Label lblEditHeader;
        private System.Windows.Forms.Panel pnlEditTop;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnAl;
        private System.Windows.Forms.Label lblSceneInfo;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.ComboBox cmbDirector;
        private System.Windows.Forms.Button btnDirStart;
        private System.Windows.Forms.Button btnDirContinue;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeger;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.Timer timerUi;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnPreloadAll = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cmbNewType = new System.Windows.Forms.ComboBox();
            this.lstScenes = new System.Windows.Forms.ListBox();
            this.lblEditHeader = new System.Windows.Forms.Label();
            this.pnlEditTop = new System.Windows.Forms.Panel();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnGuncelle = new System.Windows.Forms.Button();
            this.btnAl = new System.Windows.Forms.Button();
            this.lblSceneInfo = new System.Windows.Forms.Label();
            this.lblDirector = new System.Windows.Forms.Label();
            this.cmbDirector = new System.Windows.Forms.ComboBox();
            this.btnDirStart = new System.Windows.Forms.Button();
            this.btnDirContinue = new System.Windows.Forms.Button();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.colAlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblState = new System.Windows.Forms.Label();
            this.split = new System.Windows.Forms.SplitContainer();
            this.timerUi = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            this.pnlEditTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            //
            // lblHeader
            //
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(96, 100, 160);
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblHeader.Size = new System.Drawing.Size(600, 28);
            this.lblHeader.Text = "KELEBEK LİSTESİ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlTop
            //
            this.pnlTop.Controls.Add(this.btnPreloadAll);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.btnNew);
            this.pnlTop.Controls.Add(this.cmbNewType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(600, 42);
            //
            // btnPreloadAll
            //
            this.btnPreloadAll.Location = new System.Drawing.Point(6, 6);
            this.btnPreloadAll.Name = "btnPreloadAll";
            this.btnPreloadAll.Size = new System.Drawing.Size(130, 30);
            this.btnPreloadAll.Text = "Tümünü Hazırla";
            this.btnPreloadAll.UseVisualStyleBackColor = true;
            this.btnPreloadAll.Click += new System.EventHandler(this.btnPreloadAll_Click);
            //
            // btnRefresh
            //
            this.btnRefresh.Location = new System.Drawing.Point(142, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // btnNew
            //
            this.btnNew.Location = new System.Drawing.Point(250, 6);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(70, 30);
            this.btnNew.Text = "Yeni";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            //
            // cmbNewType
            //
            this.cmbNewType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewType.Location = new System.Drawing.Point(326, 9);
            this.cmbNewType.Name = "cmbNewType";
            this.cmbNewType.Size = new System.Drawing.Size(268, 25);
            //
            // lstScenes
            //
            this.lstScenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstScenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstScenes.IntegralHeight = false;
            this.lstScenes.ItemHeight = 20;
            this.lstScenes.Name = "lstScenes";
            this.lstScenes.SelectedIndexChanged += new System.EventHandler(this.lstScenes_SelectedIndexChanged);
            this.lstScenes.DoubleClick += new System.EventHandler(this.btnVer_Click);
            //
            // lblEditHeader
            //
            this.lblEditHeader.BackColor = System.Drawing.Color.FromArgb(96, 100, 160);
            this.lblEditHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEditHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEditHeader.ForeColor = System.Drawing.Color.White;
            this.lblEditHeader.Name = "lblEditHeader";
            this.lblEditHeader.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblEditHeader.Size = new System.Drawing.Size(600, 28);
            this.lblEditHeader.Text = "SAHNE DÜZENLEME";
            this.lblEditHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlEditTop
            //
            this.pnlEditTop.Controls.Add(this.btnVer);
            this.pnlEditTop.Controls.Add(this.btnGuncelle);
            this.pnlEditTop.Controls.Add(this.btnAl);
            this.pnlEditTop.Controls.Add(this.lblSceneInfo);
            this.pnlEditTop.Controls.Add(this.lblDirector);
            this.pnlEditTop.Controls.Add(this.cmbDirector);
            this.pnlEditTop.Controls.Add(this.btnDirStart);
            this.pnlEditTop.Controls.Add(this.btnDirContinue);
            this.pnlEditTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditTop.Name = "pnlEditTop";
            this.pnlEditTop.Size = new System.Drawing.Size(600, 102);
            //
            // lblDirector
            //
            this.lblDirector.AutoSize = true;
            this.lblDirector.ForeColor = System.Drawing.Color.DimGray;
            this.lblDirector.Location = new System.Drawing.Point(6, 73);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Text = "Director";
            //
            // cmbDirector
            //
            this.cmbDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirector.Location = new System.Drawing.Point(66, 69);
            this.cmbDirector.Name = "cmbDirector";
            this.cmbDirector.Size = new System.Drawing.Size(180, 25);
            //
            // btnDirStart
            //
            this.btnDirStart.Location = new System.Drawing.Point(252, 67);
            this.btnDirStart.Name = "btnDirStart";
            this.btnDirStart.Size = new System.Drawing.Size(110, 29);
            this.btnDirStart.Text = "Baştan Oynat";
            this.btnDirStart.UseVisualStyleBackColor = true;
            this.btnDirStart.Click += new System.EventHandler(this.btnDirStart_Click);
            //
            // btnDirContinue
            //
            this.btnDirContinue.Location = new System.Drawing.Point(368, 67);
            this.btnDirContinue.Name = "btnDirContinue";
            this.btnDirContinue.Size = new System.Drawing.Size(110, 29);
            this.btnDirContinue.Text = "Devam Et";
            this.btnDirContinue.UseVisualStyleBackColor = true;
            this.btnDirContinue.Click += new System.EventHandler(this.btnDirContinue_Click);
            //
            // btnVer
            //
            this.btnVer.BackColor = System.Drawing.Color.FromArgb(214, 240, 214);
            this.btnVer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVer.ForeColor = System.Drawing.Color.FromArgb(20, 90, 20);
            this.btnVer.Location = new System.Drawing.Point(6, 6);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(120, 32);
            this.btnVer.Text = "VER  (Ctrl+Q)";
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            //
            // btnGuncelle
            //
            this.btnGuncelle.Location = new System.Drawing.Point(132, 6);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(130, 32);
            this.btnGuncelle.Text = "Güncelle  (Ctrl+G)";
            this.btnGuncelle.UseVisualStyleBackColor = true;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            //
            // btnAl
            //
            this.btnAl.BackColor = System.Drawing.Color.FromArgb(218, 226, 244);
            this.btnAl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAl.ForeColor = System.Drawing.Color.FromArgb(30, 50, 110);
            this.btnAl.Location = new System.Drawing.Point(268, 6);
            this.btnAl.Name = "btnAl";
            this.btnAl.Size = new System.Drawing.Size(120, 32);
            this.btnAl.Text = "AL  (Ctrl+W)";
            this.btnAl.UseVisualStyleBackColor = false;
            this.btnAl.Click += new System.EventHandler(this.btnAl_Click);
            //
            // lblSceneInfo
            //
            this.lblSceneInfo.AutoSize = true;
            this.lblSceneInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblSceneInfo.Location = new System.Drawing.Point(6, 46);
            this.lblSceneInfo.Name = "lblSceneInfo";
            this.lblSceneInfo.Text = "Sahne seçilmedi";
            //
            // dgvValues
            //
            this.dgvValues.AllowUserToAddRows = false;
            this.dgvValues.AllowUserToDeleteRows = false;
            this.dgvValues.AllowUserToResizeRows = false;
            this.dgvValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvValues.BackgroundColor = System.Drawing.Color.White;
            this.dgvValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvValues.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(236, 237, 244);
            headerStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(60, 60, 90);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(236, 237, 244);
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(60, 60, 90);
            this.dgvValues.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAlan,
            this.colDeger});
            this.dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvValues.EnableHeadersVisualStyles = false;
            this.dgvValues.GridColor = System.Drawing.Color.FromArgb(225, 225, 232);
            this.dgvValues.MultiSelect = false;
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.RowHeadersVisible = false;
            this.dgvValues.RowTemplate.Height = 28;
            //
            // colAlan
            //
            this.colAlan.FillWeight = 30F;
            this.colAlan.HeaderText = "Alan";
            this.colAlan.Name = "colAlan";
            this.colAlan.ReadOnly = true;
            //
            // colDeger
            //
            this.colDeger.FillWeight = 70F;
            this.colDeger.HeaderText = "Değer";
            this.colDeger.Name = "colDeger";
            //
            // lblState
            //
            this.lblState.BackColor = System.Drawing.Color.FromArgb(248, 248, 252);
            this.lblState.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblState.ForeColor = System.Drawing.Color.DimGray;
            this.lblState.Name = "lblState";
            this.lblState.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblState.Size = new System.Drawing.Size(600, 26);
            this.lblState.Text = "Yapılandırılmadı";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // split
            //
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.split.Panel1.Controls.Add(this.lstScenes);
            this.split.Panel1.Controls.Add(this.pnlTop);
            this.split.Panel1.Controls.Add(this.lblHeader);
            this.split.Panel2.Controls.Add(this.dgvValues);
            this.split.Panel2.Controls.Add(this.pnlEditTop);
            this.split.Panel2.Controls.Add(this.lblEditHeader);
            this.split.Size = new System.Drawing.Size(600, 700);
            this.split.SplitterDistance = 360;
            //
            // timerUi
            //
            this.timerUi.Enabled = true;
            this.timerUi.Interval = 200;
            this.timerUi.Tick += new System.EventHandler(this.timerUi_Tick);
            //
            // KelebekPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.split);
            this.Controls.Add(this.lblState);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "KelebekPanel";
            this.Size = new System.Drawing.Size(600, 726);
            this.pnlTop.ResumeLayout(false);
            this.pnlEditTop.ResumeLayout(false);
            this.pnlEditTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
