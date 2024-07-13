using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Threading;

namespace YemekSite
{
    public partial class master_page : System.Web.UI.MasterPage
    {
        SqlClass sqlclass = new SqlClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                kategoriYukleme();
                updateKategoriNumbers();
                HttpCookie userCredentialsCookie = Request.Cookies["userCredentials"];
                if (userCredentialsCookie != null && !string.IsNullOrEmpty(userCredentialsCookie.Value))
                {
                    pnl_logined_user.Visible = true;
                    kullaniciBilgileriGetir();
                    lnkbtn_login.Visible = false;
                    lnkbtn_signup.Visible = false;
                }
                else
                {
                    pnl_logined_user.Visible = false;
                }
            }
        }

        
        private void updateKategoriNumbers()
        {
            //tbl_yemeklerden onaylı yemekleri seçip kategori_id lerini al
            //tbl_kategorilerdeki kategori_id lere göre yemek sayısını güncelle
            try
            {
                sqlclass.baglantiAc();

                string sqlquery = "SELECT kategori_id FROM tbl_yemekler WHERE yemek_onay= 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    sqlquery = "SELECT COUNT(*) FROM tbl_yemekler WHERE kategori_id = @kategori_id AND yemek_onay = 1";
                    komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@kategori_id", int.Parse(dr["kategori_id"].ToString()));
                    int kategori_adet = Convert.ToInt32(komut.ExecuteScalar());

                    sqlquery = "UPDATE tbl_kategoriler SET kategori_adet = @kategori_adet WHERE kategori_id = @kategori_id";
                    komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@kategori_adet", kategori_adet);
                    komut.Parameters.AddWithValue("@kategori_id", int.Parse(dr["kategori_id"].ToString()));
                    komut.ExecuteNonQuery();
                }




            }catch(Exception ex)
            {
                Response.Write(ex.Message);
            }finally
            {
                sqlclass.baglantiKapat();
            }
        }
        private void kategoriYukleme()
        {
            sqlclass.baglantiAc();
            string sqlquery = "SELECT kategori_id, kategori_ad, kategori_adet FROM tbl_kategoriler";

            SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(oku);

            DataList1.DataSource = dt;
            DataList1.DataBind();
            sqlclass.baglantiKapat();
        }

        protected void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("AnaSayfa.aspx");
        }

        protected void btnGununYemegi_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("GununYemegi.aspx");
        }

        protected void btnTarifOner_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("TarifOneri.aspx");
        }

        protected void btnAbout_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("Hakkimizda.aspx");
        }

        protected void btnCommunication_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("Iletisim.aspx");
        }

        protected void lnkbtn_signup_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("uye-ol.aspx");
        }



        protected void lnkbtn_login_Click(object sender, EventArgs e)
        {
            Thread.Sleep(700);
            Response.Redirect("~/giris-yap.aspx"); // Use Response.Redirect for debugging
        }

        protected void lnkbtn_logout_Click(object sender, EventArgs e)
        {
            try
            {
                Session["user"] = null;

                if (Request.Cookies["userCredentials"] != null)
                {
                    HttpCookie mycookie = new HttpCookie("userCredentials");
                    mycookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(mycookie);
                }
                Response.Write("<script>alert('Çıkış yapıldı.');</script>");
                Thread.Sleep(3000);
                Response.Redirect("giris-yap.aspx");
                lnkbtn_login.Visible = true;
                lnkbtn_signup.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {

            }
        }

        private void kullaniciBilgileriGetir()
        {
            pnl_logined_user.Visible = true;

            // userCredentials cookie'sinden bilgileri al
            HttpCookie userCredentialsCookie = Request.Cookies["userCredentials"];
            if (userCredentialsCookie == null)
            {
                Response.Write("User credentials cookie is missing. Please login again.");
                return;
            }

            string userCredentials = userCredentialsCookie.Value;
            string[] credentials = userCredentials.Split('&');

            string username = null;
            string password = null;

            foreach (string credential in credentials)
            {
                string[] keyValue = credential.Split('=');
                if (keyValue.Length == 2)
                {
                    if (keyValue[0].Trim().ToLower() == "username")
                    {
                        username = keyValue[1].Trim();
                    }
                    else if (keyValue[0].Trim().ToLower() == "password")
                    {
                        password = keyValue[1].Trim();
                    }
                }
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Response.Write("Invalid credentials. Please login again.");
                return;
            }

            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_kullanicilar WHERE kullanici_username = @username AND kullanici_sifre = @sifre";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@username", username);
                komut.Parameters.AddWithValue("@sifre", password);

                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lbl_user_name.Text = dr["kullanici_username"].ToString();

                    if (dr["kullanici_fotograf"] != DBNull.Value && dr["kullanici_fotograf"] is byte[])
                    {
                        byte[] resimBytes = (byte[])dr["kullanici_fotograf"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        Image1.ImageUrl = "data:image/jpg;base64," + base64String;
                    }
                    else
                    {
                        Image1.ImageUrl = "default-image-path.jpg"; // Varsayılan resim yolunu buraya ekleyin
                    }
                }
                else
                {
                    Response.Write("No user found with the given credentials.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            // redirect to profile page
            Thread.Sleep(700);
            //Response.Redirect("~/ProfilBilgileri.aspx"); // Use Response.Redirect for debugging
            string userid = getUserId();
            Response.Redirect("~/ProfilBilgileri.aspx?kullanici_id="+userid); // Use Response.Redirect for debugging

            //Response.Redirect("~/y/YoneticiYemeklerDetay.aspx?yemek_id=" + getYemekId(sender));
        }

        private Dictionary<string, string> findUserIdFromSession()
        {
            Dictionary<string, string> userinfos = new Dictionary<string, string>();

            HttpCookie userCredentialsCookie = Request.Cookies["userCredentials"];
            if (userCredentialsCookie != null)
            {
                string cookieValue = userCredentialsCookie.Value;
                // Assuming the cookie value format is "username=...&password=..."
                var credentials = HttpUtility.ParseQueryString(cookieValue);
                string username = credentials["username"];
                string password = credentials["password"];
                userinfos.Add(username, password);
                return userinfos;
            }
            return null;
        }

        private string getUserId()
        {
            try
            {
                sqlclass.baglantiAc();
                Dictionary<string, string> userinfos = findUserIdFromSession();
                if (userinfos == null)
                {
                    return null;
                }
                string username = userinfos.Keys.ToList()[0];
                string password = userinfos.Values.ToList()[0];

                string sqlquery = "SELECT kullanici_id FROM tbl_kullanicilar WHERE kullanici_username=@Param2 AND kullanici_sifre=@Param3";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param2", username);
                komut.Parameters.AddWithValue("@Param3", password);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();
                DataRow dr = dt.Rows[0];
                return dr["kullanici_id"].ToString();

            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

            return "";
        }


        // onemli
        private string getKategoriId(object sender)
        {
            Label label = (Label)sender;
            return label.Text;
        }

        // buradan devam et
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            try
            {
                sqlclass.baglantiAc();

                var categoryImage = (Image)e.Item.FindControl("pctrSlider");
                if (categoryImage != null)
                {
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        var kategoriId = DataBinder.Eval(e.Item.DataItem, "kategori_id").ToString();
                        var kategoriAd = DataBinder.Eval(e.Item.DataItem, "kategori_ad").ToString();
                        string sqlquery = "SELECT kategori_resim FROM tbl_kategoriler WHERE kategori_id = @kategori_id";
                        SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                        komut.Parameters.AddWithValue("@kategori_id", kategoriId);
                        SqlDataReader oku = komut.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(oku);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            if (dr["kategori_resim"] != DBNull.Value && dr["kategori_resim"] is byte[])
                            {
                                byte[] resimBytes = (byte[])dr["kategori_resim"];
                                string base64String = Convert.ToBase64String(resimBytes);
                                string imageUrl = "data:image/jpg;base64," + base64String;

                                // Görüntü URL'sini ayarla
                                categoryImage.ImageUrl = imageUrl;
                                //categoryImage.AlternateText = kategoriAd;
                            }
                            else
                            {
                                // Varsayılan resim ayarla
                                categoryImage.ImageUrl = "C:\\Users\\Asus\\Desktop\\YemekSite\\YemekSite\\images\\giris_resim.jpg";
                            }
                        }
                        else
                        {
                            // Varsayılan resim ayarla
                            categoryImage.ImageUrl = "C:\\Users\\Asus\\Desktop\\YemekSite\\YemekSite\\images\\giris_resim.jpg";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }




        }
    }
}