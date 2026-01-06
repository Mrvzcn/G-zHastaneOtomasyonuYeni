using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmAnaModul : RibbonForm
    {
        BarButtonItem btnRandevuAl;
        BarButtonItem btnCikis;

        public FrmAnaModul()
        {
            InitializeComponent();
        }

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            // ======================
            // FORM & MDI AYARLARI
            // ======================
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = ColorTranslator.FromHtml("#BDE8F5");

            // ======================
            // DEVEXPRESS SKIN (RIBBON BURADAN RENK ALIR)
            // ======================
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
            UserLookAndFeel.Default.UseDefaultLookAndFeel = true;

            // ======================
            // MDI ARKA PLAN
            // ======================
            MdiArkaPlanDuzenle();

            // ======================
            // DİNAMİK BUTON
            // ======================
            RandevuAlButonuOlustur();
            CikisButonuOlustur();
        }

        // =======================
        // YETKİ MERKEZİ
        // =======================
        public bool YetkisiVar(string formAdi)
        {
            string rol = (SQLBaglantisi.AktifKullaniciRolu ?? "").Trim();

            if (rol == "Admin")
                return true;

            if (rol == "Hasta")
                return formAdi == "FrmHastaKayit"
                    || formAdi == "FrmRandevuAl"
                    || formAdi == "FrmHastaRandevularim";

            if (rol == "Doktor")
                return formAdi == "FrmHastaListesi"
                    || formAdi == "FrmRandevular";

            if (rol == "Sekreter")
                return formAdi == "FrmHastaKayit"
                    || formAdi == "FrmHastaListesi"
                    || formAdi == "FrmRandevular"
                    || formAdi == "FrmRandevuAl";

            return false;
        }

        // =======================
        // RANDEVU AL BUTONU (KODLA)
        // =======================
        void RandevuAlButonuOlustur()
        {
            btnRandevuAl = new BarButtonItem
            {
                Caption = "Randevu Al",
                Name = "BtnRandevuAl",
                Id = ribbon.Manager.GetNewItemId()
            };

            btnRandevuAl.ItemClick += BtnRandevuAl_ItemClick;
            ribbon.Items.Add(btnRandevuAl);

            RibbonPage page = ribbon.Pages[0];
            RibbonPageGroup group;

            if (page.Groups.Count > 0)
                group = page.Groups[0];
            else
            {
                group = new RibbonPageGroup("Randevu İşlemleri");
                page.Groups.Add(group);
            }

            group.ItemLinks.Add(btnRandevuAl);

            // ❌ Doktor görmesin
            if (SQLBaglantisi.AktifKullaniciRolu == "Doktor")
                btnRandevuAl.Visibility = BarItemVisibility.Never;
        }

        private void BtnRandevuAl_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!YetkisiVar("FrmRandevuAl"))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                return;
            }

            FrmRandevuAl fr = new FrmRandevuAl
            {
                MdiParent = this
            };
            fr.Show();
        }

        // =======================
        // DİĞER BUTONLAR
        // =======================
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!YetkisiVar("FrmHastaKayit"))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                return;
            }

            FrmHastaKayit fr = new FrmHastaKayit
            {
                MdiParent = this
            };
            fr.Show();
        }

        private void BtnHastaListesi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!YetkisiVar("FrmHastaListesi"))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                return;
            }

            FrmHastaListesi fr = new FrmHastaListesi
            {
                MdiParent = this
            };
            fr.Show();
        }

        private void BtnDoktorKayit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!YetkisiVar("FrmDoktorlar"))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                return;
            }

            FrmDoktorlar fr = new FrmDoktorlar
            {
                MdiParent = this
            };
            fr.Show();
        }

        private void BtnRandevuListesi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!YetkisiVar("FrmRandevular"))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                return;
            }

            FrmRandevular fr = new FrmRandevular
            {
                MdiParent = this
            };
            fr.Show();
        }

        private void BtnRandevularim_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!YetkisiVar("FrmHastaRandevularim"))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                return;
            }

            FrmHastaRandevularim frm = new FrmHastaRandevularim
            {
                MdiParent = this
            };
            frm.Show();
        }

        // =======================
        // MDI ARKA PLAN RENK
        // =======================
        void MdiArkaPlanDuzenle()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient mdi)
                {
                    mdi.BackColor = ColorTranslator.FromHtml("#BDE8F5");
                }
            }
        }
        void CikisButonuOlustur()
        {
            btnCikis = new BarButtonItem
            {
                Caption = "Çıkış Yap",
                Id = ribbon.Manager.GetNewItemId(),
                Alignment = BarItemLinkAlignment.Right
            };

            // 🎨 TASARIM
            btnCikis.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCikis.Appearance.ForeColor = Color.White;
            btnCikis.Appearance.Options.UseFont = true;
            btnCikis.Appearance.Options.UseForeColor = true;

            // 🔴 KIRMIZI TON (ÇIKIŞ VURGUSU)
            btnCikis.ItemAppearance.Normal.BackColor = ColorTranslator.FromHtml("#C62828");
            btnCikis.ItemAppearance.Hovered.BackColor = ColorTranslator.FromHtml("#D32F2F");
            btnCikis.ItemAppearance.Pressed.BackColor = ColorTranslator.FromHtml("#B71C1C");

            btnCikis.ItemAppearance.Normal.Options.UseBackColor = true;
            btnCikis.ItemAppearance.Hovered.Options.UseBackColor = true;
            btnCikis.ItemAppearance.Pressed.Options.UseBackColor = true;

            // 🖼️ İKON (DevExpress hazır ikon)
            btnCikis.ImageOptions.SvgImage = SvgImage.FromResources(
                "DevExpress.Images.Actions.Close.svg",
                typeof(FrmAnaModul).Assembly
            );

            btnCikis.ItemClick += BtnCikis_ItemClick;
            ribbon.Items.Add(btnCikis);

            // 📍 SAĞ ÜSTE EKLE
            RibbonPage page = ribbon.Pages[0];

            RibbonPageGroup cikisGroup = new RibbonPageGroup();
            cikisGroup.Alignment = RibbonPageGroupAlignment.Far;
            cikisGroup.AllowTextClipping = false;

            page.Groups.Add(cikisGroup);
            cikisGroup.ItemLinks.Add(btnCikis);
        }
        private void BtnCikis_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult sonuc = MessageBox.Show(
                "Çıkış yapmak istiyor musunuz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (sonuc == DialogResult.Yes)
            {
                SQLBaglantisi.AktifKullaniciAdi = null;
                SQLBaglantisi.AktifKullaniciRolu = null;

                FrmGiris frm = new FrmGiris();
                frm.Show();

                this.Close();
            }
        }

    }
}
