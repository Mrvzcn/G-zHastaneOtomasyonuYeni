using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace GözHastaneOtomasyonu
{
    public partial class FrmDoktorlar : BaseForm

    {
        LabelControl lblID;
        LabelControl lblAd;
        LabelControl lblSoyad;
        LabelControl lblBrans;
        LabelControl lblTC;
        LabelControl lblTelefon;
        public FrmDoktorlar()
        {
            InitializeComponent();
            LayoutDuzenle(); // Tasarımı hazırlar
            Listele();       // Mevcut doktorları getirir
            BransGetir();    // Branş kutusunu ayarlar
            LabelStilUygula();

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
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM DoktorBilgileri",
                    SQLBaglantisi.baglanti
                );

                da.Fill(dt);
                gridControl1.DataSource = dt;

                // 🔒 KULLANICIDAN ID'Yİ GİZLE (DOĞRU YER)
                if (gridView1.Columns["DoktorID"] != null)
                    gridView1.Columns["DoktorID"].Visible = false;
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
            this.Size = new Size(1250, 750);

            gridControl1.Parent = this;
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.MainView = gridView1;
            gridView1.OptionsView.ShowGroupPanel = false;

            groupControl1.Parent = this;
            groupControl1.Text = "DOKTOR BİLGİLERİ";
            groupControl1.Dock = DockStyle.Right;
            groupControl1.Width = 350;

            int y = 50;

            lblID = new LabelControl();
            lblID.Text = "ID:";
            lblID.Parent = groupControl1;
            lblID.Location = new Point(20, y);

            txtID.Parent = groupControl1;
            txtID.Location = new Point(110, y);
            txtID.Width = 200;
            txtID.ReadOnly = true;

            y += 40;

            lblAd = new LabelControl();
            lblAd.Text = "Ad:";
            lblAd.Parent = groupControl1;
            lblAd.Location = new Point(20, y);

            txtAd.Parent = groupControl1;
            txtAd.Location = new Point(110, y);
            txtAd.Width = 200;

            y += 40;

            lblSoyad = new LabelControl();
            lblSoyad.Text = "Soyad:";
            lblSoyad.Parent = groupControl1;
            lblSoyad.Location = new Point(20, y);

            txtSoyad.Parent = groupControl1;
            txtSoyad.Location = new Point(110, y);
            txtSoyad.Width = 200;

            y += 40;

            lblBrans = new LabelControl();
            lblBrans.Text = "Branş:";
            lblBrans.Parent = groupControl1;
            lblBrans.Location = new Point(20, y);

            cmbBrans.Parent = groupControl1;
            cmbBrans.Location = new Point(110, y);
            cmbBrans.Width = 200;
            cmbBrans.Properties.TextEditStyle =
                DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            y += 40;

            lblTC = new LabelControl();
            lblTC.Text = "TC:";
            lblTC.Parent = groupControl1;
            lblTC.Location = new Point(20, y);

            txtTC.Parent = groupControl1;
            txtTC.Location = new Point(110, y);
            txtTC.Width = 200;

            y += 40;

            lblTelefon = new LabelControl();
            lblTelefon.Text = "Telefon:";
            lblTelefon.Parent = groupControl1;
            lblTelefon.Location = new Point(20, y);

            txtTelefon.Parent = groupControl1;
            txtTelefon.Location = new Point(110, y);
            txtTelefon.Width = 200;

            y += 60;

            btnKaydet.Parent = groupControl1;
            btnKaydet.Text = "Kaydet";
            btnKaydet.Location = new Point(110, y);
            btnKaydet.Width = 200;

            y += 60;

            btnGuncelle.Parent = groupControl1;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.Location = new Point(110, y);
            btnGuncelle.Width = 200;

            y += 60;

            btnSil.Parent = groupControl1;
            btnSil.Text = "Sil";
            btnSil.Location = new Point(110, y);
            btnSil.Width = 200;

            UIHelper.GroupStandart(groupControl1);
            LabelStilUygula();
            UIHelper.ButtonPrimary(btnKaydet);
            UIHelper.ButtonPrimary(btnGuncelle);
            UIHelper.ButtonPrimary(btnSil);
            lblID.Visible = false;
            txtID.Visible = false;

        }


        void LabelStilUygula()
            {
                UIHelper.LabelStandart(lblID);
                UIHelper.LabelStandart(lblAd);
                UIHelper.LabelStandart(lblSoyad);
                UIHelper.LabelStandart(lblBrans);
                UIHelper.LabelStandart(lblTC);
                UIHelper.LabelStandart(lblTelefon);
            }


        }
    }