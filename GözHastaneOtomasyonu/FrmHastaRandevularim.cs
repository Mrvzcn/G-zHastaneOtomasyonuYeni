using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GözHastaneOtomasyonu
{
    public partial class FrmHastaRandevularim : Form
    {
        GridControl grid;
        GridView view;
        Panel panelSag;

        TextEdit txtTarih;
        TextEdit txtSaat;
        TextEdit txtDoktor;

        SimpleButton btnGuncelle;
        SimpleButton btnIptal;

        int seciliRandevuId = -1;

        public FrmHastaRandevularim()
        {
            InitializeComponent();
            this.Load += FrmHastaRandevularim_Load;
        }

        private void FrmHastaRandevularim_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(234, 246, 251);

            GridOlustur();
            SagPanelOlustur();
            RandevulariGetir();
        }

        // ================= GRID =================
        void GridOlustur()
        {
            grid = new GridControl();
            view = new GridView();

            grid.MainView = view;
            grid.ViewCollection.Add(view);
            grid.Dock = DockStyle.Fill;

            this.Controls.Add(grid);

            view.FocusedRowChanged += View_FocusedRowChanged;
        }

        // ================= SAĞ PANEL =================
        void SagPanelOlustur()
        {
            panelSag = new Panel();
            panelSag.Dock = DockStyle.Right;
            panelSag.Width = 300;
            panelSag.BackColor = Color.White;
            panelSag.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelSag);

            int y = 20;

            panelSag.Controls.Add(Baslik("Randevu Detayı", y));
            y += 50;

            panelSag.Controls.Add(LabelOlustur("Tarih:", y));
            txtTarih = TextBoxOlustur(y);
            y += 40;

            panelSag.Controls.Add(LabelOlustur("Saat:", y));
            txtSaat = TextBoxOlustur(y);
            y += 40;

            panelSag.Controls.Add(LabelOlustur("Doktor:", y));
            txtDoktor = TextBoxOlustur(y);
            y += 60;

            btnGuncelle = Buton("Güncelle", Color.DeepSkyBlue, y);
            btnGuncelle.Click += BtnGuncelle_Click;
            y += 45;

            btnIptal = Buton("Randevu İptal", Color.IndianRed, y);
            btnIptal.Click += BtnIptal_Click;
        }

        Label Baslik(string text, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Tahoma", 12, FontStyle.Bold),
                Location = new Point(20, y),
                AutoSize = true
            };
        }

        Label LabelOlustur(string text, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(20, y),
                AutoSize = true
            };
        }

        TextEdit TextBoxOlustur(int y)
        {
            TextEdit t = new TextEdit();
            t.Location = new Point(100, y - 4);
            t.Width = 170;
            panelSag.Controls.Add(t);
            return t;
        }

        SimpleButton Buton(string text, Color renk, int y)
        {
            SimpleButton b = new SimpleButton();
            b.Text = text;
            b.Size = new Size(240, 35);
            b.Location = new Point(20, y);
            b.Appearance.BackColor = renk;
            b.Appearance.ForeColor = Color.White;
            b.Appearance.Options.UseBackColor = true;
            b.Appearance.Options.UseForeColor = true;
            panelSag.Controls.Add(b);
            return b;
        }

        // ================= VERİ =================
        void RandevulariGetir()
        {
            SQLBaglantisi.BaglantiAc();

            SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
                    RandevuID,
                    RandevuTarih,
                    RandevuSaat,
                    RandevuDoktor
                  FROM RandevuBilgileri
                  WHERE RandevuHastaTC = @p1",
                SQLBaglantisi.baglanti);

            da.SelectCommand.Parameters.AddWithValue(
                "@p1",
                SQLBaglantisi.AktifKullaniciAdi
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            grid.DataSource = dt;

            SQLBaglantisi.BaglantiKapat();
        }

        // ================= SEÇİM =================
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (view.FocusedRowHandle < 0) return;

            seciliRandevuId = Convert.ToInt32(view.GetFocusedRowCellValue("RandevuID"));
            txtTarih.Text = view.GetFocusedRowCellValue("RandevuTarih").ToString();
            txtSaat.Text = view.GetFocusedRowCellValue("RandevuSaat").ToString();
            txtDoktor.Text = view.GetFocusedRowCellValue("RandevuDoktor").ToString();
        }

        // ================= GÜNCELLE =================
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (seciliRandevuId == -1) return;

            SQLBaglantisi.BaglantiAc();

            SqlCommand cmd = new SqlCommand(
                @"UPDATE RandevuBilgileri 
                  SET RandevuTarih=@t, RandevuSaat=@s 
                  WHERE RandevuID=@id",
                SQLBaglantisi.baglanti);

            cmd.Parameters.AddWithValue("@t", txtTarih.Text);
            cmd.Parameters.AddWithValue("@s", txtSaat.Text);
            cmd.Parameters.AddWithValue("@id", seciliRandevuId);

            cmd.ExecuteNonQuery();
            SQLBaglantisi.BaglantiKapat();

            RandevulariGetir();
            XtraMessageBox.Show("Randevu güncellendi");
        }

        // ================= İPTAL =================
        private void BtnIptal_Click(object sender, EventArgs e)
        {
            if (seciliRandevuId == -1) return;

            if (MessageBox.Show("Randevu iptal edilsin mi?", "Onay",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            SQLBaglantisi.BaglantiAc();

            SqlCommand cmd = new SqlCommand(
                "DELETE FROM RandevuBilgileri WHERE RandevuID=@id",
                SQLBaglantisi.baglanti);

            cmd.Parameters.AddWithValue("@id", seciliRandevuId);
            cmd.ExecuteNonQuery();

            SQLBaglantisi.BaglantiKapat();

            RandevulariGetir();
            XtraMessageBox.Show("Randevu iptal edildi");
        }
    }
}
