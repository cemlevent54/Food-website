using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using System.Data.SqlClient;
using System.Data;

namespace YemekSite.Menuler
{
    public partial class YoneticiYemekler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_onayliyemekliste);
                Functions.panelGizle(pnl_onaysizyemekliste);
                Functions.panelGizle(pnl_yemekekle);
               
            }
        }

        SqlClass sqlclass = new SqlClass();

        protected void btn_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_onayliyemekliste);
            OnayliYemekVerileriniGetir();
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_onayliyemekliste);
        }

        protected void btn_gosterme_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_onaysizyemekliste);
            OnaysizYemekVerileriniGetir();
        }

        protected void btn_gizleme_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_onaysizyemekliste);
        }

        protected void btn_yemekgoster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_yemekekle);
            kategorileriGetir();
        }

        protected void btn_yemekgizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_yemekekle);
        }
        private string getYemekId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            string tarifId = btn.CommandArgument;
            return tarifId;
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string tarifid = getYemekId(sender);
                sqlclass.baglantiAc();
                string sqlquery = "DELETE FROM tbl_yemekler WHERE yemek_id=@Param1";
                SqlCommand komutsil = new SqlCommand(sqlquery, sqlclass.baglanti);
                komutsil.Parameters.AddWithValue("@Param1", tarifid);
                komutsil.ExecuteNonQuery();
                lblsonuc.Text = "Silme işlemi başarılı";

            }catch(Exception ex)
            {
                lblsonuc.Text = "Hata: " + ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/y/YoneticiYemeklerDetay.aspx?yemek_id=" + getYemekId(sender));
        }

        private void OnayliYemekVerileriniGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_onay = 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                if(oku != null)
                {
                    dt.Load(oku);
                    dtlist_OnayliYemekler.DataSource = dt;
                    dtlist_OnayliYemekler.DataBind();
                    oku.Close();
                }
                
            }
            catch(Exception ex)
            {
                lblsonuc.Text = "Hata: " + ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

        }

        private void OnaysizYemekVerileriniGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_onay = 0";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                if (oku != null)
                {
                    dt.Load(oku);
                    dtlist_OnaysizYemekler.DataSource = dt;
                    dtlist_OnaysizYemekler.DataBind();
                    oku.Close();
                }

            }
            catch (Exception ex)
            {
                lblsonuc.Text = "Hata: " + ex.Message.ToString();
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
                foreach (DataRow dtrow in dt.Rows)
                {
                    kategoriler.Add(int.Parse(dtrow["kategori_id"].ToString()), dtrow["kategori_ad"].ToString());
                }

                dropdown_kategoriId.Items.Clear();
                dropdown_kategoriId.DataSource = kategoriler;
                dropdown_kategoriId.DataTextField = "Value"; // This will display the category names
                dropdown_kategoriId.DataValueField = "Key";  // This will keep the category IDs as values
                dropdown_kategoriId.DataBind();


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
            Dictionary<Control, string> controlstovalidate = new Dictionary<Control, string>() {

                    { txtbox_yemekadi, "Yemek adı boş olamaz" },
                    { txtbox_yemekmalzemeleri, "Yemek malzemesi boş olamaz" },
                    { txtbox_yemektarifi, "Yemek tarifi boş olamaz" },
                    { img_yemekresim_yukleme, "Yemek resmi boş olamaz" },
                    {txtbox_yemektarihi, "Yemek tarihi boş olamaz" },
                    { dropdown_kategoriId, "Kategori seçimi yapınız" }
            };

            try
            {
                string yemekadi = txtbox_yemekadi.Text;
                string yemekmalzemeleri = txtbox_yemekmalzemeleri.Text;
                string yemektarifi = txtbox_yemektarifi.Text;
                float yemekpuan = float.Parse(txtbox_yemekpuan.Text);
                DateTime yemektarih = Convert.ToDateTime(txtbox_yemektarihi.Text);
                int kategoriId = Convert.ToInt32(dropdown_kategoriId.SelectedValue);
                bool yemek_onay = false;

                if(Functions.ValidateForm(controlstovalidate) == "")
                {
                    sqlclass.baglantiAc();
                    string sqlquery = "INSERT INTO tbl_yemekler" +
                        "(yemek_ad,yemek_malzeme,yemek_tarif,yemek_puan,yemek_tarih,kategori_id,yemek_onay,yemek_resim) " +
                        "VALUES(@Param1,@Param2,@Param3,@Param4,@Param5,@Param6,@Param7,@Param8)";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", yemekadi);
                    komut.Parameters.AddWithValue("@Param2", yemekmalzemeleri);
                    komut.Parameters.AddWithValue("@Param3", yemektarifi);
                    komut.Parameters.AddWithValue("@Param4", yemekpuan);
                    komut.Parameters.AddWithValue("@Param5", yemektarih);
                    komut.Parameters.AddWithValue("@Param6", kategoriId);
                    komut.Parameters.AddWithValue("@Param7", yemek_onay);
                    byte[] resim = Convert.FromBase64String(HiddenField2.Value.Split(',')[1]);
                    komut.Parameters.Add("@Param8", SqlDbType.Image).Value = resim;
                    komut.ExecuteNonQuery();
                }
                else
                {
                    lbl_mesaj.Text = Functions.ValidateForm(controlstovalidate);
                }
                

            }
            catch(Exception ex)
            {
                lblsonuc.Text = "Hata: " + ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiAc();
            }
        }

        protected void btn_resimekle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, lbl_mesaj, HiddenField2);
            resimGosterme(HiddenField2);
        }

        private void resimGosterme(HiddenField hdfield)
        {
            img_yemekresim_yukleme.ImageUrl = hdfield.Value;
        }
    }
}