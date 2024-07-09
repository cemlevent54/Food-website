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
    public partial class KategoriDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                kategoriDetayGetir();
            }
        }

        SqlClass sqlclass = new SqlClass();
        string kategori_id = "";
        private string getKategoriId()
        {
            string kategoriId = Request.QueryString["kategori_id"];
            return kategoriId;
        }

        private void kategoriDetayGetir()
        {
            SqlDataReader oku = null;
            kategori_id = getKategoriId();
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_kategoriler WHERE kategori_id = @Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kategori_id);
                oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();
                
                dt.Columns.Add("kategori_resim_base64", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    if (row["kategori_resim"] != DBNull.Value && row["kategori_resim"] is byte[])
                    {
                        byte[] resimBytes = (byte[])row["kategori_resim"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        row["kategori_resim_base64"] = "data:image/jpg;base64," + base64String;
                    }
                }
                DataRow dr = dt.Rows[0];
                txtbox_kategoriad.Text = dr["kategori_ad"].ToString().Trim();
                Image1.ImageUrl = dr["kategori_resim_base64"].ToString();

                

            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                if (oku != null)
                {
                    oku.Close();
                }
                sqlclass.baglantiKapat();
            }
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                kategori_id = Request.QueryString["kategori_id"];
                sqlclass.baglantiAc();
                if(string.IsNullOrEmpty(txtbox_kategoriad.Text.Trim()))
                {
                    return;
                }
                string resim = HiddenField1.Value;
                string kategori_ad = txtbox_kategoriad.Text.Trim();
                string sqlquery = "UPDATE tbl_kategoriler SET kategori_ad = @Param1, kategori_resim=@Param3 WHERE kategori_id = @Param2";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kategori_ad);
                //// Convert base64 image string to byte array
                byte[] k_resim = Convert.FromBase64String(resim.Split(',')[1]);
                komut.Parameters.Add("@Param3", SqlDbType.Image).Value = k_resim;
                komut.Parameters.AddWithValue("@Param2", kategori_id);
                komut.ExecuteNonQuery();
                Label1.Text = "Güncelleme işlemi başarılı";
                
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

        private void resimiGetir()
        {

        }

        protected void btn_resimyukle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, Label1, HiddenField1);
            Image1.ImageUrl = HiddenField1.Value;
        }
    }
}