using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpellChecker.Native;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public partial class FrmRandevular : BaseForm
    {
        public FrmRandevular()
        {
            InitializeComponent();
            LayoutDuzenle();
            Listele();
            DoktorlariGetir();
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
        private void FrmRandevular_Load(object sender, EventArgs e)
        {

        }

        // --- KONTROLLER ---
        GridControl gridControl1 = new GridControl();
        GridView gridView1 = new GridView();
        GroupControl groupControl1 = new GroupControl();

        TextEdit txtID = new TextEdit();
        DateEdit deTarih = new DateEdit();
        TimeEdit teSaat = new TimeEdit();
        ComboBoxEdit cmbDoktor = new ComboBoxEdit();
        TextEdit txtHastaTC = new TextEdit();

        LabelControl lblHastaBilgi = new LabelControl();
        SimpleButton btnSorgula = new SimpleButton();
        SimpleButton btnKaydet = new SimpleButton();
        SimpleButton btnSil = new SimpleButton();
        SimpleButton btnAIDestek;

        void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                // SQL JOIN kullanarak TC yerine Hasta Adını da listeye ekliyoruz
                string sorgu = @"SELECT R.RandevuID, R.RandevuTarih, R.RandevuSaat, R.RandevuDoktor, 
                         R.RandevuHastaTC, H.AdSoyad AS 'Hasta Adı' 
                         FROM RandevuBilgileri R 
                         LEFT JOIN HastaBilgileri H ON R.RandevuHastaTC = H.TCKimlikNo";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, SQLBaglantisi.baglanti);
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Listeleme hatası: " + ex.Message); }
        }
        void DoktorlariGetir()
        {
            try
            {
                cmbDoktor.Properties.Items.Clear();
                SQLBaglantisi.BaglantiAc();
                SqlCommand komut = new SqlCommand("Select (DoktorAd + ' ' + DoktorSoyad) From DoktorBilgileri where DoktorBrans='Göz'", SQLBaglantisi.baglanti);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read()) { cmbDoktor.Properties.Items.Add(dr[0].ToString()); }
                SQLBaglantisi.BaglantiKapat();
            }
            catch (Exception ex) { MessageBox.Show("Doktorlar çekilemedi: " + ex.Message); }
        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();

                // SQL sütun isimlerini tablonla tam uyumlu hale getirdik
                SqlCommand komut = new SqlCommand("Select AdSoyad From HastaBilgileri where TCKimlikNo=@p1", SQLBaglantisi.baglanti);
                komut.Parameters.AddWithValue("@p1", txtHastaTC.Text);

                SqlDataReader dr = komut.ExecuteReader();

                if (dr.Read())
                {
                    // Sadece AdSoyad sütunu geldiği için dr[0] yeterli
                    lblHastaBilgi.Text = "HASTA: " + dr[0].ToString();
                    lblHastaBilgi.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblHastaBilgi.Text = "Kayıt Bulunamadı!";
                    lblHastaBilgi.ForeColor = Color.Red;
                }
                SQLBaglantisi.BaglantiKapat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgulama hatası: " + ex.Message);
                SQLBaglantisi.BaglantiKapat();
            }
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // 1. GÜVENLİK KONTROLÜ: Alanlar boş mu?
            if (string.IsNullOrEmpty(txtHastaTC.Text) || cmbDoktor.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen Hasta TC ve Doktor seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SQLBaglantisi.BaglantiAc();
                SqlCommand komut = new SqlCommand(
                    "INSERT INTO RandevuBilgileri (RandevuTarih, RandevuSaat, RandevuDoktor, RandevuHastaTC) " +
                    "VALUES (@p1, @p2, @p3, @p4)", SQLBaglantisi.baglanti);

                // DateEdit ve TimeEdit değerlerini daha sağlıklı alalım
                komut.Parameters.AddWithValue("@p1", deTarih.DateTime.Date);
                komut.Parameters.AddWithValue("@p2", teSaat.Text);
                komut.Parameters.AddWithValue("@p3", cmbDoktor.Text);
                komut.Parameters.AddWithValue(
     "@p4",
     SQLBaglantisi.AktifKullaniciAdi
 );
                ;

                komut.ExecuteNonQuery();
                SQLBaglantisi.BaglantiKapat();

                MessageBox.Show("Randevu başarıyla oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                RandevuTemizle(); // Kayıttan sonra kutuları boşaltalım
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme hatası: " + ex.Message);
                SQLBaglantisi.BaglantiKapat();
            }
        }

        // Yeni Temizle Metodu
        void RandevuTemizle()
        {
            txtID.Text = "";
            txtHastaTC.Text = "";
            lblHastaBilgi.Text = "Hasta: ---";
            lblHastaBilgi.ForeColor = Color.Black;
            cmbDoktor.SelectedIndex = -1;
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SQLBaglantisi.BaglantiAc();
                SqlCommand komut = new SqlCommand("Delete From RandevuBilgileri where RandevuID=@p1", SQLBaglantisi.baglanti);
                komut.Parameters.AddWithValue("@p1", txtID.Text);
                komut.ExecuteNonQuery();
                SQLBaglantisi.BaglantiKapat();
                Listele();
                MessageBox.Show("Randevu iptal edildi.");
            }
            catch (Exception ex) { MessageBox.Show("Silme hatası: " + ex.Message); }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["RandevuID"].ToString();
                deTarih.Text = dr["RandevuTarih"].ToString();
                teSaat.Text = dr["RandevuSaat"].ToString();
                cmbDoktor.Text = dr["RandevuDoktor"].ToString();
                txtHastaTC.Text = dr["RandevuHastaTC"].ToString();
            }
        }

        private void LayoutDuzenle()
        {
            this.Text = "Randevu Sistemi";
            this.Size = new Size(1200, 700);
            gridControl1.Parent = this; gridControl1.Dock = DockStyle.Fill;
            gridControl1.MainView = gridView1; gridView1.OptionsView.ShowGroupPanel = false;
            groupControl1.Parent = this; groupControl1.Text = "RANDEVU İŞLEMLERİ";
            groupControl1.Dock = DockStyle.Right; groupControl1.Width = 350;

            btnKaydet.Click += btnKaydet_Click;
            btnSil.Click += btnSil_Click;
            btnSorgula.Click += btnSorgula_Click;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
           // gridView1.DoubleClick += GridView1_DoubleClick;

            int y = 50;
            new LabelControl { Parent = groupControl1, Text = "ID:", Location = new Point(20, y) };
            txtID.Parent = groupControl1; txtID.Location = new Point(120, y); txtID.Width = 180; txtID.ReadOnly = true;
            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Tarih:", Location = new Point(20, y) };
            deTarih.Parent = groupControl1; deTarih.Location = new Point(120, y);
            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Saat:", Location = new Point(20, y) };
            teSaat.Parent = groupControl1; teSaat.Location = new Point(120, y);
            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Doktor:", Location = new Point(20, y) };
            cmbDoktor.Parent = groupControl1; cmbDoktor.Location = new Point(120, y);
            y += 40;
            new LabelControl { Parent = groupControl1, Text = "Hasta TC:", Location = new Point(20, y) };
            txtHastaTC.Parent = groupControl1; txtHastaTC.Location = new Point(120, y);
            y += 35;
            btnSorgula.Parent = groupControl1; btnSorgula.Text = "Sorgula"; btnSorgula.Location = new Point(120, y);
            y += 45;
            lblHastaBilgi.Parent = groupControl1; lblHastaBilgi.Text = "Hasta: ---"; lblHastaBilgi.Location = new Point(120, y);
            y += 60;
            btnKaydet.Parent = groupControl1; btnKaydet.Text = "Kaydet"; btnKaydet.Location = new Point(120, y);
            y += 45;
            btnSil.Parent = groupControl1; btnSil.Text = "Sil"; btnSil.Location = new Point(120, y);
            y += 60;

            btnAIDestek = new SimpleButton();

            btnAIDestek.Parent = groupControl1;
            btnAIDestek.Text = "MUAYENE İÇİN\nAI’DAN DESTEK AL";
            btnAIDestek.Size = new Size(180, 70);
            btnAIDestek.Location = new Point(120, y);

            btnAIDestek.Appearance.Font = new Font("Tahoma", 9, FontStyle.Bold);
            btnAIDestek.Appearance.BackColor =
    ColorTranslator.FromHtml("#1C4D8D");

            btnAIDestek.Appearance.ForeColor = Color.White;

            btnAIDestek.Appearance.Options.UseBackColor = true;
            btnAIDestek.Appearance.Options.UseForeColor = true;


            btnAIDestek.Click += BtnAIDestek_Click;
            UIHelper.GroupStandart(groupControl1);
            GridHelper.StandartAyarla(gridControl1, gridView1);


        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            // Listeden (Grid) seçili olan satırı alıyoruz
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                // Yeni oluşturduğumuz Muayene formunu çağırıyoruz
                FrmMuayene frm = new FrmMuayene();

                // Listeden aldığımız TC'yi yeni formdaki txtHastaTC alanına aktarıyoruz
                frm.txtHastaTC.Text = dr["RandevuHastaTC"].ToString();

                // Yeni formdaki verileri (Ad Soyad, Cinsiyet vb.) doldurması için tetikliyoruz
                frm.HastaBilgileriniGetir(frm.txtHastaTC.Text);

                // Muayene formunu açıyoruz
                frm.Show();
            }
        }
        private void BtnAIDestek_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr == null)
            {
                MessageBox.Show("Lütfen önce bir randevu seçin.");
                return;
            }

            string hastaTC = dr["RandevuHastaTC"].ToString();

            FrmMuayene frm = new FrmMuayene();
            frm.MdiParent = this.MdiParent;

            frm.txtHastaTC.Text = hastaTC;
            frm.HastaBilgileriniGetir(hastaTC);

            frm.Show();
        }

    }
}
    