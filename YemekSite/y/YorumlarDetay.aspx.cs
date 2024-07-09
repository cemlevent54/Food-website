using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace YemekSite.Menuler
{
    public partial class YorumlarDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                yorum_id = getYorumId();
                yemekKategoriGetir();
                if(!string.IsNullOrEmpty(yorum_id))
                {
                    if (onayYorumKontrol(yorum_id) == true)
                    {
                        btn_yorumguncelle.Visible = true;
                        btn_yorumonayla.Visible = false;
                    }
                    else
                    {
                        btn_yorumguncelle.Visible = true;
                        btn_yorumonayla.Visible = true;
                    }
                    YorumGetir(yorum_id);
                    HiddenField1.Value = dropdown_yemekler.SelectedValue;
                }
                
                
                
            }

            
        }

        SqlClass sqlclass = new SqlClass();
        bool yorum_onay = false;
        string yorum_id = "";
        private string getYorumId()
        {
            string yorumid = Request.QueryString["yorum_id"];
            return yorumid;
        }
        private bool onayYorumKontrol(string yorumid)
        {

            sqlclass.baglantiAc();
            string sqlquery = "SELECT yorum_onay FROM tbl_yorumlar WHERE yorum_id = @Param1";
            SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
            komut.Parameters.AddWithValue("@Param1", yorumid);
            SqlDataReader oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(oku);
            foreach(DataRow dr in dt.Rows)
            {
                yorum_onay = Convert.ToBoolean(dr["yorum_onay"]);
            }
            sqlclass.baglantiKapat();

            return yorum_onay;
            
        }

        private void yemekKategoriGetir()
        {
            sqlclass.baglantiAc();
            string sqlquery = "SELECT yemek_id,yemek_ad FROM tbl_yemekler";
            SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(oku);
            oku.Close();
            Dictionary<int,string> yemekkategoriler = new Dictionary<int, string>();
            foreach(DataRow dr in dt.Rows)
            {
                yemekkategoriler.Add(Convert.ToInt32(dr["yemek_id"]), dr["yemek_ad"].ToString());
            }
            dropdown_yemekler.DataSource = yemekkategoriler;
            dropdown_yemekler.DataTextField = "Value";
            dropdown_yemekler.DataValueField = "Key";
            dropdown_yemekler.DataBind();

        }
        private void YorumGetir(string yorumid)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yorumlar WHERE yorum_id = @Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", yorumid);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);

                foreach(DataRow dr in dt.Rows)
                {
                    txtbox_adsoyad.Text = dr["yorum_adsoyad"].ToString();
                    txtbox_mail.Text = dr["yorum_mail"].ToString();
                    txtbox_yorumtarih.Text = dr["yorum_tarih"].ToString();
                    txtbox_yorum.Text = dr["yorum_icerik"].ToString();
                    HiddenField1.Value = dr["yemek_id"].ToString();
                    dropdown_yemekler.SelectedValue = dr["yemek_id"].ToString();
                    yorum_onay = Convert.ToBoolean(dr["yorum_onay"]);

                }

            }catch(Exception ex)
            {
                lbl_sonuc.Text = ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }


        protected void btn_yorumguncelle_Click(object sender, EventArgs e)
        {
            string yorumid = getYorumId();
            string adsoyad = txtbox_adsoyad.Text;
            string mail = txtbox_mail.Text;
            DateTime yorumtarih = Convert.ToDateTime(txtbox_yorumtarih.Text);
            string yorum = txtbox_yorum.Text;
            string yemekid = dropdown_yemekler.SelectedValue;
            int foodid;
            

            // İlk öğenin geçerli bir seçim olup olmadığını kontrol edin
            if (yemekid == "0" || !int.TryParse(yemekid, out foodid))
            {
                Response.Write("Lütfen geçerli bir yemek seçimi yapınız.");
                return;
            }

            Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                {txtbox_adsoyad,"Ad Soyad "},
                {txtbox_mail,"Mail "},
                {txtbox_yorumtarih,"Yorum Tarihi "},
                {txtbox_yorum,"Yorum "}
                //{dropdown_yemekler,"Yemek Seçimi "}
            };

            try
            {
                sqlclass.baglantiAc();
                if (Functions.ValidateForm(controlsToValidate) == "" && Functions.IsValidEmail(mail))
                {
                    string sqlquery = "UPDATE tbl_yorumlar " +
                        "SET yorum_adsoyad = @Param1, yorum_mail = @Param2, yorum_tarih = @Param3, yorum_icerik = @Param4, yemek_id = @Param5 " +
                        "WHERE yorum_id = @Param6";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", adsoyad);
                    komut.Parameters.AddWithValue("@Param2", mail);
                    komut.Parameters.AddWithValue("@Param3", yorumtarih);
                    komut.Parameters.AddWithValue("@Param4", yorum);
                    komut.Parameters.AddWithValue("@Param5", HiddenField1.Value);
                    komut.Parameters.AddWithValue("@Param6", yorumid);
                    komut.ExecuteNonQuery();
                    lbl_sonuc.Text = "Yorum Güncellendi";
                }
                else
                {
                    lbl_sonuc.Text = Functions.ValidateForm(controlsToValidate);
                }
            }
            catch (Exception ex)
            {
                lbl_sonuc.Text = ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_yorumonayla_Click(object sender, EventArgs e)
        {
            string yorumid = getYorumId();
            string mail = txtbox_mail.Text;
            Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                {txtbox_adsoyad,"Ad Soyad "},
                {txtbox_mail,"Mail "},
                {txtbox_yorumtarih,"Yorum Tarihi "},
                {txtbox_yorum,"Yorum "},
                //{dropdown_yemekler,"Yemek Seçimi "}

            };
            try
            {
                sqlclass.baglantiAc();
                if (Functions.ValidateForm(controlsToValidate) == "" && Functions.IsValidEmail(mail))
                {
                    string sqlquery = "UPDATE tbl_yorumlar " +
                        "SET yorum_onay = 1 " +
                        "WHERE yorum_id = @Param1";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", yorumid);
                    komut.ExecuteNonQuery();
                    lbl_sonuc.Text = "Yorum Onaylandı";
                }
                else
                {
                    lbl_sonuc.Text = Functions.ValidateForm(controlsToValidate);
                }


            }
            catch (Exception ex)
            {
                lbl_sonuc.Text = ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void dropdown_yemekler_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenField1.Value = dropdown_yemekler.SelectedValue;
        }
    }
}