namespace GözHastaneOtomasyonu
{
    partial class FrmHastaKayit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblTC = new DevExpress.XtraEditors.LabelControl();
            this.LblAdSoyad = new DevExpress.XtraEditors.LabelControl();
            this.TxtTelefon = new DevExpress.XtraEditors.TextEdit();
            this.LblTelefon = new DevExpress.XtraEditors.LabelControl();
            this.LblAdres = new DevExpress.XtraEditors.LabelControl();
            this.TxtAdSoyad = new DevExpress.XtraEditors.TextEdit();
            this.TxtTC = new DevExpress.XtraEditors.TextEdit();
            this.TxtAdres = new DevExpress.XtraEditors.MemoEdit();
            this.BtnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.TxtCinsiyet = new DevExpress.XtraEditors.ComboBoxEdit();
            this.LblCinsiyet = new DevExpress.XtraEditors.LabelControl();
            this.LblDogumTarihi = new DevExpress.XtraEditors.LabelControl();
            this.TxtDogumTarihi = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTelefon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAdSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAdres.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCinsiyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDogumTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDogumTarihi.Properties.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTC
            // 
            this.LblTC.Location = new System.Drawing.Point(33, 74);
            this.LblTC.Name = "LblTC";
            this.LblTC.Size = new System.Drawing.Size(77, 16);
            this.LblTC.TabIndex = 0;
            this.LblTC.Text = "TC Kimlik No:";
            // 
            // LblAdSoyad
            // 
            this.LblAdSoyad.Location = new System.Drawing.Point(33, 113);
            this.LblAdSoyad.Name = "LblAdSoyad";
            this.LblAdSoyad.Size = new System.Drawing.Size(59, 16);
            this.LblAdSoyad.TabIndex = 1;
            this.LblAdSoyad.Text = "Ad Soyad:";
            // 
            // TxtTelefon
            // 
            this.TxtTelefon.Location = new System.Drawing.Point(138, 148);
            this.TxtTelefon.Name = "TxtTelefon";
            this.TxtTelefon.Size = new System.Drawing.Size(216, 22);
            this.TxtTelefon.TabIndex = 2;
            // 
            // LblTelefon
            // 
            this.LblTelefon.Location = new System.Drawing.Point(33, 151);
            this.LblTelefon.Name = "LblTelefon";
            this.LblTelefon.Size = new System.Drawing.Size(48, 16);
            this.LblTelefon.TabIndex = 4;
            this.LblTelefon.Text = "Telefon:";
            // 
            // LblAdres
            // 
            this.LblAdres.Location = new System.Drawing.Point(33, 184);
            this.LblAdres.Name = "LblAdres";
            this.LblAdres.Size = new System.Drawing.Size(38, 16);
            this.LblAdres.TabIndex = 5;
            this.LblAdres.Text = "Adres:";
            // 
            // TxtAdSoyad
            // 
            this.TxtAdSoyad.Location = new System.Drawing.Point(138, 110);
            this.TxtAdSoyad.Name = "TxtAdSoyad";
            this.TxtAdSoyad.Size = new System.Drawing.Size(216, 22);
            this.TxtAdSoyad.TabIndex = 7;
            // 
            // TxtTC
            // 
            this.TxtTC.Location = new System.Drawing.Point(138, 71);
            this.TxtTC.Name = "TxtTC";
            this.TxtTC.Size = new System.Drawing.Size(216, 22);
            this.TxtTC.TabIndex = 8;
            // 
            // TxtAdres
            // 
            this.TxtAdres.Location = new System.Drawing.Point(138, 185);
            this.TxtAdres.Name = "TxtAdres";
            this.TxtAdres.Size = new System.Drawing.Size(329, 131);
            this.TxtAdres.TabIndex = 9;
            // 
            // BtnKaydet
            // 
            this.BtnKaydet.Location = new System.Drawing.Point(162, 340);
            this.BtnKaydet.Name = "BtnKaydet";
            this.BtnKaydet.Size = new System.Drawing.Size(132, 56);
            this.BtnKaydet.TabIndex = 10;
            this.BtnKaydet.Text = "Kaydet";
            this.BtnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // TxtCinsiyet
            // 
            this.TxtCinsiyet.Location = new System.Drawing.Point(582, 68);
            this.TxtCinsiyet.Name = "TxtCinsiyet";
            this.TxtCinsiyet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TxtCinsiyet.Properties.Items.AddRange(new object[] {
            "KADIN",
            "ERKEK"});
            this.TxtCinsiyet.Size = new System.Drawing.Size(125, 22);
            this.TxtCinsiyet.TabIndex = 11;
            // 
            // LblCinsiyet
            // 
            this.LblCinsiyet.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblCinsiyet.Appearance.Options.UseFont = true;
            this.LblCinsiyet.Location = new System.Drawing.Point(504, 71);
            this.LblCinsiyet.Name = "LblCinsiyet";
            this.LblCinsiyet.Size = new System.Drawing.Size(49, 16);
            this.LblCinsiyet.TabIndex = 12;
            this.LblCinsiyet.Text = "Cinsiyet:";
            // 
            // LblDogumTarihi
            // 
            this.LblDogumTarihi.Location = new System.Drawing.Point(471, 119);
            this.LblDogumTarihi.Name = "LblDogumTarihi";
            this.LblDogumTarihi.Size = new System.Drawing.Size(82, 16);
            this.LblDogumTarihi.TabIndex = 13;
            this.LblDogumTarihi.Text = "Doğum Tarihi:";
            // 
            // TxtDogumTarihi
            // 
            this.TxtDogumTarihi.EditValue = null;
            this.TxtDogumTarihi.Location = new System.Drawing.Point(582, 113);
            this.TxtDogumTarihi.Name = "TxtDogumTarihi";
            this.TxtDogumTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TxtDogumTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TxtDogumTarihi.Size = new System.Drawing.Size(125, 22);
            this.TxtDogumTarihi.TabIndex = 14;
            // 
            // FrmHastaKayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TxtDogumTarihi);
            this.Controls.Add(this.LblDogumTarihi);
            this.Controls.Add(this.LblCinsiyet);
            this.Controls.Add(this.TxtCinsiyet);
            this.Controls.Add(this.BtnKaydet);
            this.Controls.Add(this.TxtAdres);
            this.Controls.Add(this.TxtTC);
            this.Controls.Add(this.TxtAdSoyad);
            this.Controls.Add(this.LblAdres);
            this.Controls.Add(this.LblTelefon);
            this.Controls.Add(this.TxtTelefon);
            this.Controls.Add(this.LblAdSoyad);
            this.Controls.Add(this.LblTC);
            this.Name = "FrmHastaKayit";
            this.Text = "FrmHastaKayit";
            this.Load += new System.EventHandler(this.FrmHastaKayit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TxtTelefon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAdSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtAdres.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtCinsiyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDogumTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtDogumTarihi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblTC;
        private DevExpress.XtraEditors.LabelControl LblAdSoyad;
        private DevExpress.XtraEditors.TextEdit TxtTelefon;
        private DevExpress.XtraEditors.LabelControl LblTelefon;
        private DevExpress.XtraEditors.LabelControl LblAdres;
        private DevExpress.XtraEditors.TextEdit TxtAdSoyad;
        private DevExpress.XtraEditors.TextEdit TxtTC;
        private DevExpress.XtraEditors.MemoEdit TxtAdres;
        private DevExpress.XtraEditors.SimpleButton BtnKaydet;
        private DevExpress.XtraEditors.ComboBoxEdit TxtCinsiyet;
        private DevExpress.XtraEditors.LabelControl LblCinsiyet;
        private DevExpress.XtraEditors.LabelControl LblDogumTarihi;
        private DevExpress.XtraEditors.DateEdit TxtDogumTarihi;
    }
}