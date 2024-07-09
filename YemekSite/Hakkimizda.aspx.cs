using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace YemekSite
{
    public partial class Hakkimizda1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    sqlclass.baglantiAc();
                    string sqlquery = "SELECT * FROM tbl_hakkimizda";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    SqlDataReader oku = komut.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(oku);
                    oku.Close();
                    lbl_hakkimizda.Text = dt.Rows[0]["hakkimizda_metin"].ToString();

                }catch(Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    sqlclass.baglantiKapat();
                }
            }
        }

        SqlClass sqlclass = new SqlClass();
    }
}