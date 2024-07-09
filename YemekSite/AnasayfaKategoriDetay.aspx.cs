using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace YemekSite.AnasayfaPages
{
    public partial class KategoriDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YemekVerileriniGetir();
            }
        }
        DataTable dt = new DataTable();
        SqlClass sqlclass = new SqlClass();
        private string getKategoriId()
        {
            return Request.QueryString["kategori_id"];
        }

        private void YemekVerileriniGetir()
        {
            try
            {
                string kategori_id = Request.QueryString["kategori_id"];
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE kategori_id=@Param1 AND yemek_onay = 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", kategori_id);
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
                dtlist_KategoriDetay.DataSource = dt;
                dtlist_KategoriDetay.DataBind();
                sqlclass.baglantiKapat();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }   

        private void KategoriResminiGetir()
        {
            // burayi yonetici panelinden kategori resmi ekleyince yapacagiz
        }
    }
}