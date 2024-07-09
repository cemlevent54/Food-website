using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class giris_yap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        SqlClass sqlclass = new SqlClass();

        protected void linkLbl_sifre_Click(object sender, EventArgs e)
        {
            Response.Redirect("sifre-unuttum.aspx");
        }

        protected void linkLbl_uyeOl_Click(object sender, EventArgs e)
        {
            Response.Redirect("uye-ol.aspx");
        }

        private string checkKullaniciTuru(string kullaniciadi,string sifre)
        {
            string tur = "";
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT kullanici_tur FROM tbl_kullanicilar WHERE kullanici_username = @Param1 AND kullanici_sifre = @Param2";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kullaniciadi);
                komut.Parameters.AddWithValue("@Param2", sifre);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();
                DataRow dr = dt.Rows[0];
                tur = dr["kullanici_tur"].ToString();

            }catch(Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
            return tur;


        }
        protected void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string kullaniciAdi = username.Text.Trim();
                string sifre = password.Text.Trim();
                bool hatirla = rememberMe.Checked;
                string kullaniciTuru = checkKullaniciTuru(kullaniciAdi, sifre);
                
                if(kullaniciTuru == "admin")
                {
                    if (ValidateUser(kullaniciAdi, sifre))
                    {
                        if (hatirla)
                        {
                            HttpCookie httpCookie = new HttpCookie("adminCredentials");
                            httpCookie.Values.Add("username", kullaniciAdi);
                            httpCookie.Values.Add("password", sifre);
                            httpCookie.Expires = DateTime.Now.AddDays(30);
                            Response.Cookies.Add(httpCookie);

                            //kullanici.RememberMe = true;
                            //updateUserRememberMe(kullanici.Username, kullanici.Sifre);
                        }
                        //Thread.Sleep(5000);
                        Session["username"] = kullaniciAdi;

                        Response.Redirect("~/y/AnasayfaYonetici.aspx");
                        updateLoginState(kullaniciAdi, sifre);
                    }
                    
                }
                else if(kullaniciTuru == "normal")
                {
                    if (ValidateUser(kullaniciAdi, sifre))
                    {
                        if (hatirla)
                        {
                            HttpCookie httpCookie = new HttpCookie("userCredentials");
                            httpCookie.Values.Add("username", kullaniciAdi);
                            httpCookie.Values.Add("password", sifre);
                            httpCookie.Expires = DateTime.Now.AddDays(30);
                            Response.Cookies.Add(httpCookie);

                            //kullanici.RememberMe = true;
                            //updateUserRememberMe(kullanici.Username, kullanici.Sifre);
                        }
                        //Thread.Sleep(5000);
                        Session["username"] = kullaniciAdi;

                        Response.Redirect("~/Anasayfa.aspx");
                        updateLoginState(kullaniciAdi, sifre);
                    }
                }
                else
                {
                    uploadStatusLabel.Text = "Kullanıcı adı veya şifre yanlış.";
                    return;
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }

        private void updateLoginState(string username,string password)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "UPDATE tbl_kullanicilar SET kullanici_remember = @Param1 WHERE kullanici_username = @Param2 AND kullanici_sifre = @Param3";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                bool rem = rememberMe.Checked;
                komut.Parameters.AddWithValue("@Param1", rem);
                komut.Parameters.AddWithValue("@Param2", username);
                komut.Parameters.AddWithValue("@Param3", password);
                int d = komut.ExecuteNonQuery();
                uploadStatusLabel.Text = "Giriş başarılı.";



            }catch(Exception ex)
            {
                uploadStatusLabel.Text = "Bir hata oluştu: " + ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

        }
        private Kullanici CreateUsertoUse(string username, string password)
        {
            try
            {
                sqlclass.baglantiAc();

                string sqlquery = "SELECT * FROM tbl_kullanicilar WHERE kullanici_username = @Param1 AND kullanici_sifre = @Param2";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", username);
                komut.Parameters.AddWithValue("@Param2", password);

                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string isim = dr["kullanici_isim"].ToString();
                    string soyisim = dr["kullanici_soyisim"].ToString();
                    string mail = dr["kullanici_mail"].ToString();
                    string kullaniciAdi = dr["kullanici_isim"].ToString();
                    string foto = dr["kullanici_fotograf"].ToString();
                    string sifre = dr["kullanici_sifre"].ToString();
                    string tur = dr["kullanici_tur"].ToString();
                    bool rem = Convert.ToBoolean(dr["kullanici_remember"]);
                    string telno = dr["kullanici_telno"].ToString();

                    Kullanici kullanici = new Kullanici(isim, soyisim, mail, sifre, foto, rem, tur, kullaniciAdi, telno);
                    return kullanici;
                }
                else
                {
                    uploadStatusLabel.Text = "Kullanıcı adı veya şifre yanlış.";
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                uploadStatusLabel.Text = "Bir hata oluştu: " + ex.Message;
                return null;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        private bool ValidateUser(string kullaniciadi, string sifre)
        {
            bool isValid = false;

            try
            {
                sqlclass.baglantiAc();
                string sql = "SELECT COUNT(*) FROM tbl_kullanicilar WHERE kullanici_username = @Param1 AND kullanici_sifre = @Param2";
                SqlCommand komut = new SqlCommand(sql, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kullaniciadi);
                komut.Parameters.AddWithValue("@Param2", sifre);

                int count = (int)komut.ExecuteScalar();
                isValid = (count == 1);
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                uploadStatusLabel.Text = "Veritabanı hatası: " + ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

            return isValid;
        }

        

        private void updateUserRememberMe(string kullaniciadi, string sifre)
        {
            try
            {
                // bu fonksiyonda sorun var. buraya dönülecek
                sqlclass.baglantiAc();
                string sql = "UPDATE tbl_kullanicilar SET kullanici_remember = @Param1 WHERE kullanici_username = @Param2 AND kullanici_sifre = @Param3;";
                SqlCommand komut = new SqlCommand(sql, sqlclass.baglanti);
                bool rem = rememberMe.Checked;
                komut.Parameters.Add("@Param1", SqlDbType.Bit).Value = rem;
                komut.Parameters.Add("@Param2", SqlDbType.NVarChar).Value = kullaniciadi; // Trimming just in case
                komut.Parameters.Add("@Param3", SqlDbType.NVarChar).Value = sifre;
                int d = komut.ExecuteNonQuery();
                uploadStatusLabel.Text = "Giriş başarılı.";

            }catch(Exception ex)
            {
                uploadStatusLabel.Text = "Veritabanı hatası: " + ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }



    }
}