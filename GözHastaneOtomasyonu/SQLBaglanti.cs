using System;

using System.Configuration; // App.config okumak için gerekli kütüphane

using System.Data;

using System.Data.SqlClient;



namespace GözHastaneOtomasyonu

{

    // Statik sınıf yapıyoruz, böylece her yerden doğrudan erişilebilir

    public static class SQLBaglantisi

    {

        public static string AktifKullaniciRolu = "";
        public static string AktifKullaniciAdi;


        // Bağlantı nesnemizi App.config'ten otomatik olarak almalı

        public static SqlConnection baglanti = new SqlConnection(

            ConfigurationManager.ConnectionStrings["baglanti"].ConnectionString

        );



        // Bağlantıyı açan metot

        public static void BaglantiAc()

        {

            if (baglanti.State == ConnectionState.Closed)

            {

                try

                {

                    baglanti.Open();

                }

                catch (Exception ex)

                {

                    // Hata durumunda kullanıcıya bilgi gösteririz

                    System.Windows.Forms.MessageBox.Show("Veritabanı bağlantısı kurulamadı!\nHata: " + ex.Message, "HATA", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    //                                                                                                                   ^ Hata ikonu eklendi

                }

            }

        }



        // Bağlantıyı kapatan metot

        public static void BaglantiKapat()

        {

            if (baglanti.State == ConnectionState.Open)

            {

                baglanti.Close();

            }

        }

    }

}