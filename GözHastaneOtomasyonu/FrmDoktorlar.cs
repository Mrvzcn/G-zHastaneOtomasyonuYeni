using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace GözHastaneOtomasyonu
{
    public partial class FrmDoktorlar : DevExpress.XtraEditors.XtraForm
    {
        public FrmDoktorlar()
        {
            InitializeComponent();
            LayoutDuzenle(); // Tasarımı hazırlar
            Listele();       // Mevcut doktorları getirir
            BransGetir();    // Branş kutusunu ayarlar
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

        // --- KONTROL TANIMLAMALARI ---
        GridControl gridControl1 = new GridControl();
        GridView gridView1 = new GridView();
        GroupControl groupControl1 = new GroupControl();

        TextEdit txtID = new TextEdit();
        TextEdit txtAd = new TextEdit();
        TextEdit txtSoyad = new TextEdit();
        ComboBoxEdit cmbBrans = new ComboBoxEdit();
        TextEdit txtTC = new TextEdit();
        TextEdit txtTelefon = new TextEdit();

        SimpleButton btnKaydet = new SimpleButton();
        SimpleButton btnGuncelle = new SimpleButton();
        SimpleButton btnSil = new SimpleButton();

        // 1. Verileri Listeleme
        void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * From DoktorBilgileri", SQLBaglantisi.baglanti);
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        // 2. Branş Ayarlama (Sadece Göz)
        void BransGetir()
        {
            cmbBrans.Properties.Items.Clear();
            cmbBrans.Properties.Items.Add("Göz");
            cmbBrans.SelectedIndex = 0; // Varsayılan "Göz" seçili
        }

        // 3. Kutuları Temizleme
        void Temizle()
        {
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTC.Text = "";
            txtTelefon.Text = "";
            cmbBrans.SelectedIndex = 0; // Yine "Göz" kalsın
            txtAd.Focus();
        }

        // 4. Kaydetme İşlemi
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();
                SqlCommand komut = new SqlCommand("insert into DoktorBilgileri (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorTelefon) values (@p1, @p2, @p3, @p4, @p5)", SQLBaglantisi.baglanti);
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@p4", txtTC.Text);
                komut.Parameters.AddWithValue("@p5", txtTelefon.Text);
                komut.ExecuteNonQuery();
                SQLBaglantisi.BaglantiKapat();

                MessageBox.Show("Doktor başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message);
            }
        }

        // 5. Güncelleme İşlemi
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();
                SqlCommand komut = new SqlCommand("Update DoktorBilgileri set DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorTC=@p4, DoktorTelefon=@p5 where DoktorID=@p6", SQLBaglantisi.baglanti);
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@p4", txtTC.Text);
                komut.Parameters.AddWithValue("@p5", txtTelefon.Text);
                komut.Parameters.AddWithValue("@p6", txtID.Text);
                komut.ExecuteNonQuery();
                SQLBaglantisi.BaglantiKapat();

                MessageBox.Show("Doktor bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
        }

        // 6. Silme İşlemi
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();
                SqlCommand komut = new SqlCommand("Delete From DoktorBilgileri where DoktorID=@p1", SQLBaglantisi.baglanti);
                komut.Parameters.AddWithValue("@p1", txtID.Text);
                komut.ExecuteNonQuery();
                SQLBaglantisi.BaglantiKapat();

                MessageBox.Show("Doktor kaydı silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message);
            }
        }

        // 7. Tablodan Seçim Yapınca Kutuları Doldurma
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["DoktorID"].ToString();
                txtAd.Text = dr["DoktorAd"].ToString();
                txtSoyad.Text = dr["DoktorSoyad"].ToString();
                cmbBrans.Text = dr["DoktorBrans"].ToString();
                txtTC.Text = dr["DoktorTC"].ToString();
                txtTelefon.Text = dr["DoktorTelefon"].ToString();
            }
        }

        // --- TASARIM VE OLAY BAĞLAMA ---
        private void LayoutDuzenle()
        {
            this.Text = "Doktor Kayıt Paneli";
            this.Size = new System.Drawing.Size(1250, 750);

            // Tablo Ayarları
            gridControl1.Parent = this;
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.MainView = gridView1;
            gridView1.OptionsView.ShowGroupPanel = false;

            // Yan Panel
            groupControl1.Parent = this;
            groupControl1.Text = "DOKTOR BİLGİLERİ";
            groupControl1.Dock = DockStyle.Right;
            groupControl1.Width = 350;

            // Olayları Bağlama
            btnKaydet.Click += btnKaydet_Click;
            btnSil.Click += btnSil_Click;
            btnGuncelle.Click += btnGuncelle_Click;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;

            // Yerleşim Kodları
            int y = 50;
            new LabelControl { Parent = groupControl1, Text = "ID:", Location = new System.Drawing.Point(20, y) };
            txtID.Parent = groupControl1; txtID.Location = new System.Drawing.Point(110, y); txtID.Width = 200; txtID.ReadOnly = true;

            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Ad:", Location = new System.Drawing.Point(20, y) };
            txtAd.Parent = groupControl1; txtAd.Location = new System.Drawing.Point(110, y); txtAd.Width = 200;

            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Soyad:", Location = new System.Drawing.Point(20, y) };
            txtSoyad.Parent = groupControl1; txtSoyad.Location = new System.Drawing.Point(110, y); txtSoyad.Width = 200;

            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Branş:", Location = new System.Drawing.Point(20, y) };
            cmbBrans.Parent = groupControl1; cmbBrans.Location = new System.Drawing.Point(110, y); cmbBrans.Width = 200;

            // --- ÖNEMLİ: Kutuyu yazıya kapatan ayar ---
            cmbBrans.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            y += 40;
            new LabelControl { Parent = groupControl1, Text = "TC:", Location = new System.Drawing.Point(20, y) };
            txtTC.Parent = groupControl1; txtTC.Location = new System.Drawing.Point(110, y); txtTC.Width = 200;

            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Telefon:", Location = new System.Drawing.Point(20, y) };
            txtTelefon.Parent = groupControl1; txtTelefon.Location = new System.Drawing.Point(110, y); txtTelefon.Width = 200;

            y += 60;
            btnKaydet.Parent = groupControl1; btnKaydet.Text = "Kaydet"; btnKaydet.Location = new System.Drawing.Point(110, y); btnKaydet.Width = 200;
            y += 40;
            btnGuncelle.Parent = groupControl1; btnGuncelle.Text = "Güncelle"; btnGuncelle.Location = new System.Drawing.Point(110, y); btnGuncelle.Width = 200;
            y += 40;
            btnSil.Parent = groupControl1; btnSil.Text = "Sil"; btnSil.Location = new System.Drawing.Point(110, y); btnSil.Width = 200;
        }
    }
}