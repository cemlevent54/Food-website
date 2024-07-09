using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class YemekDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YemekVerileriniGetir();
                yorumVeriGetir();
            }
        }

        SqlClass sqlclass = new SqlClass();
        string yemek_id = "";
        private void YemekVerileriniGetir()
        {
            try
            {
                yemek_id = getYemekId();
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", yemek_id);
                SqlDataReader oku = komut.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oku); // Veritabanındaki tüm sütunları yükler

                // Base64 string olarak saklayacağız
                dt.Columns.Add("yemek_resim_base64", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["yemek_resim"] != DBNull.Value && row["yemek_resim"] is byte[])
                    {
                        byte[] resimBytes = (byte[])row["yemek_resim"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        row["yemek_resim_base64"] = "data:image/jpg;base64," + base64String;
                    }
                }

                dtlist_yemekdetay.DataSource = dt;
                dtlist_yemekdetay.DataBind();

                sqlclass.baglantiKapat();

            }
            catch (Exception ex)
            {
                Response.Write("Bir hata oluştu: " + ex.Message);
            }
        }

        private string getYemekId()
        {
            return Request.QueryString["yemek_id"];
        }

        protected void btn_yorumyap_Click(object sender, EventArgs e)
        {
            try
            {
                List<Control> controls = new List<Control>() {
                txtbox_isimsoyisim, txtbox_mail, txtbox_yorum
            };
                List<Control> controlsToClear = new List<Control>() {
                txtbox_isimsoyisim, txtbox_mail, txtbox_yorum

            };
                Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>()
            {
                { txtbox_isimsoyisim, "Name and Surname is required." },
                { txtbox_mail, "Email is required." },
                { txtbox_yorum, "Comment is required." }
            };

                if (string.IsNullOrEmpty(Functions.ValidateForm(controlsToValidate)))
                {
                    string adsoyad = txtbox_isimsoyisim.Text;
                    string mail = txtbox_mail.Text;
                    string yorum = txtbox_yorum.Text;
                    string tarih = DateTime.Now.ToString("dd-MM--yyyy");
                    bool yorumOnay = false;
                    yemek_id = getYemekId();

                    Yorumlar yeni_yorum = new Yorumlar(adsoyad, mail, tarih, yorumOnay, yorum, yemek_id);
                    yorumEkle(yeni_yorum);
                    Functions.ClearForm(controlsToClear);
                }
                else
                {
                    lblsonuc.Text = Functions.ValidateForm(controlsToValidate);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Bir hata oluştu: " + ex.Message);
            }





        }

        private void yorumEkle(Yorumlar yeni_yorum)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "INSERT INTO tbl_yorumlar (yorum_adsoyad,yorum_mail,yorum_tarih,yorum_onay,yorum_icerik,yemek_id) VALUES (@Param1,@Param2,@Param3,@Param4,@Param5,@Param6)";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                DateTime tarih = DateTime.TryParse(yeni_yorum.Tarih, out tarih) ? tarih : DateTime.Now;
                komut.Parameters.AddWithValue("@Param1", yeni_yorum.IsimSoyisim);
                komut.Parameters.AddWithValue("@Param2", yeni_yorum.Email);
                komut.Parameters.AddWithValue("@Param3", tarih);
                komut.Parameters.AddWithValue("@Param4", yeni_yorum.Onay);
                komut.Parameters.AddWithValue("@Param5", yeni_yorum.Yorum);
                komut.Parameters.AddWithValue("@Param6", yeni_yorum.Yemek_id);
                komut.ExecuteNonQuery();

                lblsonuc.Text = "Yorumunuz başarıyla eklendi.";

            }
            catch (Exception ex)
            {
                Response.Write("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        private void yorumVeriGetir()
        {
            
            try
            {
                yemek_id = getYemekId();
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yorumlar WHERE yemek_id=@Param1 AND yorum_onay=1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", yemek_id);
                SqlDataReader oku = komut.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oku); // Veritabanındaki tüm sütunları yükler

                dtlist_yorumlar.DataSource = dt;
                dtlist_yorumlar.DataBind();

                sqlclass.baglantiKapat();

            }
            catch (Exception ex)
            {
                Response.Write("Bir hata oluştu: " + ex.Message);
            }
        }
    }
}