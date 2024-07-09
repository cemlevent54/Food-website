using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
                HttpCookie userCredentialsCookie = Request.Cookies["adminCredentials"];
                if (userCredentialsCookie != null && !string.IsNullOrEmpty(userCredentialsCookie.Value))
                {
                    getAllUserInfo();
                }
                else
                {
                    lnkbtn_cikisYap.Visible = false;
                    Image1.Visible = false;
                    lbl_kullaniciAdi.Visible = false;
                }
            }
        }

        private Dictionary<string, string> SummarizeUserCookie()
        {
            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            // userCredentials cookie'sinden bilgileri al
            HttpCookie userCredentialsCookie = Request.Cookies["adminCredentials"];
            if (userCredentialsCookie == null)
            {
                Response.Write("User credentials cookie is missing. Please login again.");
                return null;
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
                return null;
            }

            userInfo["username"] = username;
            userInfo["password"] = password;

            return userInfo;
        }

        SqlClass sqlclass = new SqlClass();

        private void getAllUserInfo()
        {
            Dictionary<string, string> userInfo = SummarizeUserCookie();
            if (userInfo == null || !userInfo.ContainsKey("username") || !userInfo.ContainsKey("password"))
            {
                return;
            }

            string username = userInfo["username"];
            string password = userInfo["password"];

            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_kullanicilar WHERE kullanici_username = @username AND kullanici_sifre = @password";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@username", username);
                komut.Parameters.AddWithValue("@password", password);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dt.Columns.Add("kullanici_fotograf_base64", typeof(string));

                oku.Close();

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    if (dr["kullanici_fotograf"] != DBNull.Value && dr["kullanici_fotograf"] is byte[])
                    {
                        byte[] resimBytes = (byte[])dr["kullanici_fotograf"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        dr["kullanici_fotograf_base64"] = "data:image/jpg;base64," + base64String;
                    }
                    lbl_kullaniciAdi.Text = dr["kullanici_username"].ToString();
                    Image1.ImageUrl = dr["kullanici_fotograf_base64"].ToString();
                }
                else
                {
                    Response.Write("No user found with the provided credentials.");
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

        protected void lnkbtn_cikisYap_Click(object sender, EventArgs e)
        {
            try
            {
                Session["user"] = null;

                if (Request.Cookies["adminCredentials"] != null)
                {
                    HttpCookie mycookie = new HttpCookie("adminCredentials");
                    mycookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(mycookie);
                }
                Response.Write("<script>alert('Çıkış yapıldı.');</script>");
                Thread.Sleep(3000);
                Response.Redirect("~/AnaSayfa.aspx");
                //lnkbtn_login.Visible = true;
                //lnkbtn_signup.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {

            }
        }
    }
}