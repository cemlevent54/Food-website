using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite.y
{
    public partial class KullaniciKontrolDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string kullanici_id = getKullaniciId();
                if(!string.IsNullOrEmpty(getKullaniciId()))
                {
                    kullaniciBilgileriniGetir(getKullaniciId());
                }
                else
                {
                    Response.Write("Kullanıcı id boş olamaz");
                }
                
            }
        }
        SqlClass sqlclass = new SqlClass();
        private string getKullaniciId()
        {
            return Request.QueryString["kullanici_id"];
        }

        private void kullaniciBilgileriniGetir(string kullanici_id)
        {
            try
            {
                sqlclass.baglantiAc();
                string query = "SELECT * FROM tbl_kullanicilar WHERE kullanici_id = @kullanici_id";
                SqlCommand komut = new SqlCommand(query, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@kullanici_id", kullanici_id);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();
                dt.Columns.Add("kullanici_fotograf_base64", typeof(string));
                foreach(DataRow dr in dt.Rows)
                {
                    txtbox_id.Text = dr["kullanici_id"].ToString();
                    txtbox_ad.Text = dr["kullanici_isim"].ToString();
                    txtbox_soyad.Text = dr["kullanici_soyisim"].ToString();
                    txtbox_email.Text = dr["kullanici_mail"].ToString();
                    txtbox_sifre.Text = dr["kullanici_sifre"].ToString();
                    txtbox_telefon.Text = dr["kullanici_telno"].ToString();
                    txtbox_username.Text = dr["kullanici_username"].ToString();
                    //txtbox_kullaniciTur.Text = dr["kullanici_tur"].ToString();
                    txtbox_login.Text = dr["kullanici_remember"].ToString();
                    if(dr["kullanici_tur"].ToString() == "admin")
                    {
                        ddl_kullaniciTuru.SelectedIndex = 0;
                    }
                    else
                    {
                        ddl_kullaniciTuru.SelectedIndex = 1;
                    }
                    if (dr["kullanici_fotograf"] != DBNull.Value && dr["kullanici_fotograf"] is byte[])
                    {
                        byte[] resimBytes = (byte[])dr["kullanici_fotograf"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        dr["kullanici_fotograf_base64"] = "data:image/jpg;base64," + base64String;
                    }
                    img_userphoto.ImageUrl = dr["kullanici_fotograf_base64"].ToString();

                }
                

            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_guncelle_Click(object sender, EventArgs e)
        {
            Dictionary<Control,string> controlsToValidate = new Dictionary<Control, string>() {
                {txtbox_ad, "Ad  "},
                {txtbox_soyad, "Soyad"},
                {txtbox_email, "Email "},
                {txtbox_sifre, "Şifre "},
                {txtbox_telefon, "Telefon "},
                {txtbox_username, "Username "},
                {txtbox_login, "Login "},
                {txtbox_id, "Kullanıcı id "}
            };

            try
            {
                string validation = Functions.ValidateForm(controlsToValidate);
                bool mailval = Functions.IsValidEmail(txtbox_email.Text);
                if(validation == "" && mailval)
                {
                    string id = txtbox_id.Text.Trim();
                    string ad = txtbox_ad.Text.Trim();
                    string soyad = txtbox_soyad.Text.Trim();
                    string email = txtbox_email.Text.Trim();
                    string sifre = txtbox_sifre.Text.Trim();
                    string telefon = txtbox_telefon.Text.Trim();
                    string username = txtbox_username.Text.Trim();
                    string kullaniciTur = ddl_kullaniciTuru.SelectedItem.Text;
                    string login = txtbox_login.Text.Trim();
                    string resim = HiddenField1.Value;
                    bool loginBool = Convert.ToBoolean(login);
                    sqlclass.baglantiAc();
                    string sqlquery = "UPDATE tbl_kullanicilar SET kullanici_isim = @Param1, " +
                                     "kullanici_soyisim = @Param2, kullanici_mail = @Param3, " +
                                     "kullanici_sifre = @Param4, kullanici_telno = @Param5, " +
                                     "kullanici_username = @Param6, kullanici_tur = @Param7, " +
                                     "kullanici_remember = @Param8, kullanici_fotograf=@Param10 " +
                                     "WHERE kullanici_id = @Param9";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", ad);
                    komut.Parameters.AddWithValue("@Param2", soyad);
                    komut.Parameters.AddWithValue("@Param3", email);
                    komut.Parameters.AddWithValue("@Param4", sifre);
                    komut.Parameters.AddWithValue("@Param5", telefon);
                    komut.Parameters.AddWithValue("@Param6", username);
                    komut.Parameters.AddWithValue("@Param7", kullaniciTur);
                    komut.Parameters.AddWithValue("@Param8", loginBool);
                    komut.Parameters.AddWithValue("@Param9", id);
                    // Convert base64 image string to byte array
                    byte[] k_resim = Convert.FromBase64String(resim.Split(',')[1]);
                    komut.Parameters.Add("@Param10", SqlDbType.Image).Value = k_resim;
                    komut.ExecuteNonQuery();
                    lblsonuc.Text = "Kullanıcı bilgileri başarıyla güncellendi";
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

        protected void btn_resimguncelle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, lblresim, HiddenField1);
            img_userphoto.ImageUrl = HiddenField1.Value;
        }

        

        
    }
}