using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite.Menuler
{
    public partial class Kategoriler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                pnl_kategoriListesi.Visible = false;
                pnl_kategoriEkleme.Visible = false;
            }
        }

        protected void btn_kategorigoster_Click(object sender, EventArgs e)
        {
            pnl_kategoriListesi.Visible = true;
            kategorileriGetir();
            
        }

        protected void btn_kategorigizle_Click(object sender, EventArgs e)
        {
            pnl_kategoriListesi.Visible = false;
        }

        private void kategorileriGetir()
        {
            
            sqlclass.baglantiAc();
            string sqlquery = "SELECT * FROM tbl_kategoriler";
            SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(oku);

            dtlist_Kategori.DataSource = dt;
            dtlist_Kategori.DataBind();
            sqlclass.baglantiKapat();

        }
        private string getKategoriId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            string kategoriId = btn.CommandArgument;
            return kategoriId;
        }

        SqlClass sqlclass = new SqlClass();
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string kategori_id = getKategoriId(sender);

            // Check if kategori_id is valid
            if (string.IsNullOrEmpty(kategori_id))
            {
                //Response.Write("Kategori ID geçersiz.");
                ;
                return;
            }

            sqlclass.baglantiAc();

            try
            {
                string sqlquery = "DELETE FROM tbl_kategoriler WHERE kategori_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kategori_id);

                // Log the kategori_id for debugging
                //Response.Write("Kategori ID: " + kategori_id + "<br/>");

                int rowsAffected = komut.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    //Response.Write("Silme işlemi başarılı");
                    ;
                }
                else
                {
                    //Response.Write("Silme işlemi sırasında bir hata oluştu: Kategori bulunamadı.");
                    ;
                }

            }
            catch (Exception ex)
            {
                Response.Write("Silme işlemi sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //href = '<%# "YemekDetay.aspx?yemek_id=" + Eval("yemek_id") %>' >
            string kategori_id = getKategoriId(sender);
            Response.Redirect("~/y/KategoriDetay.aspx?kategori_id=" +kategori_id);
        }

        protected void btn_goster_Click(object sender, EventArgs e)
        {
            pnl_kategoriEkleme.Visible = true;
            lblsonuc.Text = "";
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            pnl_kategoriEkleme.Visible = false;
            lblsonuc.Text = "";
        }


        protected void btn_KategoriEkle_Click(object sender, EventArgs e)
        {
            
            try
            {
                if(string.IsNullOrEmpty(txt_kategoriAd.Text))
                {
                    lblsonuc.Text = "Kategori adı boş olamaz";
                    return;
                }
                string kategoriad = txt_kategoriAd.Text.Trim();
                string resim = HiddenField1.Value;
                sqlclass.baglantiAc();
                string sqlquery = "INSERT INTO tbl_kategoriler (kategori_ad,kategori_resim,kategori_adet) VALUES (@Param1,@Param2,0)";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kategoriad);
                //// Convert base64 image string to byte array
                byte[] k_resim = Convert.FromBase64String(resim.Split(',')[1]);
                komut.Parameters.Add("@Param2", SqlDbType.Image).Value = k_resim;
                komut.ExecuteNonQuery();
                lblsonuc.Text = "Kategori ekleme işlemi başarılı";
                txt_kategoriAd.Text = "";
                Image1.ImageUrl = "";
            }catch(Exception ex)
            {
                Response.Write("Kategori ekleme sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_resimyukle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, lblsonuc, HiddenField1);
            Image1.ImageUrl = HiddenField1.Value;
        }
    }
}