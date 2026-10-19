namespace SHOW_TV_ANAHABER.Ui
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpEngine;
        private System.Windows.Forms.Label lblEngineHost;
        private System.Windows.Forms.TextBox txtEngineHost;
        private System.Windows.Forms.NumericUpDown numEnginePort;
        private System.Windows.Forms.Label lblKelebek1;
        private System.Windows.Forms.TextBox txtKelebek1Host;
        private System.Windows.Forms.NumericUpDown numKelebek1Port;
        private System.Windows.Forms.Label lblKelebek2;
        private System.Windows.Forms.TextBox txtKelebek2Host;
        private System.Windows.Forms.NumericUpDown numKelebek2Port;

        private System.Windows.Forms.GroupBox grpProgram;
        private System.Windows.Forms.Label lblControllerName;
        private System.Windows.Forms.TextBox txtControllerName;
        private System.Windows.Forms.Label lblKelebekPlaylist1;
        private System.Windows.Forms.NumericUpDown numKelebekPlaylist1;
        private System.Windows.Forms.Label lblKelebekPlaylist2;
        private System.Windows.Forms.NumericUpDown numKelebekPlaylist2;
        private System.Windows.Forms.Label lblKelebekGuiDir;
        private System.Windows.Forms.NumericUpDown numKelebekGuiDir;

        private System.Windows.Forms.GroupBox grpDb;
        private System.Windows.Forms.Label lblDbNote;
        private System.Windows.Forms.Label lblVizrtDb;
        private System.Windows.Forms.TextBox txtVizrtDb;
        private System.Windows.Forms.Label lblNewsDb;
        private System.Windows.Forms.TextBox txtNewsDb;

        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpEngine = new System.Windows.Forms.GroupBox();
            this.lblEngineHost = new System.Windows.Forms.Label();
            this.txtEngineHost = new System.Windows.Forms.TextBox();
            this.numEnginePort = new System.Windows.Forms.NumericUpDown();
            this.lblKelebek1 = new System.Windows.Forms.Label();
            this.txtKelebek1Host = new System.Windows.Forms.TextBox();
            this.numKelebek1Port = new System.Windows.Forms.NumericUpDown();
            this.lblKelebek2 = new System.Windows.Forms.Label();
            this.txtKelebek2Host = new System.Windows.Forms.TextBox();
            this.numKelebek2Port = new System.Windows.Forms.NumericUpDown();
            this.grpProgram = new System.Windows.Forms.GroupBox();
            this.lblControllerName = new System.Windows.Forms.Label();
            this.txtControllerName = new System.Windows.Forms.TextBox();
            this.lblKelebekPlaylist1 = new System.Windows.Forms.Label();
            this.numKelebekPlaylist1 = new System.Windows.Forms.NumericUpDown();
            this.lblKelebekPlaylist2 = new System.Windows.Forms.Label();
            this.numKelebekPlaylist2 = new System.Windows.Forms.NumericUpDown();
            this.lblKelebekGuiDir = new System.Windows.Forms.Label();
            this.numKelebekGuiDir = new System.Windows.Forms.NumericUpDown();
            this.grpDb = new System.Windows.Forms.GroupBox();
            this.lblDbNote = new System.Windows.Forms.Label();
            this.lblVizrtDb = new System.Windows.Forms.Label();
            this.txtVizrtDb = new System.Windows.Forms.TextBox();
            this.lblNewsDb = new System.Windows.Forms.Label();
            this.txtNewsDb = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpEngine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnginePort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebek1Port)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebek2Port)).BeginInit();
            this.grpProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebekPlaylist1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebekPlaylist2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebekGuiDir)).BeginInit();
            this.grpDb.SuspendLayout();
            this.SuspendLayout();
            //
            // grpEngine
            //
            this.grpEngine.Controls.Add(this.lblEngineHost);
            this.grpEngine.Controls.Add(this.txtEngineHost);
            this.grpEngine.Controls.Add(this.numEnginePort);
            this.grpEngine.Controls.Add(this.lblKelebek1);
            this.grpEngine.Controls.Add(this.txtKelebek1Host);
            this.grpEngine.Controls.Add(this.numKelebek1Port);
            this.grpEngine.Controls.Add(this.lblKelebek2);
            this.grpEngine.Controls.Add(this.txtKelebek2Host);
            this.grpEngine.Controls.Add(this.numKelebek2Port);
            this.grpEngine.Location = new System.Drawing.Point(12, 12);
            this.grpEngine.Name = "grpEngine";
            this.grpEngine.Size = new System.Drawing.Size(560, 130);
            this.grpEngine.TabStop = false;
            this.grpEngine.Text = "Viz Engine makineleri (adres, port)";
            //
            // lblEngineHost
            //
            this.lblEngineHost.AutoSize = true;
            this.lblEngineHost.Location = new System.Drawing.Point(12, 30);
            this.lblEngineHost.Name = "lblEngineHost";
            this.lblEngineHost.Text = "Ana reji Engine";
            //
            // txtEngineHost
            //
            this.txtEngineHost.Location = new System.Drawing.Point(180, 27);
            this.txtEngineHost.Name = "txtEngineHost";
            this.txtEngineHost.Size = new System.Drawing.Size(260, 25);
            //
            // numEnginePort
            //
            this.numEnginePort.Location = new System.Drawing.Point(450, 27);
            this.numEnginePort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numEnginePort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numEnginePort.Name = "numEnginePort";
            this.numEnginePort.Size = new System.Drawing.Size(90, 25);
            this.numEnginePort.Value = new decimal(new int[] { 6100, 0, 0, 0 });
            //
            // lblKelebek1
            //
            this.lblKelebek1.AutoSize = true;
            this.lblKelebek1.Location = new System.Drawing.Point(12, 64);
            this.lblKelebek1.Name = "lblKelebek1";
            this.lblKelebek1.Text = "Kelebek hedefi 1 (Viz #1)";
            //
            // txtKelebek1Host
            //
            this.txtKelebek1Host.Location = new System.Drawing.Point(180, 61);
            this.txtKelebek1Host.Name = "txtKelebek1Host";
            this.txtKelebek1Host.Size = new System.Drawing.Size(260, 25);
            //
            // numKelebek1Port
            //
            this.numKelebek1Port.Location = new System.Drawing.Point(450, 61);
            this.numKelebek1Port.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numKelebek1Port.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numKelebek1Port.Name = "numKelebek1Port";
            this.numKelebek1Port.Size = new System.Drawing.Size(90, 25);
            this.numKelebek1Port.Value = new decimal(new int[] { 6100, 0, 0, 0 });
            //
            // lblKelebek2
            //
            this.lblKelebek2.AutoSize = true;
            this.lblKelebek2.Location = new System.Drawing.Point(12, 98);
            this.lblKelebek2.Name = "lblKelebek2";
            this.lblKelebek2.Text = "Kelebek hedefi 2 (Viz #2)";
            //
            // txtKelebek2Host
            //
            this.txtKelebek2Host.Location = new System.Drawing.Point(180, 95);
            this.txtKelebek2Host.Name = "txtKelebek2Host";
            this.txtKelebek2Host.Size = new System.Drawing.Size(260, 25);
            //
            // numKelebek2Port
            //
            this.numKelebek2Port.Location = new System.Drawing.Point(450, 95);
            this.numKelebek2Port.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numKelebek2Port.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numKelebek2Port.Name = "numKelebek2Port";
            this.numKelebek2Port.Size = new System.Drawing.Size(90, 25);
            this.numKelebek2Port.Value = new decimal(new int[] { 6100, 0, 0, 0 });
            //
            // grpProgram
            //
            this.grpProgram.Controls.Add(this.lblControllerName);
            this.grpProgram.Controls.Add(this.txtControllerName);
            this.grpProgram.Controls.Add(this.lblKelebekPlaylist1);
            this.grpProgram.Controls.Add(this.numKelebekPlaylist1);
            this.grpProgram.Controls.Add(this.lblKelebekPlaylist2);
            this.grpProgram.Controls.Add(this.numKelebekPlaylist2);
            this.grpProgram.Controls.Add(this.lblKelebekGuiDir);
            this.grpProgram.Controls.Add(this.numKelebekGuiDir);
            this.grpProgram.Location = new System.Drawing.Point(12, 150);
            this.grpProgram.Name = "grpProgram";
            this.grpProgram.Size = new System.Drawing.Size(560, 130);
            this.grpProgram.TabStop = false;
            this.grpProgram.Text = "Program modu ve kelebekler";
            //
            // lblControllerName
            //
            this.lblControllerName.AutoSize = true;
            this.lblControllerName.Location = new System.Drawing.Point(12, 30);
            this.lblControllerName.Name = "lblControllerName";
            this.lblControllerName.Text = "Kontrolcü adı (controller_name)";
            //
            // txtControllerName
            //
            this.txtControllerName.Location = new System.Drawing.Point(260, 27);
            this.txtControllerName.Name = "txtControllerName";
            this.txtControllerName.Size = new System.Drawing.Size(280, 25);
            //
            // lblKelebekPlaylist1
            //
            this.lblKelebekPlaylist1.AutoSize = true;
            this.lblKelebekPlaylist1.Location = new System.Drawing.Point(12, 64);
            this.lblKelebekPlaylist1.Name = "lblKelebekPlaylist1";
            this.lblKelebekPlaylist1.Text = "Kelebek playlist 1 / 2 (group_id)";
            //
            // numKelebekPlaylist1
            //
            this.numKelebekPlaylist1.Location = new System.Drawing.Point(260, 61);
            this.numKelebekPlaylist1.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numKelebekPlaylist1.Name = "numKelebekPlaylist1";
            this.numKelebekPlaylist1.Size = new System.Drawing.Size(90, 25);
            //
            // lblKelebekPlaylist2
            //
            this.lblKelebekPlaylist2.AutoSize = true;
            this.lblKelebekPlaylist2.Location = new System.Drawing.Point(360, 64);
            this.lblKelebekPlaylist2.Name = "lblKelebekPlaylist2";
            this.lblKelebekPlaylist2.Text = "/";
            //
            // numKelebekPlaylist2
            //
            this.numKelebekPlaylist2.Location = new System.Drawing.Point(380, 61);
            this.numKelebekPlaylist2.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numKelebekPlaylist2.Name = "numKelebekPlaylist2";
            this.numKelebekPlaylist2.Size = new System.Drawing.Size(90, 25);
            //
            // lblKelebekGuiDir
            //
            this.lblKelebekGuiDir.AutoSize = true;
            this.lblKelebekGuiDir.Location = new System.Drawing.Point(12, 98);
            this.lblKelebekGuiDir.Name = "lblKelebekGuiDir";
            this.lblKelebekGuiDir.Text = "Kelebek sahne kategorisi (dir_id)";
            //
            // numKelebekGuiDir
            //
            this.numKelebekGuiDir.Location = new System.Drawing.Point(260, 95);
            this.numKelebekGuiDir.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numKelebekGuiDir.Name = "numKelebekGuiDir";
            this.numKelebekGuiDir.Size = new System.Drawing.Size(90, 25);
            //
            // grpDb
            //
            this.grpDb.Controls.Add(this.lblDbNote);
            this.grpDb.Controls.Add(this.lblVizrtDb);
            this.grpDb.Controls.Add(this.txtVizrtDb);
            this.grpDb.Controls.Add(this.lblNewsDb);
            this.grpDb.Controls.Add(this.txtNewsDb);
            this.grpDb.Location = new System.Drawing.Point(12, 288);
            this.grpDb.Name = "grpDb";
            this.grpDb.Size = new System.Drawing.Size(560, 170);
            this.grpDb.TabStop = false;
            this.grpDb.Text = "Veritabanı (yalnızca SELECT yetkili hesap)";
            //
            // lblDbNote
            //
            this.lblDbNote.ForeColor = System.Drawing.Color.DimGray;
            this.lblDbNote.Location = new System.Drawing.Point(12, 24);
            this.lblDbNote.Name = "lblDbNote";
            this.lblDbNote.Size = new System.Drawing.Size(536, 36);
            this.lblDbNote.Text = "Örnek: server=HOST;port=3306;user id=KULLANICI;password=SIFRE;database=VERITABANI;charset=utf8;\r\nBoş bırakılırsa uygulama veritabanı olmadan çalışır.";
            //
            // lblVizrtDb
            //
            this.lblVizrtDb.AutoSize = true;
            this.lblVizrtDb.Location = new System.Drawing.Point(12, 70);
            this.lblVizrtDb.Name = "lblVizrtDb";
            this.lblVizrtDb.Text = "Sahne tanımları veritabanı";
            //
            // txtVizrtDb
            //
            this.txtVizrtDb.Location = new System.Drawing.Point(12, 90);
            this.txtVizrtDb.Name = "txtVizrtDb";
            this.txtVizrtDb.Size = new System.Drawing.Size(536, 25);
            //
            // lblNewsDb
            //
            this.lblNewsDb.AutoSize = true;
            this.lblNewsDb.Location = new System.Drawing.Point(12, 120);
            this.lblNewsDb.Name = "lblNewsDb";
            this.lblNewsDb.Text = "Haber veritabanı";
            //
            // txtNewsDb
            //
            this.txtNewsDb.Location = new System.Drawing.Point(12, 140);
            this.txtNewsDb.Name = "txtNewsDb";
            this.txtNewsDb.Size = new System.Drawing.Size(536, 25);
            //
            // lblFile
            //
            this.lblFile.AutoSize = true;
            this.lblFile.ForeColor = System.Drawing.Color.DimGray;
            this.lblFile.Location = new System.Drawing.Point(12, 470);
            this.lblFile.Name = "lblFile";
            this.lblFile.Text = "Dosya:";
            //
            // btnOk
            //
            this.btnOk.Location = new System.Drawing.Point(372, 496);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 32);
            this.btnOk.Text = "Kaydet";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(478, 496);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 32);
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // SettingsForm
            //
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 540);
            this.Controls.Add(this.grpEngine);
            this.Controls.Add(this.grpProgram);
            this.Controls.Add(this.grpDb);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar";
            this.grpEngine.ResumeLayout(false);
            this.grpEngine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnginePort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebek1Port)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebek2Port)).EndInit();
            this.grpProgram.ResumeLayout(false);
            this.grpProgram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebekPlaylist1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebekPlaylist2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKelebekGuiDir)).EndInit();
            this.grpDb.ResumeLayout(false);
            this.grpDb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
