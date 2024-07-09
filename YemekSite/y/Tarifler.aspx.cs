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
    public partial class Tarifler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_onaylitarifliste);
                Functions.panelGizle(pnl_onaysiztarifliste);
            }
        }

        private void OnaysizTarifleriGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_tarif WHERE tarif_onay = 0";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                if (oku != null)
                {
                    dt.Load(oku);
                    dtlist_OnaysizTarifler.DataSource = dt;
                    dtlist_OnaysizTarifler.DataBind();
                }

            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        private void OnayliTarifleriGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_tarif WHERE tarif_onay = 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                if (oku != null)
                {
                    dt.Load(oku);
                    dtlist_OnayliTarifler.DataSource = dt;
                    dtlist_OnayliTarifler.DataBind();
                }

            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }
        protected void btn_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_onaylitarifliste);
            OnayliTarifleriGetir();
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_onaylitarifliste);
        }

        protected void btn_gosterme_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_onaysiztarifliste);
            OnaysizTarifleriGetir();
        }

        protected void btn_gizleme_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_onaysiztarifliste);
        }
        
        SqlClass sqlclass = new SqlClass();

        private string getTarifId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            string tarifId = btn.CommandArgument;
            return tarifId;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string tarifId = getTarifId(sender);
                sqlclass.baglantiAc();
                string sqlquery = "DELETE FROM tbl_tarif WHERE tarif_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", tarifId);
                komut.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/y/TarifDetay.aspx?tarif_id=" + getTarifId(sender));
        }
    }
}