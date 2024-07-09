using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class uye_ol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        SqlClass sqlclass = new SqlClass();

        protected void signupButton_Click(object sender, EventArgs e)
        {
            string isim = txtBox_isim.Text.Trim();
            string soyisim = txtBox_soyisim.Text.Trim();
            string kullaniciAdi = txtBox_username.Text.Trim();
            string telefonNumarasi = txtBox_telno.Text.Trim();
            string email = txtBox_email.Text.Trim();
            string sifre = txtBox_password.Text.Trim();
            string sifreTekrar = txtBox_confirmPassword.Text.Trim();
            string resim = HiddenField1.Value;
            string kullaniciTuru = "normal";
            bool rememberMe = false;
            Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>
            {
                { txtBox_isim, "İsim"},
                { txtBox_soyisim, "Soyisim"},
                {txtBox_telno, "Telefon Numarası" },
                { txtBox_username, "Kullanıcı Adı" },
                { txtBox_email, "E-mail" },
                { txtBox_password, "Şifre alanı" },
                { txtBox_confirmPassword, "Şifre alanı" },
                { profilePicture, "Resim alanı" }
            };
            List<Control> controlsToClearList = new List<Control>() {
                  txtBox_isim,txtBox_soyisim,txtBox_telno,txtBox_username,txtBox_email,txtBox_password,txtBox_confirmPassword,profilePicture
                };

            if (Functions.ValidateForm(controlsToValidate) == "")
            {
                // hata yok
                // Validate password and email
                if (Functions.passwordControl(sifre, sifreTekrar) && Functions.IsValidEmail(email))
                {
                    Kullanici yeni_kullanici = new Kullanici(isim, soyisim, email, sifre, resim, rememberMe, kullaniciTuru, kullaniciAdi, telefonNumarasi);
                    try
                    {
                        uyeKaydet(yeni_kullanici);
                        Functions.ClearForm(controlsToClearList);
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "Kayıt başarısız: " + ex.Message;
                    }
                }
                else
                {
                    Label1.Text = "Şifreler uyuşmuyor veya email geçersiz.";
                }
            }

            else
            {
                // hatalı case
                Label1.Text = Functions.ValidateForm(controlsToValidate);
            }


        }



        private void uyeKaydet(Kullanici yeni_kullanici)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "INSERT INTO tbl_kullanicilar (kullanici_isim,kullanici_soyisim,kullanici_username,kullanici_mail,kullanici_sifre,kullanici_fotograf,kullanici_remember,kullanici_tur,kullanici_telno) " +
                    "VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9)";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@param1", yeni_kullanici.Isim);
                komut.Parameters.AddWithValue("@param2", yeni_kullanici.Soyisim);
                komut.Parameters.AddWithValue("@param3", yeni_kullanici.Username);
                komut.Parameters.AddWithValue("@param4", yeni_kullanici.Email);
                komut.Parameters.AddWithValue("@param5", yeni_kullanici.Sifre);

                // Convert base64 image string to byte array
                byte[] resim = Convert.FromBase64String(yeni_kullanici.Resim.Split(',')[1]);
                komut.Parameters.Add("@param6", SqlDbType.Image).Value = resim;

                komut.Parameters.AddWithValue("@param7", yeni_kullanici.RememberMe);
                komut.Parameters.AddWithValue("@param8", yeni_kullanici.KullaniciTuru);
                komut.Parameters.AddWithValue("@param9", yeni_kullanici.TelefonNumarasi);
                komut.ExecuteNonQuery();
                Label1.Text = yeni_kullanici.Username + " sisteme kaydedildi.";
            }

            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void linkLbl_girisYap_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);  // 2000 milliseconds = 2 seconds
            Response.Redirect("giris-yap.aspx");
        }

        protected void uploadButton_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(profilePicture, uploadStatusLabel, HiddenField1);
            resimGoster(profileImage);
        }
        public void resimGoster(Image image)
        {
            image.ImageUrl = HiddenField1.Value;
        }
        protected void txtBox_password_TextChanged(object sender, EventArgs e)
        {

        }


    }
}