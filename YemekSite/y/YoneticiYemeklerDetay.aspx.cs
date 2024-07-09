using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite.Menuler
{
    public partial class YoneticiYemeklerDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                kategorileriGetir();
                yemek_id = getYemekId();
                if (!string.IsNullOrEmpty(yemek_id))
                {

                    if (getYemekOnayStates(yemek_id) == true)
                    {
                        btn_guncelle.Visible = true;
                        btn_onayla.Visible = false;
                        btn_ekle.Visible = false;
                    }
                    else
                    {
                        btn_guncelle.Visible = true;
                        btn_onayla.Visible = true;
                        btn_ekle.Visible = false;
                    }
                    YemekVerileriniGetir(yemek_id);
                    
                }
            }
        }

        SqlClass sqlclass = new SqlClass();
        string yemek_id = "";
        bool yemek_onay = false;
        private string getYemekId()
        {
            string yemek_id = Request.QueryString["yemek_id"];
            return yemek_id;
        }

        private bool getYemekOnayStates(string tarifid)
        {
            sqlclass.baglantiAc();
            string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_id=@Param1";
            SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
            komut.Parameters.AddWithValue("@Param1", tarifid);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                if (oku["yemek_onay"].ToString() == "1")
                {
                    yemek_onay = true;
                }
                else
                {
                    yemek_onay = false;
                }
            }
            else
            {
                lbl_mesaj.Text = "Yemek Bulunamadı";
            }
            sqlclass.baglantiKapat();
            oku.Close();
            return yemek_onay;

        }
        
        private void YemekVerileriniGetir(string tarifid)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", tarifid);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dt.Columns.Add("yemek_resim_base64", typeof(string));

                foreach(DataRow dtrow in dt.Rows)
                {
                    txtbox_yemekadi.Text = dtrow["yemek_ad"].ToString();
                    txtbox_yemekmalzemeleri.Text = dtrow["yemek_malzeme"].ToString();
                    txtbox_yemektarifi.Text = dtrow["yemek_tarif"].ToString();

                    string yemektarihstr = dtrow["yemek_tarih"].ToString().Trim();
                    DateTime yemektarihdt;
                    string formattedDate = "";
                    if (DateTime.TryParse(yemektarihstr, out yemektarihdt))
                    {
                        // Parsing was successful, convert DateTime to string in desired format
                        formattedDate = yemektarihdt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        // Handle the error, inform the user, etc.
                        lbl_mesaj.Text = "Geçersiz tarih formatı. Lütfen geçerli bir tarih girin.";
                    }
                    txtbox_yemektarihi.Text = formattedDate.ToString();
                    txtbox_yemekpuan.Text = dtrow["yemek_puan"].ToString();

                    txtbox_kategoriId.Text = dtrow["kategori_id"].ToString();
                    HiddenField1.Value = dtrow["kategori_id"].ToString();
                    dropdown_kategoriId.SelectedValue = HiddenField1.Value;

                    if (dtrow["yemek_resim"] != DBNull.Value && dtrow["yemek_resim"] is byte[])
                    {
                        byte[] resimBytes = (byte[])dtrow["yemek_resim"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        dtrow["yemek_resim_base64"] = "data:image/jpg;base64," + base64String;
                    }
                    

                }

                DataRow dr = dt.Rows[0];
                img_yemekresim_yukleme.ImageUrl = dr["yemek_resim_base64"].ToString();


            }catch(Exception ex)
            {
                lbl_mesaj.Text = ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        private void kategorileriGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT kategori_id,kategori_ad FROM tbl_kategoriler";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                
                Dictionary<int, string> kategoriler = new Dictionary<int, string>();
                foreach(DataRow dtrow in dt.Rows)
                {
                    kategoriler.Add(int.Parse(dtrow["kategori_id"].ToString()), dtrow["kategori_ad"].ToString());
                }

                dropdown_kategoriId.Items.Clear();
                dropdown_kategoriId.DataSource = kategoriler;
                dropdown_kategoriId.DataTextField = "Value"; // This will display the category names
                dropdown_kategoriId.DataValueField = "Key";  // This will keep the category IDs as values
                dropdown_kategoriId.DataBind();
                

            }
            catch(Exception ex)
            {
                lbl_mesaj.Text = ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }
        
        protected void btn_guncelle_Click(object sender, EventArgs e)
        {
            Dictionary<Control,string> controlsToValidate = new Dictionary<Control, string>() {
                {txtbox_yemekadi,"Yemek Adı Boş Geçilemez"},
                {txtbox_yemekmalzemeleri,"Yemek Malzemeleri Boş Geçilemez"},
                {txtbox_yemektarifi,"Yemek Tarifi Boş Geçilemez"},
                {txtbox_yemektarihi,"Yemek Tarihi Boş Geçilemez"},
                {txtbox_yemekpuan,"Yemek Puanı Boş Geçilemez"},
                {txtbox_kategoriId,"Kategori Id Boş Geçilemez"}
            };
            try
            {
                string yemekadi = txtbox_yemekadi.Text;
                string yemekmalzemeleri = txtbox_yemekmalzemeleri.Text;
                string yemektarifi = txtbox_yemektarifi.Text;
                string yemektarihi = txtbox_yemektarihi.Text;
                float yemekpuan = float.Parse(txtbox_yemekpuan.Text);
                string kategoriId = txtbox_kategoriId.Text;
                string katid = dropdown_kategoriId.SelectedValue;
                string resim = HiddenField2.Value;

                if(Functions.ValidateForm(controlsToValidate) == "")
                {
                    string yemek_id = getYemekId();
                    sqlclass.baglantiAc();
                    string sqlquery = "UPDATE tbl_yemekler " +
                        "SET yemek_ad=@Param1, yemek_malzeme=@Param2, yemek_tarif=@Param3, yemek_tarih=@Param4," +
                        "yemek_puan=@Param5,kategori_id=@Param6, yemek_resim=@Param8 WHERE yemek_id=@Param7";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", yemekadi);
                    komut.Parameters.AddWithValue("@Param2", yemekmalzemeleri);
                    komut.Parameters.AddWithValue("@Param3", yemektarifi);
                    DateTime.TryParse(yemektarihi, out DateTime yemektarihidt);
                    komut.Parameters.AddWithValue("@Param4", yemektarihidt);
                    komut.Parameters.AddWithValue("@Param5", yemekpuan);
                    komut.Parameters.AddWithValue("@Param6", kategoriId);
                    komut.Parameters.AddWithValue("@Param7", yemek_id);
                    //// Convert base64 image string to byte array
                    byte[] k_resim = Convert.FromBase64String(resim.Split(',')[1]);
                    komut.Parameters.Add("@Param8", SqlDbType.Image).Value = k_resim;

                    komut.ExecuteNonQuery();
                    lbl_mesaj.Text = "Yemek Güncellendi";
                    
                }
                else
                {
                    lbl_mesaj.Text = Functions.ValidateForm(controlsToValidate);
                }

            }catch(Exception ex)
            {
                lbl_mesaj.Text = ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_onayla_Click(object sender, EventArgs e)
        {
            Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                {txtbox_yemekadi,"Yemek Adı Boş Geçilemez"},
                {txtbox_yemekmalzemeleri,"Yemek Malzemeleri Boş Geçilemez"},
                {txtbox_yemektarifi,"Yemek Tarifi Boş Geçilemez"},
                {txtbox_yemektarihi,"Yemek Tarihi Boş Geçilemez"},
                {txtbox_yemekpuan,"Yemek Puanı Boş Geçilemez"},
                {txtbox_kategoriId,"Kategori Id Boş Geçilemez"}
            };
            try
            {
                string yemekadi = txtbox_yemekadi.Text;
                string yemekmalzemeleri = txtbox_yemekmalzemeleri.Text;
                string yemektarifi = txtbox_yemektarifi.Text;
                string yemektarihi = txtbox_yemektarihi.Text;
                float yemekpuan = float.Parse(txtbox_yemekpuan.Text);
                string kategoriId = txtbox_kategoriId.Text;

                if (Functions.ValidateForm(controlsToValidate) == "")
                {
                    string yemek_id = getYemekId();
                    sqlclass.baglantiAc();
                    string sqlquery = "UPDATE tbl_yemekler SET yemek_onay= 1 WHERE yemek_id=@Param2";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param2", yemek_id);
                    komut.ExecuteNonQuery();
                    lbl_mesaj.Text = "Yemek Onaylandi.";

                    // kategoride bulunan miktari artırma
                    string sqlquery2 = "UPDATE tbl_kategoriler SET kategori_adet = kategori_adet + 1 WHERE kategori_id=@Param1";
                    SqlCommand komut2 = new SqlCommand(sqlquery2, sqlclass.baglanti);
                    komut2.Parameters.AddWithValue("@Param1", kategoriId);
                    komut2.ExecuteNonQuery();


                }
                else
                {
                    lbl_mesaj.Text = Functions.ValidateForm(controlsToValidate);
                }

            }
            catch (Exception ex)
            {
                lbl_mesaj.Text = ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_ekle_Click(object sender, EventArgs e)
        {

        }

        protected void dropdown_kategoriId_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenField1.Value = dropdown_kategoriId.SelectedValue;
            txtbox_kategoriId.Text = dropdown_kategoriId.SelectedValue;

        }

        protected void btn_resimekle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, lbl_mesaj, HiddenField2);
            img_yemekresim_yukleme.ImageUrl = HiddenField2.Value;
        }
    }
}