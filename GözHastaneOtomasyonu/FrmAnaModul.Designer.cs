namespace GözHastaneOtomasyonu
{
    partial class FrmAnaModul
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.BtnHastaKayit = new DevExpress.XtraBars.BarButtonItem();
            this.BtnDoktorKayit = new DevExpress.XtraBars.BarButtonItem();
            this.BtnRandevuListesi = new DevExpress.XtraBars.BarButtonItem();
            this.BtnHastaListesi = new DevExpress.XtraBars.BarButtonItem();
            this.BtnRandevularim = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.BtnHastaKayit,
            this.BtnDoktorKayit,
            this.BtnRandevuListesi,
            this.BtnHastaListesi,
            this.BtnRandevularim});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 5;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1036, 193);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // BtnHastaKayit
            // 
            this.BtnHastaKayit.Caption = "Hasta Kayıt";
            this.BtnHastaKayit.Id = 1;
            this.BtnHastaKayit.Name = "BtnHastaKayit";
            this.BtnHastaKayit.Tag = "HastaKayit";
            this.BtnHastaKayit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // BtnDoktorKayit
            // 
            this.BtnDoktorKayit.Caption = "Doktor Kayıt";
            this.BtnDoktorKayit.Id = 2;
            this.BtnDoktorKayit.Name = "BtnDoktorKayit";
            this.BtnDoktorKayit.Tag = "DoktorKayit";
            this.BtnDoktorKayit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnDoktorKayit_ItemClick);
            // 
            // BtnRandevuListesi
            // 
            this.BtnRandevuListesi.Caption = "Randevu Listesi";
            this.BtnRandevuListesi.Id = 3;
            this.BtnRandevuListesi.Name = "BtnRandevuListesi";
            this.BtnRandevuListesi.Tag = "RandevuListesi";
            this.BtnRandevuListesi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnRandevuListesi_ItemClick);
            // 
            // BtnHastaListesi
            // 
            this.BtnHastaListesi.Caption = "Hasta Listesi";
            this.BtnHastaListesi.Id = 4;
            this.BtnHastaListesi.Name = "BtnHastaListesi";
            this.BtnHastaListesi.Tag = "HastaListesi";
            this.BtnHastaListesi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnHastaListesi_ItemClick);
            // 
            // BtnRandevularim
            this.BtnRandevularim.Caption = "Randevularım";
            this.BtnRandevularim.Id = 6;
            this.BtnRandevularim.Name = "BtnRandevularim";
            this.BtnRandevularim.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnRandevularim_ItemClick);

            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Ana İşlemler";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnHastaKayit);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnDoktorKayit);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnRandevuListesi);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnHastaListesi);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnRandevularim);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Kayıt Yönetimi";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 627);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1036, 30);
            // 
            // FrmAnaModul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 657);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "FrmAnaModul";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "FrmAnaModul";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAnaModul_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem BtnHastaKayit;
        private DevExpress.XtraBars.BarButtonItem BtnDoktorKayit;
        private DevExpress.XtraBars.BarButtonItem BtnRandevuListesi;
        private DevExpress.XtraBars.BarButtonItem BtnHastaListesi;
        private DevExpress.XtraBars.BarButtonItem BtnRandevularim;

    }
}