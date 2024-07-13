using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class ProfilBilgileri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                profilBilgileriGetir();
            }
        }
        SqlClass sqlclass = new SqlClass();

        private string getUserId()
        {
            string user_id = Request.QueryString["kullanici_id"];
            return user_id;
        }

        private void profilBilgileriGetir()
        {

            string kullanici_id = getUserId();
            if (kullanici_id == null)
                return;

            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_kullanicilar WHERE kullanici_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kullanici_id);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dt.Columns.Add("kullanici_fotograf_base64", typeof(string));
                oku.Close();
                

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtbox_id.Text = dr["kullanici_id"].ToString();
                    txtbox_username.Text = dr["kullanici_username"].ToString();
                    txtbox_sifre.Text = dr["kullanici_sifre"].ToString();
                    txtbox_ad.Text = dr["kullanici_isim"].ToString();
                    txtbox_soyad.Text = dr["kullanici_soyisim"].ToString();
                    txtbox_email.Text = dr["kullanici_mail"].ToString();
                    txtbox_telefon.Text = dr["kullanici_telno"].ToString();
                    
                    //txtbox_resim.Text = dr["kullanici_fotograf"].ToString();
                    if (dr["kullanici_fotograf"] != DBNull.Value && dr["kullanici_fotograf"] is byte[])
                    {
                        byte[] resimBytes = (byte[])dr["kullanici_fotograf"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        dr["kullanici_fotograf_base64"] = "data:image/jpg;base64," + base64String;
                    }
                    img_userphoto.ImageUrl = dr["kullanici_fotograf_base64"].ToString();
                    HiddenField1.Value = dr["kullanici_fotograf_base64"].ToString();
                    lblsonuc.Text = "Profil bilgileriniz başarıyla getirildi.";
                }
                else
                {
                    lblsonuc.Text = "Kullanıcı bilgileri bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Bir hata oluştu: " + ex.Message);
                
                // Log the exception if needed
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_resimguncelle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, lblresim, HiddenField1);
            img_userphoto.ImageUrl = HiddenField1.Value;
        }

        protected void btn_guncelle_Click(object sender, EventArgs e)
        {
            Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                {txtbox_ad, "Ad  "},
                {txtbox_soyad, "Soyad"},
                {txtbox_email, "Email "},
                {txtbox_sifre, "Şifre "},
                {txtbox_telefon, "Telefon "},
                {txtbox_username, "Username "},
                {txtbox_id, "Kullanıcı id "}
            };

            try
            {
                string validation = Functions.ValidateForm(controlsToValidate);
                bool mailval = Functions.IsValidEmail(txtbox_email.Text);
                if (validation == "" && mailval)
                {
                    string id = txtbox_id.Text.Trim();
                    string ad = txtbox_ad.Text.Trim();
                    string soyad = txtbox_soyad.Text.Trim();
                    string email = txtbox_email.Text.Trim();
                    string sifre = txtbox_sifre.Text.Trim();
                    string telefon = txtbox_telefon.Text.Trim();
                    string username = txtbox_username.Text.Trim();
                    string resim = HiddenField1.Value;

                    // Debugging output
                    //Response.Write($"HiddenField1.Value: {resim}<br/>");

                    if (!string.IsNullOrEmpty(resim))
                    {
                        sqlclass.baglantiAc();
                        string sqlquery = "UPDATE tbl_kullanicilar SET kullanici_isim = @Param1, " +
                                         "kullanici_soyisim = @Param2, kullanici_mail = @Param3, " +
                                         "kullanici_sifre = @Param4, kullanici_telno = @Param5, " +
                                         "kullanici_username = @Param6, " +
                                         "kullanici_fotograf=@Param10 " +
                                         "WHERE kullanici_id = @Param9";
                        SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                        komut.Parameters.AddWithValue("@Param1", ad);
                        komut.Parameters.AddWithValue("@Param2", soyad);
                        komut.Parameters.AddWithValue("@Param3", email);
                        komut.Parameters.AddWithValue("@Param4", sifre);
                        komut.Parameters.AddWithValue("@Param5", telefon);
                        komut.Parameters.AddWithValue("@Param6", username);
                        komut.Parameters.AddWithValue("@Param9", id);

                        // Split the base64 image string and check for errors
                        string[] resimParts = resim.Split(',');
                        if (resimParts.Length == 2)
                        {
                            byte[] k_resim = Convert.FromBase64String(resimParts[1]);
                            komut.Parameters.Add("@Param10", SqlDbType.Image).Value = k_resim;
                        }
                        else
                        {
                            lblsonuc.Text = "Resim güncellenirken bir hata oluştu. Lütfen tekrar deneyin.";
                            return;
                        }

                        komut.ExecuteNonQuery();
                        lblsonuc.Text = "Kullanıcı bilgileri başarıyla güncellendi";
                    }
                    else
                    {
                        lblsonuc.Text = "Resim güncellenirken bir hata oluştu. Lütfen tekrar deneyin.";
                    }
                }
                else
                {
                    lblsonuc.Text = Functions.ValidateForm(controlsToValidate);
                }

            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        
        protected void btn_kullaniciSil_Click(object sender, EventArgs e)
        {
            // delete user and log out
            string kullanici_id = txtbox_id.Text.ToString().Trim();
            
            // devam et
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "DELETE FROM tbl_kullanicilar WHERE kullanici_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kullanici_id);

                int affectedRows = komut.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    lblsonuc.Text = "Kullanıcı başarıyla silindi.";

                    // Oturumu kapat
                    FormsAuthentication.SignOut();
                    Session.Abandon();



                    // Oturum kapatma işlemini tamamlamak için gerekli olan cookieleri sil
                    HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                    cookie1.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Add(cookie1);

                    HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
                    cookie2.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Add(cookie2);

                    HttpCookie cookie3 = new HttpCookie("userCredentials", "");
                    cookie3.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Add(cookie3);

                    Thread.Sleep(300);
                    Response.Redirect("AnaSayfa.aspx");
                }
                else
                {
                    lblsonuc.Text = "Verilen kimlik bilgilerine sahip kullanıcı bulunamadı.";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

            

        }
    }
}