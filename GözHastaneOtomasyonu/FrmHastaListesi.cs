using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmHastaListesi : XtraForm
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
                    // 🔴 HASTA SADECE KENDİNİ GÖRÜR
                    sorgu = @"
                SELECT *
                FROM HastaBilgileri
                WHERE KullaniciAd = @kullaniciAdi";
                }
                else
                {
                    // Admin, Sekreter vb.
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

                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
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

            // TASARIM DÜZELTME: Tabloyu sola yasla, paneli sağa al
            gridControl1.Dock = DockStyle.Left;
            gridControl1.Width = 600; // Tablo genişliği
            gridControl1.Dock = DockStyle.Fill; // Panel geri kalan yeri kaplasın


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
                txtID.Text = dr["HastaID"].ToString();
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
            if (string.IsNullOrEmpty(txtID.Text))
            {
                XtraMessageBox.Show("Lütfen güncellenecek hastayı seçin.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SQLBaglantisi.BaglantiAc();

                SqlCommand komut = new SqlCommand(
                    "UPDATE HastaBilgileri SET AdSoyad=@p1, TCKimlikNo=@p2, Telefon=@p3 WHERE HastaID=@p4",
                    SQLBaglantisi.baglanti);

                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtTC.Text);
                komut.Parameters.AddWithValue("@p3", mskTelefon.Text);
                komut.Parameters.AddWithValue("@p4", txtID.Text);

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

    }
}

