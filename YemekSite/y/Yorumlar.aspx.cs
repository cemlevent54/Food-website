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
    public partial class Yorumlar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_onayliyorumliste);
                Functions.panelGizle(pnl_onaysizyorumliste);
            }
        }

        SqlClass sqlclass = new SqlClass();
        private void OnayliYorumlariGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yorumlar WHERE yorum_onay = 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dtlist_OnayliYorumlar.DataSource = dt;
                dtlist_OnayliYorumlar.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        private void OnaysizYorumlariGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yorumlar WHERE yorum_onay = 0";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dtlist_onaysizyorum.DataSource = dt;
                dtlist_onaysizyorum.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        private string getYorumId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            string yorumid = btn.CommandArgument;
            return yorumid;

        }

        protected void btn_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_onayliyorumliste);
            OnayliYorumlariGetir();
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_onayliyorumliste);
        }

        protected void btn_onaysiz_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_onaysizyorumliste);
            OnaysizYorumlariGetir();
        }

        protected void btn_onaysiz_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_onaysizyorumliste);
        }

        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                sqlclass.baglantiAc();
                string yorumid = getYorumId(sender);
                string sqlquery = "DELETE FROM tbl_yorumlar WHERE yorum_id = @Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", yorumid);
                komut.ExecuteNonQuery();
                lblsonuc.Text = "Yorum Silindi";

            }catch (Exception ex)
            {
                lblsonuc.Text = ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/y/YorumlarDetay.aspx?yorum_id=" + getYorumId(sender));
        }


    }
}