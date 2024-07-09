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
    public partial class HakkimizdaDuzenle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_hakkimizda);
                hakkimizdaGetir();
            }
        }
        SqlClass sqlclass = new SqlClass();
        private void hakkimizdaGetir()
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
                txtbox_hakkimizda.Text = dt.Rows[0]["hakkimizda_metin"].ToString();
            }catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btn_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_hakkimizda);
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_hakkimizda);
        }

        protected void btn_hakkimizdaguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "UPDATE tbl_hakkimizda SET hakkimizda_metin=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", txtbox_hakkimizda.Text);
                komut.ExecuteNonQuery();
                Label1.Text = "Hakkımızda metni güncellendi.";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }
    }
}