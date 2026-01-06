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
    public partial class FrmHastaRandevularim : BaseForm
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
            panelSag.Width = 320;
            panelSag.BackColor = Color.White;
            panelSag.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelSag);

            int y = 25;

            // BAŞLIK
            Label lblBaslik = new Label
            {
                Text = "Randevu Detayı",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#0F2854"),
                Location = new Point(20, y),
                AutoSize = true
            };
            panelSag.Controls.Add(lblBaslik);
            y += 50;

            // TARİH
            panelSag.Controls.Add(LabelOlustur("Tarih:", y));
            txtTarih = TextBoxOlustur(y);
            y += 40;

            // SAAT
            panelSag.Controls.Add(LabelOlustur("Saat:", y));
            txtSaat = TextBoxOlustur(y);
            y += 40;

            // DOKTOR
            panelSag.Controls.Add(LabelOlustur("Doktor:", y));
            txtDoktor = TextBoxOlustur(y);
            y += 60;

            // ===== GÜNCELLE BUTONU =====
            btnGuncelle = new SimpleButton();
            btnGuncelle.Parent = panelSag;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.Size = new Size(260, 50);
            btnGuncelle.Location = new Point(30, y);
            UIHelper.ButtonPrimary(btnGuncelle);
            btnGuncelle.Click += BtnGuncelle_Click;

            y += 65;

            // ===== İPTAL BUTONU =====
            btnIptal = new SimpleButton();
            btnIptal.Parent = panelSag;
            btnIptal.Text = "Randevu İptal";
            btnIptal.Size = new Size(260, 50);
            btnIptal.Location = new Point(30, y);
            UIHelper.ButtonPrimary(btnIptal);
            btnIptal.Click += BtnIptal_Click;

            // Panel genel stil
            UIHelper.PanelStandart(panelSag);
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
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#0F2854"),
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
