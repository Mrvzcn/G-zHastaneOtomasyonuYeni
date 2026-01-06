using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmHastaListesi : BaseForm
    {
        public FrmHastaListesi()
        {
            InitializeComponent();
            // Tıklama olayını koda bağladık (Manuel bağlantı)
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
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
        void Listele()
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                string rol = SQLBaglantisi.AktifKullaniciRolu;
                string sorgu;

                if (rol == "Doktor")
                {
                    sorgu = @"
            SELECT DISTINCT h.*
            FROM HastaBilgileri h
            INNER JOIN RandevuBilgileri r
                ON r.RandevuHastaTC = h.TCKimlikNo
            WHERE r.RandevuDoktor = @doktorAdi";
                }
                else if (rol == "Hasta")
                {
                    sorgu = @"
            SELECT *
            FROM HastaBilgileri
            WHERE KullaniciAd = @kullaniciAdi";
                }
                else
                {
                    sorgu = "SELECT * FROM HastaBilgileri";
                }

                SqlDataAdapter da = new SqlDataAdapter(sorgu, SQLBaglantisi.baglanti);

                if (rol == "Doktor")
                {
                    da.SelectCommand.Parameters.AddWithValue(
                        "@doktorAdi",
                        SQLBaglantisi.AktifKullaniciAdi
                    );
                }

                if (rol == "Hasta")
                {
                    da.SelectCommand.Parameters.AddWithValue(
                        "@kullaniciAdi",
                        SQLBaglantisi.AktifKullaniciAdi
                    );
                }

                // ✅ dt BURADA tanımlı
                DataTable dt = new DataTable();
                da.Fill(dt);

                // ✅ dt BURADA kullanılıyor (DOĞRU)
                gridControl1.DataSource = dt;

                // 🔒 ID SÜTUNUNU GİZLE
                if (gridView1.Columns["HastaID"] != null)
                {
                    gridView1.Columns["HastaID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Listeleme hatası: " + ex.Message);
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }




        private void FrmHastaListesi_Load(object sender, EventArgs e)
        {
            Listele();
           //UIHelper.LabelStandart(labelControl1);
            UIHelper.LabelStandart(labelControl2);
            UIHelper.LabelStandart(labelControl4);
            UIHelper.LabelStandart(labelControl5);

            // 🔵 SEÇİLİ HASTA BİLGİLERİ → SAĞ PANEL
            groupControl1.Dock = DockStyle.Right;
            groupControl1.Width = 320;

            // 🔵 GRID → KALAN ALANI DOLDURSUN
            gridControl1.Dock = DockStyle.Fill;

            // Standart görünüm (başlık, padding vs.)
            UIHelper.GroupStandart(groupControl1);

            Color yeniHastaRenk = ColorTranslator.FromHtml("#1C4D8D");

            simpleButton1.Appearance.BackColor = yeniHastaRenk; // GÜNCELLE
            simpleButton1.Appearance.ForeColor = Color.White;
            simpleButton1.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);

            BtnSil.Appearance.BackColor = yeniHastaRenk; //  SİL
            BtnSil.Appearance.ForeColor = Color.White;
            BtnSil.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);

            simpleButton1.Height = 40;
            BtnSil.Height = 40;
        }


        private void BtnSil_Click(object sender, EventArgs e)
        {
            var seciliID = gridView1.GetFocusedRowCellValue("HastaID");

            if (seciliID != null)
            {
                DialogResult onay = XtraMessageBox.Show(seciliID.ToString() + " ID'li hastayı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (onay == DialogResult.Yes)
                {
                    try
                    {
                        SQLBaglantisi.BaglantiAc();
                        SqlCommand komutSil = new SqlCommand("DELETE FROM HastaBilgileri WHERE HastaID=@p1", SQLBaglantisi.baglanti);
                        komutSil.Parameters.AddWithValue("@p1", seciliID);
                        komutSil.ExecuteNonQuery();

                        XtraMessageBox.Show("Hasta kaydı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Listele();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Silme hatası: " + ex.Message);
                    }
                    finally
                    {
                        SQLBaglantisi.BaglantiKapat();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz hastayı tablodan seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                // SQL sorgunda sütun başlığı neyse tırnak içine onu yazmalısın
                //txtID.Text = dr["HastaID"].ToString();
                txtAd.Text = dr["AdSoyad"].ToString(); // "HastaAd" yerine "Ad Soyad" yazdık
                txtTC.Text = dr["TCKimlikNo"].ToString();
                mskTelefon.Text = dr["Telefon"].ToString();
            }
        }

        // Kullanılmayan boş metodları temizleyebilirsin
        private void txtTC_EditValueChanged(object sender, EventArgs e) { }
        private void gridControl1_Click(object sender, EventArgs e) { }

        private void txtSoyad_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        
            private void simpleButton1_Click(object sender, EventArgs e)
        {
            

            try
            {
                SQLBaglantisi.BaglantiAc();

                SqlCommand komut = new SqlCommand(
                    "UPDATE HastaBilgileri SET AdSoyad=@p1, TCKimlikNo=@p2, Telefon=@p3 WHERE HastaID=@p4",
                    SQLBaglantisi.baglanti);

                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtTC.Text);
                komut.Parameters.AddWithValue("@p3", mskTelefon.Text);
                komut.Parameters.AddWithValue("@p4",
    gridView1.GetFocusedRowCellValue("HastaID"));


                komut.ExecuteNonQuery();

                XtraMessageBox.Show("Hasta bilgileri başarıyla güncellendi.", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Listele();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
            finally
            {
                SQLBaglantisi.BaglantiKapat();
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
    }
}

