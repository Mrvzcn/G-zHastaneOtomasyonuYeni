using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmRandevuAl :BaseForm
    {

        Panel panelOrta;
        TextEdit txtHastaTC;
        ComboBoxEdit cmbDoktor;
        DateEdit dateTarih;
        ComboBoxEdit cmbSaat;
        SimpleButton btnKaydet;

        public int SeciliRandevuID { get; set; } = 0;


        public FrmRandevuAl()
        {
            InitializeComponent();
            FormuOlustur();
            DoktorlariDoldur();
            SaatleriDoldur();

            this.Shown += (s, e) => PaneliOrtala();
            this.Resize += (s, e) => PaneliOrtala();
        }

        void FormuOlustur()
        {
            this.Text = "Randevu Al";
            this.ClientSize = new Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(234, 246, 251);

            // ORTA PANEL
            panelOrta = new Panel();
            panelOrta.Size = new Size(480, 340);
            panelOrta.Parent = this;
            UIHelper.PanelStandart(panelOrta);

            int y = 30;

            // ===== HASTA TC =====
            LabelControl lblTC = new LabelControl()
            {
                Text = "Hasta TC:",
                Location = new Point(40, y)
            };
            UIHelper.LabelStandart(lblTC);

            txtHastaTC = new TextEdit()
            {
                Location = new Point(170, y - 5),
                Width = 230
            };
            UIHelper.TextEditStandart(txtHastaTC);

            y += 45;

            // ===== DOKTOR =====
            LabelControl lblDoktor = new LabelControl()
            {
                Text = "Doktor:",
                Location = new Point(40, y)
            };
            UIHelper.LabelStandart(lblDoktor);

            cmbDoktor = new ComboBoxEdit()
            {
                Location = new Point(170, y - 5),
                Width = 230
            };

            y += 45;

            // ===== TARİH =====
            LabelControl lblTarih = new LabelControl()
            {
                Text = "Tarih:",
                Location = new Point(40, y)
            };
            UIHelper.LabelStandart(lblTarih);

            dateTarih = new DateEdit()
            {
                Location = new Point(170, y - 5),
                Width = 230
            };

            y += 45;

            // ===== SAAT =====
            LabelControl lblSaat = new LabelControl()
            {
                Text = "Saat:",
                Location = new Point(40, y)
            };
            UIHelper.LabelStandart(lblSaat);

            cmbSaat = new ComboBoxEdit()
            {
                Location = new Point(170, y - 5),
                Width = 230
            };

            y += 65;

            // ===== BUTON =====
            btnKaydet = new SimpleButton()
            {
                Text = "Randevu Kaydet",
                Width = 260,
                Height = 50,
                Location = new Point(
                    (panelOrta.Width - 260) / 2,
                    y
                )
            };

            UIHelper.ButtonPrimary(btnKaydet);
            btnKaydet.Click += BtnKaydet_Click;

            // ===== CONTROLS =====
            panelOrta.Controls.Add(lblTC);
            panelOrta.Controls.Add(txtHastaTC);
            panelOrta.Controls.Add(lblDoktor);
            panelOrta.Controls.Add(cmbDoktor);
            panelOrta.Controls.Add(lblTarih);
            panelOrta.Controls.Add(dateTarih);
            panelOrta.Controls.Add(lblSaat);
            panelOrta.Controls.Add(cmbSaat);
            panelOrta.Controls.Add(btnKaydet);
        }


        void DoktorlariDoldur()
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                SqlCommand cmd = new SqlCommand(
                    "SELECT KullaniciAd FROM Kullanicilar WHERE KullaniciRol='Doktor'",
                    SQLBaglantisi.baglanti);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbDoktor.Properties.Items.Add(dr["KullaniciAd"].ToString());
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Doktorlar yüklenemedi: " + ex.Message);
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }

        void SaatleriDoldur()
        {
            string[] saatler =
            {
                "09:00","09:30","10:00","10:30",
                "11:00","11:30","13:00","13:30",
                "14:00","14:30","15:00","15:30"
            };

            cmbSaat.Properties.Items.AddRange(saatler);
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHastaTC.Text) ||
                cmbDoktor.SelectedIndex == -1 ||
                cmbSaat.SelectedIndex == -1)
            {
                XtraMessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            try
            {
                SQLBaglantisi.BaglantiAc();

                // 🔒 ÇAKIŞMA KONTROLÜ
                SqlCommand kontrolCmd = new SqlCommand(
                    @"SELECT COUNT(*) 
              FROM RandevuBilgileri 
              WHERE RandevuDoktor = @doktor
                AND RandevuTarih = @tarih
                AND RandevuSaat = @saat",
                    SQLBaglantisi.baglanti);

                kontrolCmd.Parameters.AddWithValue("@doktor", cmbDoktor.Text);
                kontrolCmd.Parameters.AddWithValue("@tarih", dateTarih.DateTime.Date);
                kontrolCmd.Parameters.AddWithValue("@saat", cmbSaat.Text);

                int sayi = (int)kontrolCmd.ExecuteScalar();

                if (sayi > 0)
                {
                    XtraMessageBox.Show(
                        "Bu doktor için seçilen tarih ve saat doludur.\nLütfen başka bir saat seçin.",
                        "Randevu Çakışması",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ✅ KAYDET
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO RandevuBilgileri 
              (RandevuHastaTC, RandevuDoktor, RandevuTarih, RandevuSaat)
              VALUES (@tc, @doktor, @tarih, @saat)",
                    SQLBaglantisi.baglanti);

                cmd.Parameters.AddWithValue("@tc", txtHastaTC.Text);
                cmd.Parameters.AddWithValue("@doktor", cmbDoktor.Text);
                cmd.Parameters.AddWithValue("@tarih", dateTarih.DateTime.Date);
                cmd.Parameters.AddWithValue("@saat", cmbSaat.Text);

                cmd.ExecuteNonQuery();

                XtraMessageBox.Show("Randevu başarıyla kaydedildi.");
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }
        void PaneliOrtala()
        {
            if (panelOrta == null) return;

            panelOrta.Left = (this.ClientSize.Width - panelOrta.Width) / 2;
            panelOrta.Top = (this.ClientSize.Height - panelOrta.Height) / 2;
        }


    }
}

