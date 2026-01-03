using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmMuayene : XtraForm
    {
        // === PANELLER ===
        GroupControl groupHastaBilgi = new GroupControl();
        GroupControl groupAIOntani = new GroupControl();
        GroupControl groupMuayene = new GroupControl();
        GroupControl groupIlkMuayene = new GroupControl();

        // === HASTA ===
        public TextEdit txtHastaTC = new TextEdit();
        TextEdit txtAdSoyad = new TextEdit();
        TextEdit txtYasCinsiyet = new TextEdit();

        // === MUAYENE ===
        MemoEdit txtSikayet = new MemoEdit();
        LabelControl lblAITahmin = new LabelControl();

        TextEdit txtMiyopSag = new TextEdit();
        TextEdit txtMiyopSol = new TextEdit();
        TextEdit txtAstigmatSag = new TextEdit();
        TextEdit txtAstigmatSol = new TextEdit();
        // Hipermetrop
        TextEdit txtHipermetropSag = new TextEdit();
        TextEdit txtHipermetropSol = new TextEdit();

        // Gözlük / Lens
        TextEdit txtGozlukSag = new TextEdit();
        TextEdit txtGozlukSol = new TextEdit();


        ComboBoxEdit cmbGlokomRisk = new ComboBoxEdit();

        SimpleButton btnAIPredict = new SimpleButton();
        SimpleButton btnKaydet = new SimpleButton();

        public FrmMuayene()
        {
            InitializeComponent();
            LayoutDuzenle();
        }

        private void LayoutDuzenle()
        {
            this.Text = "Doktor Muayene Paneli & AI Destekli Tanı";
            this.Size = new Size(820, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ================= HASTA BİLGİLERİ =================
            groupHastaBilgi.Parent = this;
            groupHastaBilgi.Text = "HASTA BİLGİLERİ";
            groupHastaBilgi.Bounds = new Rectangle(10, 10, 350, 180);

            int y = 35;
            new LabelControl { Parent = groupHastaBilgi, Text = "TC Kimlik:", Location = new Point(15, y) };
            txtHastaTC.Parent = groupHastaBilgi;
            txtHastaTC.Bounds = new Rectangle(120, y, 200, 22);
            txtHastaTC.ReadOnly = true;

            y += 35;
            new LabelControl { Parent = groupHastaBilgi, Text = "Ad Soyad:", Location = new Point(15, y) };
            txtAdSoyad.Parent = groupHastaBilgi;
            txtAdSoyad.Bounds = new Rectangle(120, y, 200, 22);
            txtAdSoyad.ReadOnly = true;

            y += 35;
            new LabelControl { Parent = groupHastaBilgi, Text = "Yaş / Cinsiyet:", Location = new Point(15, y) };
            txtYasCinsiyet.Parent = groupHastaBilgi;
            txtYasCinsiyet.Bounds = new Rectangle(120, y, 200, 22);
            txtYasCinsiyet.ReadOnly = true;

            // ================= AI ÖN TANI PANELİ =================
            groupAIOntani.Parent = this;
            groupAIOntani.Text = "AI ÖN TANI VE DEĞERLENDİRME";
            groupAIOntani.Bounds = new Rectangle(10, 200, 350, 220);

            lblAITahmin.Parent = groupAIOntani;
            lblAITahmin.Text = "Henüz AI değerlendirmesi yapılmadı.";
            lblAITahmin.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
            lblAITahmin.Appearance.ForeColor = Color.DarkGreen;
            lblAITahmin.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblAITahmin.AutoSizeMode = LabelAutoSizeMode.None;
            lblAITahmin.Bounds = new Rectangle(10, 30, 325, 170);

            // ================= MUAYENE PANELİ =================
            groupMuayene.Parent = this;
            groupMuayene.Text = "MUAYENE DETAYLARI";
            groupMuayene.Bounds = new Rectangle(370, 10, 430, 580);

            // -------- İLK MUAYENE BULGULARI --------
            groupIlkMuayene.Parent = groupMuayene;
            groupIlkMuayene.Text = "İLK MUAYENE BULGULARI";
            groupIlkMuayene.Bounds = new Rectangle(20, 35, 380, 220);

            int y2 = 30;

            // Miyop
            new LabelControl { Parent = groupIlkMuayene, Text = "Miyop (Sağ / Sol):", Location = new Point(10, y2) };
            txtMiyopSag.Parent = groupIlkMuayene;
            txtMiyopSag.Bounds = new Rectangle(170, y2, 70, 22);
            txtMiyopSol.Parent = groupIlkMuayene;
            txtMiyopSol.Bounds = new Rectangle(250, y2, 70, 22);

            y2 += 35;

            // Hipermetrop
            new LabelControl { Parent = groupIlkMuayene, Text = "Hipermetrop (Sağ / Sol):", Location = new Point(10, y2) };
            txtHipermetropSag.Parent = groupIlkMuayene;
            txtHipermetropSag.Bounds = new Rectangle(170, y2, 70, 22);
            txtHipermetropSol.Parent = groupIlkMuayene;
            txtHipermetropSol.Bounds = new Rectangle(250, y2, 70, 22);

            y2 += 35;

            // Astigmat
            new LabelControl { Parent = groupIlkMuayene, Text = "Astigmat (Sağ / Sol):", Location = new Point(10, y2) };
            txtAstigmatSag.Parent = groupIlkMuayene;
            txtAstigmatSag.Bounds = new Rectangle(170, y2, 70, 22);
            txtAstigmatSol.Parent = groupIlkMuayene;
            txtAstigmatSol.Bounds = new Rectangle(250, y2, 70, 22);

            y2 += 35;

            // Glokom
            new LabelControl { Parent = groupIlkMuayene, Text = "Glokom (Göz Tansiyonu):", Location = new Point(10, y2) };
            cmbGlokomRisk.Parent = groupIlkMuayene;
            cmbGlokomRisk.Bounds = new Rectangle(170, y2, 150, 22);
            cmbGlokomRisk.Properties.Items.AddRange(new[] { "Normal", "Şüpheli", "Yüksek" });

            y2 += 35;

            // Gözlük / Lens
            new LabelControl { Parent = groupIlkMuayene, Text = "Gözlük / Lens (Sağ / Sol):", Location = new Point(10, y2) };
            txtGozlukSag.Parent = groupIlkMuayene;
            txtGozlukSag.Bounds = new Rectangle(170, y2, 70, 22);
            txtGozlukSol.Parent = groupIlkMuayene;
            txtGozlukSol.Bounds = new Rectangle(250, y2, 70, 22);


            // -------- ŞİKAYET --------
            // ŞİKAYET BAŞLIĞI
            new LabelControl
            {
                Parent = groupMuayene,
                Text = "Şikayetiniz",
                Location = new Point(20, 265),
                Appearance = { Font = new Font("Tahoma", 9.5f, FontStyle.Bold) }
            };

            txtSikayet.Parent = groupMuayene;
            txtSikayet.Bounds = new Rectangle(20, 290, 380, 150);



            // -------- BUTONLAR --------
            // ================= BUTONLAR =================

            // AI Tanı Önerisi
            btnAIPredict.Parent = groupMuayene;
            btnAIPredict.Text = "AI TANI ÖNERİSİ AL";
            btnAIPredict.Bounds = new Rectangle(20, 450, 380, 45);
            btnAIPredict.Appearance.BackColor = Color.DarkSlateBlue;
            btnAIPredict.Appearance.ForeColor = Color.White;
            btnAIPredict.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
            btnAIPredict.Click += BtnAIPredict_Click;

            // Muayeneyi Tamamla ve Kaydet (AI butonunun ALTINDA)
            btnKaydet.Parent = groupMuayene;
            btnKaydet.Text = "MUAYENEYİ TAMAMLA VE KAYDET";
            btnKaydet.Bounds = new Rectangle(20, 505, 380, 40);
            btnKaydet.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
            btnKaydet.Click += BtnKaydet_Click;

        }

        // ================= AI BUTONU =================
        private async void BtnAIPredict_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSikayet.Text))
            {
                XtraMessageBox.Show("Lütfen önce şikayet girin.");
                return;
            }

            lblAITahmin.Text = "Gemini düşünüyor...";
            lblAITahmin.ForeColor = Color.DarkOrange;
            btnAIPredict.Enabled = false;

            try
            {
                string apiKey = ConfigurationManager.AppSettings["GEMINI_API_KEY"];
                string apiUrl =
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    var body = new
                    {
                        contents = new[]
                        {
                            new {
                                parts = new[] {
                                    new {
                                        text =
                                        "Bir göz doktoru gibi davran.\n" +
                                        "En fazla 3 maddelik kısa ön tanı yaz.\n\n" +
                                        $"Şikayet: {txtSikayet.Text}\n" +
                                        $"Miyop: {txtMiyopSag.Text}/{txtMiyopSol.Text}\n" +
                                        $"Astigmat: {txtAstigmatSag.Text}/{txtAstigmatSol.Text}\n" +
                                        $"Glokom: {cmbGlokomRisk.Text}"
                                    }
                                }
                            }
                        }
                    };

                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);
                    var responseJson = await response.Content.ReadAsStringAsync();

                    dynamic result = JsonConvert.DeserializeObject(responseJson);
                    lblAITahmin.Text = "AI Ön Tanı:\n" +
                        result.candidates[0].content.parts[0].text.ToString().Trim();
                    lblAITahmin.ForeColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                lblAITahmin.Text = "AI hata verdi.";
                lblAITahmin.ForeColor = Color.Red;
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                btnAIPredict.Enabled = true;
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Muayene kaydedildi.");
        }
        public void HastaBilgileriniGetir(string tc)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                SqlCommand cmd = new SqlCommand(
                    "SELECT AdSoyad, Cinsiyet, DogumTarihi FROM HastaBilgileri WHERE TCKimlikNo = @tc",
                    SQLBaglantisi.baglanti);

                cmd.Parameters.AddWithValue("@tc", tc);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtHastaTC.Text = tc;
                    txtAdSoyad.Text = dr["AdSoyad"].ToString();
                    txtYasCinsiyet.Text =
                        dr["Cinsiyet"].ToString() + " / " +
                        Convert.ToDateTime(dr["DogumTarihi"]).ToShortDateString();
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hasta bilgileri getirilemedi:\n" + ex.Message);
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }

    }
}
