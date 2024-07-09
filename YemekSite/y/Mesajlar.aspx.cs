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
    public partial class Mesajlar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_mesajlar);
                MesajVerileriniGetir();
            }
        }
        SqlClass sqlclass = new SqlClass();
        private string getMesajId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            return btn.CommandArgument;
        }

        private void MesajVerileriniGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT mesajlar_id,mesajlar_adsoyad FROM tbl_mesajlar";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dtlist_mesajlar.DataSource = dt;
                dtlist_mesajlar.DataBind();
                oku.Close();


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
            Functions.panelGoster(pnl_mesajlar);
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_mesajlar);
        }

        
        protected void btnView_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/y/MesajlarDetay.aspx?mesajlar_id=" + getMesajId(sender));
        }
    }
}