using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GözHastaneOtomasyonu
{
    public partial class FrmGiris : Form
    {
        Panel panelKart;
        LabelControl lblBaslik;
        LabelControl lblAltBaslik;

        SimpleButton btnGiris;
        SimpleButton btnKayitOl;
        SimpleButton btnSifreGoster;

        bool sifreGizli = true;

        public FrmGiris()
        {
            InitializeComponent();
            FormAyarla();
            KartOlustur();
            BaslikOlustur();
            AlanlariOlustur();
            ButonlariOlustur();

            this.Resize += (s, e) =>
            {
                KartOrtala();
                BaslikOrtala();
            };
        }

        // ================= FORM =================
        void FormAyarla()
        {
            this.Text = "Göz Hastanesi Otomasyonu | Giriş";
            this.BackColor = ColorTranslator.FromHtml("#BDE8F5");
            this.ClientSize = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.FormBorderStyle = FormBorderStyle.Sizable;
this.MaximizeBox = true;
this.MinimizeBox = true;

        }

        // ================= KART =================
        void KartOlustur()
        {
            panelKart = new Panel();
            panelKart.Size = new Size(420, 330);
            panelKart.BackColor = Color.White;
            panelKart.BorderStyle = BorderStyle.FixedSingle;
            panelKart.Parent = this;

            KartOrtala();
        }

        void KartOrtala()
        {
            panelKart.Left = (this.ClientSize.Width - panelKart.Width) / 2;
            panelKart.Top = (this.ClientSize.Height - panelKart.Height) / 2;
        }

        // ================= BAŞLIK =================
        void BaslikOlustur()
        {
            lblBaslik = new LabelControl();
            lblBaslik.Text = "EyeNova Göz";
            lblBaslik.Appearance.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBaslik.Appearance.ForeColor = ColorTranslator.FromHtml("#0F2854");
            lblBaslik.Parent = this;

            lblAltBaslik = new LabelControl();
            lblAltBaslik.Text = "Akıllı Teknoloji, Net Tanı, Kusursuz Görüş ";
            lblAltBaslik.Appearance.Font = new Font("Segoe UI", 11);
            lblAltBaslik.Appearance.ForeColor = ColorTranslator.FromHtml("#1C4D8D");
            lblAltBaslik.Parent = this;

            lblBaslik.Left = (this.ClientSize.Width - lblBaslik.Width) / 2;
            lblBaslik.Top = panelKart.Top - 90;

            lblAltBaslik.Left = (this.ClientSize.Width - lblAltBaslik.Width) / 2;
            lblAltBaslik.Top = lblBaslik.Bottom + 5;
        }

        // ================= ALANLAR =================
        void AlanlariOlustur()
        {
            int y = 40;

            // Kullanıcı Adı
            LabelControl lblKullanici = new LabelControl();
            lblKullanici.Text = "Kullanıcı Adı";
            lblKullanici.Location = new Point(40, y);
            lblKullanici.Parent = panelKart;
            lblKullanici.Appearance.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblKullanici.Appearance.ForeColor = ColorTranslator.FromHtml("#0F2854");

            TxtKullaniciAdi.Parent = panelKart;
            TxtKullaniciAdi.Location = new Point(40, y + 20);
            TxtKullaniciAdi.Size = new Size(300, 28);
            TxtKullaniciAdi.Properties.NullValuePrompt = "kullaniciadi";
            TxtKullaniciAdi.Properties.Appearance.Font = new Font("Segoe UI", 10);

            y += 70;

            // Şifre
            LabelControl lblSifre = new LabelControl();
            lblSifre.Text = "Şifre";
            lblSifre.Location = new Point(40, y);
            lblSifre.Parent = panelKart;
            lblSifre.Appearance.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSifre.Appearance.ForeColor = ColorTranslator.FromHtml("#0F2854");

            TxtSifre.Parent = panelKart;
            TxtSifre.Location = new Point(40, y + 20);
            TxtSifre.Size = new Size(260, 28);
            TxtSifre.Properties.UseSystemPasswordChar = true;
            TxtSifre.Properties.NullValuePrompt = "••••••";
            TxtSifre.Properties.Appearance.Font = new Font("Segoe UI", 10);

            // Şifre Göz
            btnSifreGoster = new SimpleButton();
            btnSifreGoster.Text = "👁";
            btnSifreGoster.Size = new Size(35, 28);
            btnSifreGoster.Location = new Point(TxtSifre.Right + 5, TxtSifre.Top);
            btnSifreGoster.Parent = panelKart;

            btnSifreGoster.Click += (s, e) =>
            {
                sifreGizli = !sifreGizli;
                TxtSifre.Properties.UseSystemPasswordChar = sifreGizli;
                btnSifreGoster.Text = sifreGizli ? "👁" : "🙈";
            };
        }

        // ================= BUTONLAR =================
        void ButonlariOlustur()
        {
            // Giriş Yap
            btnGiris = new SimpleButton();
            btnGiris.Text = "GİRİŞ YAP";
            btnGiris.Size = new Size(300, 45);
            btnGiris.Location = new Point(40, 190);
            btnGiris.Parent = panelKart;

            btnGiris.Appearance.BackColor = ColorTranslator.FromHtml("#4988C4");
            btnGiris.Appearance.ForeColor = Color.White;
            btnGiris.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnGiris.Appearance.Options.UseBackColor = true;
            btnGiris.Appearance.Options.UseForeColor = true;
            btnGiris.Appearance.Options.UseFont = true;

            btnGiris.Click += BtnGirisYap_Click;

            // Kayıt Ol
            btnKayitOl = new SimpleButton();
            btnKayitOl.Text = "Yeni Hasta Kaydı Oluştur";
            btnKayitOl.Size = new Size(300, 35);
            btnKayitOl.Location = new Point(40, 245);
            btnKayitOl.Parent = panelKart;

            btnKayitOl.Appearance.BackColor = ColorTranslator.FromHtml("#1C4D8D");
            btnKayitOl.Appearance.ForeColor = Color.White;
            btnKayitOl.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnKayitOl.Appearance.Options.UseBackColor = true;
            btnKayitOl.Appearance.Options.UseForeColor = true;

            btnKayitOl.Click += (s, e) =>
            {
                FrmHastaKayit frm = new FrmHastaKayit();
                frm.ShowDialog();
            };
        }

        // ================= GİRİŞ =================
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                SqlCommand cmd = new SqlCommand(
                    "SELECT KullaniciRol FROM Kullanicilar WHERE KullaniciAd=@u AND Sifre=@s",
                    SQLBaglantisi.baglanti);

                cmd.Parameters.AddWithValue("@u", TxtKullaniciAdi.Text.Trim());
                cmd.Parameters.AddWithValue("@s", TxtSifre.Text);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    SQLBaglantisi.AktifKullaniciRolu = dr["KullaniciRol"].ToString();
                    SQLBaglantisi.AktifKullaniciAdi = TxtKullaniciAdi.Text.Trim();

                    FrmAnaModul ana = new FrmAnaModul();
                    ana.Show();
                    this.Hide();
                }
                else
                {
                    XtraMessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Uyarı");
                }

                dr.Close();
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }
        void BaslikOrtala()
        {
            int ustBosluk = this.ClientSize.Height / 8;

            lblBaslik.Left = (this.ClientSize.Width - lblBaslik.Width) / 2;
            lblBaslik.Top = ustBosluk;

            lblAltBaslik.Left = (this.ClientSize.Width - lblAltBaslik.Width) / 2;
            lblAltBaslik.Top = lblBaslik.Bottom + 5;
        }

    }
}
