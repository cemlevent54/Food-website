using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace YemekSite
{
    public partial class AnaSayfa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                veriGetir();
                
            }
            
        }

        SqlClass sqlclass = new SqlClass();

        

        private void veriGetir()
        {
            try
            {

                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_onay = 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
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


                dtlist_AnaSayfa.DataSource = dt;
                dtlist_AnaSayfa.DataBind();

                sqlclass.baglantiKapat();

            }
            catch (Exception ex)
            {
                Response.Write("Bir hata oluştu: " + ex.Message);
            }
        }

        protected void dtlist_AnaSayfa_ItemDataBound(object sender, DataListItemEventArgs e)
        {


        }
    }
}