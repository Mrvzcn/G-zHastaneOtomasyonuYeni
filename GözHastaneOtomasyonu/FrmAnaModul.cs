using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmAnaModul : RibbonForm
    {
        BarButtonItem btnRandevuAl;

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
    }
}
