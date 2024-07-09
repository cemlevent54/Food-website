using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite
{
    public partial class GununYemegi1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gununYemeginiGetir();
            }
        }
        
        SqlClass sqlclass = new SqlClass();
        private void gununYemeginiGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_gununyemegi";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dt.Columns.Add("gununyemegi_resim_base64", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["gununyemegi_resim"] != DBNull.Value && row["gununyemegi_resim"] is byte[])
                    {
                        byte[] resimBytes = (byte[])row["gununyemegi_resim"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        row["gununyemegi_resim_base64"] = "data:image/jpg;base64," + base64String;
                    }
                }

                Label1.Text = dt.Rows[0]["gununyemegi_ad"].ToString();
                Image1.ImageUrl = dt.Rows[0]["gununyemegi_resim_base64"].ToString();
                Label2.Text = dt.Rows[0]["gununyemegi_malzeme"].ToString();
                Label3.Text = dt.Rows[0]["gununyemegi_tarif"].ToString();
                Label4.Text = dt.Rows[0]["gununyemegi_puan"].ToString();
                Label5.Text = dt.Rows[0]["gununyemegi_tarih"].ToString();
                oku.Close(); // SqlDataReader'ı kapat
            }
            catch (Exception ex)
            {
                // Hata işlemleri yapılabilir, örneğin hata mesajı gösterilebilir
                Response.Write("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat(); // Bağlantıyı kapatma işlemi finally bloğunda yapılır
            }
        }
    }
}