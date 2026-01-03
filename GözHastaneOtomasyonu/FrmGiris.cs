using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GözHastaneOtomasyonu
{
    public partial class FrmGiris : Form
    {
        Panel panelOrta;

        SimpleButton btnSifreGoster;
        SimpleButton btnKayitOl;
        LabelControl lblBaslik;


        bool sifreGizli = true;

        public FrmGiris()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(234, 246, 251);

            OlusturOrtaPanel();
            BaslikYazisiOlustur();

            TxtSifre.Properties.UseSystemPasswordChar = true;
            SifreGozButonuOlustur();

            this.Load += FrmGiris_Load;

            this.Resize += (s, e) =>
            {
                OrtalaPanel();

                if (lblBaslik != null)
                    OrtalaBaslik();
            };
        }

        void OlusturOrtaPanel()
        {
            panelOrta = new Panel();
            panelOrta.Size = new Size(400, 260);
            panelOrta.BackColor = Color.White; // 🔴 BEYAZ PANEL
            panelOrta.Parent = this;
            panelOrta.Padding = new Padding(10);

            // Hafif çerçeve hissi
            panelOrta.BorderStyle = BorderStyle.FixedSingle;

            OrtalaPanel();
        }

        void OrtalaPanel()
        {
            panelOrta.Left = (this.ClientSize.Width - panelOrta.Width) / 2;
            panelOrta.Top = (this.ClientSize.Height - panelOrta.Height) / 2;
        }

        // ================= LOAD =================
        private void FrmGiris_Load(object sender, EventArgs e)
        {
            panelOrta.Controls.Clear();

            int xLabel = 30;
            int xInput = 140;
            int y = 35;

            // ---- Kullanıcı Adı ----
            LabelControl lblKullanici = new LabelControl();
            lblKullanici.Text = "Kullanıcı Adı:";
            lblKullanici.Location = new Point(xLabel, y);
            panelOrta.Controls.Add(lblKullanici);

            TxtKullaniciAdi.Parent = panelOrta;
            TxtKullaniciAdi.Location = new Point(xInput, y - 3);
            TxtKullaniciAdi.Width = 200;

            y += 45;

            // ---- Şifre ----
            LabelControl lblSifre = new LabelControl();
            lblSifre.Text = "Şifre:";
            lblSifre.Location = new Point(xLabel, y);
            panelOrta.Controls.Add(lblSifre);

            TxtSifre.Parent = panelOrta;
            TxtSifre.Location = new Point(xInput, y - 3);
            TxtSifre.Width = 200;

            // Şifre göz butonu
            btnSifreGoster.Parent = panelOrta;
            btnSifreGoster.Location = new Point(TxtSifre.Right + 5, TxtSifre.Top);

            y += 55;

            // ---- Giriş Yap ----
            BtnGirisYap.Parent = panelOrta;
            BtnGirisYap.Text = "Giriş Yap";
            BtnGirisYap.Size = new Size(200, 40);
            BtnGirisYap.Location = new Point(xInput, y);

            y += 55;

            // ---- Yeni Hasta Kaydı ----
            btnKayitOl = new SimpleButton();
            btnKayitOl.Text = "Yeni Hasta Kaydı Oluştur";
            btnKayitOl.Size = new Size(200, 35);
            btnKayitOl.Location = new Point(xInput, y);
            btnKayitOl.Appearance.BackColor = Color.Teal;
            btnKayitOl.Appearance.ForeColor = Color.White;
            btnKayitOl.Appearance.Options.UseBackColor = true;
            btnKayitOl.Appearance.Options.UseForeColor = true;
            btnKayitOl.Parent = panelOrta;

            btnKayitOl.Click += (s, ev) =>
            {
                FrmHastaKayit frm = new FrmHastaKayit();
                frm.ShowDialog();
            };

            OrtalaPanel();
        }

        // ================= ŞİFRE GÖZ =================
        void SifreGozButonuOlustur()
        {
            btnSifreGoster = new SimpleButton();
            btnSifreGoster.Size = new Size(30, TxtSifre.Height);
            btnSifreGoster.Text = "👁";
            btnSifreGoster.Appearance.Font = new Font("Segoe UI", 10);
            btnSifreGoster.Cursor = Cursors.Hand;

            btnSifreGoster.Click += (s, e) =>
            {
                sifreGizli = !sifreGizli;
                TxtSifre.Properties.UseSystemPasswordChar = sifreGizli;
                btnSifreGoster.Text = sifreGizli ? "👁" : "🙈";
            };
        }

        // ================= GİRİŞ =================
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                SqlCommand komut = new SqlCommand(
                    "SELECT KullaniciRol FROM Kullanicilar WHERE KullaniciAd=@p1 AND Sifre=@p2",
                    SQLBaglantisi.baglanti);

                komut.Parameters.AddWithValue("@p1", TxtKullaniciAdi.Text.Trim());
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);

                SqlDataReader dr = komut.ExecuteReader();

                if (dr.Read())
                {
                    SQLBaglantisi.AktifKullaniciRolu =
                        dr["KullaniciRol"].ToString().Trim();

                    SQLBaglantisi.AktifKullaniciAdi =
                        TxtKullaniciAdi.Text.Trim();

                    FrmAnaModul ana = new FrmAnaModul();
                    ana.Show();
                    this.Hide();
                }
                else
                {
                    XtraMessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                }

                dr.Close();
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }
        void BaslikYazisiOlustur()
        {
            lblBaslik = new LabelControl();
            lblBaslik.Text = "Sağlıklı günler dileriz";
            lblBaslik.Appearance.Font = new Font("Tahoma", 26, FontStyle.Bold);
            lblBaslik.Appearance.ForeColor = Color.FromArgb(0, 102, 153); // koyu mavi
            lblBaslik.AutoSizeMode = LabelAutoSizeMode.Horizontal;

            lblBaslik.Parent = this;

            OrtalaBaslik();
        }
        void OrtalaBaslik()
        {
            lblBaslik.Left = (this.ClientSize.Width - lblBaslik.Width) / 2;
            lblBaslik.Top = panelOrta.Top - 60;
        }

    }
}
