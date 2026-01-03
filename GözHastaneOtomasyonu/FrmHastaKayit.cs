using GözHastaneOtomasyonu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmHastaKayit : Form
    {
        TextBox TxtKullaniciAd;
        TextBox TxtSifre;

        Label lblKullanici;
        Label lblSifre;

        Panel panelOrta;
        public FrmHastaKayit()
        {
            InitializeComponent();
            OlusturOrtaPanel();
            KullaniciAlanlariniOlustur();

            this.BackColor = Color.FromArgb(234, 246, 251); // Açık mavi arka plan

        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var ana = this.MdiParent as FrmAnaModul;
            if (ana != null && !ana.YetkisiVar(this.Name))
            {
                MessageBox.Show("Bu ekrana erişim yetkiniz yok.");
                Close();
            }
        }
        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {
            BtnKaydet.Location = new Point(
            TxtDogumTarihi.Left - 15,
            TxtDogumTarihi.Bottom + 35
   );

            BtnKaydet.Size = new Size(180, 45); 
            // DEVEXPRESS'E UYGUN RENK AYARLARI
            BtnKaydet.Appearance.BackColor = Color.Teal;
            BtnKaydet.Appearance.ForeColor = Color.White;
            BtnKaydet.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
            BtnKaydet.Appearance.Options.UseBackColor = true;
            BtnKaydet.Appearance.Options.UseForeColor = true;
            BtnKaydet.Appearance.Options.UseFont = true;

            this.Resize += (s, args) =>
            {
                OrtalaPanel();
            };
            // 🔴 TÜM KONTROLLERİ PANELE TAŞI (TEK TEK)

            // LABEL'lar
            panelOrta.Controls.Add(LblTC);
            panelOrta.Controls.Add(LblAdSoyad);
            panelOrta.Controls.Add(LblTelefon);
            panelOrta.Controls.Add(LblAdres);
            panelOrta.Controls.Add(LblCinsiyet);
            panelOrta.Controls.Add(LblDogumTarihi);
            panelOrta.Controls.Add(lblKullanici);
            panelOrta.Controls.Add(lblSifre);

            // TEXTBOX / CONTROLLER
            panelOrta.Controls.Add(TxtTC);
            panelOrta.Controls.Add(TxtAdSoyad);
            panelOrta.Controls.Add(TxtTelefon);
            panelOrta.Controls.Add(TxtAdres);
            panelOrta.Controls.Add(TxtDogumTarihi);
            panelOrta.Controls.Add(TxtCinsiyet);
            panelOrta.Controls.Add(TxtKullaniciAd);
            panelOrta.Controls.Add(TxtSifre);
            panelOrta.Controls.Add(BtnKaydet);

            // 🔴 PANELİ ORTALA
            OrtalaPanel();

            // 🔴 FORM BOYUTU DEĞİŞİNCE YENİDEN ORTALA
            this.Resize += (s, e2) => OrtalaPanel();
        

        }


        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                // 1️⃣ HASTA BİLGİLERİ
                SqlCommand komutHasta = new SqlCommand(
                    "INSERT INTO HastaBilgileri (TCKimlikNo, AdSoyad, Telefon, Adres, DogumTarihi, Cinsiyet, KullaniciAd) " +
                    "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7)",
                    SQLBaglantisi.baglanti);

                komutHasta.Parameters.AddWithValue("@p1", TxtTC.Text);
                komutHasta.Parameters.AddWithValue("@p2", TxtAdSoyad.Text);
                komutHasta.Parameters.AddWithValue("@p3", TxtTelefon.Text);
                komutHasta.Parameters.AddWithValue("@p4", TxtAdres.Text);
                komutHasta.Parameters.AddWithValue("@p5", TxtDogumTarihi.DateTime);
                komutHasta.Parameters.AddWithValue("@p6", TxtCinsiyet.Text);
                komutHasta.Parameters.AddWithValue("@p7", TxtKullaniciAd.Text);

                komutHasta.ExecuteNonQuery();

                // 2️⃣ KULLANICI BİLGİLERİ
                SqlCommand komutKullanici = new SqlCommand(
                    "INSERT INTO Kullanicilar (KullaniciAd, Sifre, KullaniciRol) " +
                    "VALUES (@k1,@k2,@k3)",
                    SQLBaglantisi.baglanti);

                komutKullanici.Parameters.AddWithValue("@k1", TxtKullaniciAd.Text);
                komutKullanici.Parameters.AddWithValue("@k2", TxtSifre.Text);
                komutKullanici.Parameters.AddWithValue("@k3", "Hasta");

                komutKullanici.ExecuteNonQuery();

                MessageBox.Show("Hasta ve kullanıcı kaydı başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }

        // Bu metot, kayıt işleminden sonra tüm kutuları boşaltır
        void Temizle()
            {
                TxtTC.Text = "";
                TxtAdSoyad.Text = "";
                TxtTelefon.Text = "";
                TxtAdres.Text = "";
                TxtDogumTarihi.Text = ""; // DateEdit için
                TxtCinsiyet.Text = "";    // ComboBoxEdit için
                TxtTC.Focus();            // İmleci tekrar ilk kutuya (TC) odaklar
            }

        void KullaniciAlanlariniOlustur()
        {
            // Kullanıcı Adı Label
            lblKullanici = new System.Windows.Forms.Label();
            lblKullanici.Text = "Kullanıcı Adı:";
            lblKullanici.Location = new Point(30, 300);
            lblKullanici.AutoSize = true;
            panelOrta.Controls.Add(lblKullanici);

            // Kullanıcı Adı TextBox
            TxtKullaniciAd = new TextBox();
            TxtKullaniciAd.Location = new Point(150, 295);
            TxtKullaniciAd.Width = 200;
            panelOrta.Controls.Add(TxtKullaniciAd);

            // Şifre Label
            lblSifre = new System.Windows.Forms.Label();
            lblSifre.Text = "Şifre:";
            lblSifre.Location = new Point(30, 340);
            lblSifre.AutoSize = true;
            panelOrta.Controls.Add(lblSifre);

            // Şifre TextBox
            TxtSifre = new TextBox();
            TxtSifre.Location = new Point(150, 335);
            TxtSifre.Width = 200;
            TxtSifre.UseSystemPasswordChar = true;
            panelOrta.Controls.Add(TxtSifre);
        }

        void OlusturOrtaPanel()
        {
            panelOrta = new Panel();
            panelOrta.Size = new Size(700, 400);   // Hasta kayıt daha büyük
            panelOrta.BackColor = Color.White;     // 🔴 BEYAZ PANEL
            panelOrta.Parent = this;
            panelOrta.Padding = new Padding(15);

            panelOrta.BorderStyle = BorderStyle.FixedSingle;

            OrtalaPanel();
        }

        void OrtalaPanel()
        {
            panelOrta.Left = (this.ClientSize.Width - panelOrta.Width) / 2;
            panelOrta.Top = (this.ClientSize.Height - panelOrta.Height) / 2;
        }

    }
}

