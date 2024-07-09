using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class Iletisim1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        SqlClass sqlclass  = new SqlClass();
        protected void btn_gonder_Click(object sender, EventArgs e)
        {
            var controlsToValidate = new Dictionary<Control, string>
            {
                { txtBox_adSoyad, txtBox_adSoyad.Text },
                { txtBox_mail, txtBox_mail.Text },
                { txtBox_konu, txtBox_konu.Text },
                { txtBox_mesaj, txtBox_mesaj.Text },
                // Diğer kontrolleri de buraya ekleyebilirsiniz
            };
            string adsoyad = txtBox_adSoyad.Text;
            string mail = txtBox_mail.Text;
            string konu = txtBox_konu.Text;
            string mesaj = txtBox_mesaj.Text;
            try
            {
                string validationMessage = Functions.ValidateForm(controlsToValidate);
                if(string.IsNullOrEmpty(validationMessage))
                {
                    Mesajlar yeni_mesaj = new Mesajlar(adsoyad, mail, konu, mesaj);
                    iletisimEkle(yeni_mesaj);

                    var controlsToClear = new List<Control> { txtBox_adSoyad, txtBox_mail, txtBox_konu, txtBox_mesaj};
                    Functions.ClearForm(controlsToClear);
                }
                else
                {
                    Label1.Text = validationMessage;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }


            
        }

        private void iletisimEkle(Mesajlar yenimesaj)
        {
            try
            {
                var parts = yenimesaj.ToString().Split(';');
                sqlclass.baglantiAc();

                string sqlquery = "INSERT INTO tbl_mesajlar (mesajlar_adsoyad, mesajlar_mail, mesajlar_konu,mesajlar_icerik) VALUES (@param1, @param2, @param3, @param4)";
                // sorguyu tamamla, nesne oluştur, parametreleri ekle ve sorguyu çalıştır
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@param1", parts[0]);
                komut.Parameters.AddWithValue("@param2", parts[1]);
                komut.Parameters.AddWithValue("@param3", parts[2]);
                komut.Parameters.AddWithValue("@param4", parts[3]);

                komut.ExecuteNonQuery();
                Label1.Text = "Mesajınız başarıyla alındı.";
                sqlclass.baglantiKapat();
            }
            catch(Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        

        
    }
}