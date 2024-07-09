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
    public partial class GununYemegiSecme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_yemekListesi);
                YemekleriGetir();
            }
        }
        SqlClass sqlclass = new SqlClass();
        private string getYemekId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            return btn.CommandArgument;
        }

        private void YemekleriGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_yemekler WHERE yemek_onay = 1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                dtlist_Yemekler.DataSource = dt;
                dtlist_Yemekler.DataBind();


            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();

            }
        }

        private bool satir_kontrol = false;
        private bool gununYemegiTabloRowControl()
        {

            // varsa 1 yoksa 0 döndür
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT COUNT(*) FROM tbl_gununyemegi";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                int sayi = Convert.ToInt32(komut.ExecuteScalar());
                if(sayi == 0)
                {
                    satir_kontrol = false;
                }
                else
                {
                    satir_kontrol = true;
                }



            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }

            return satir_kontrol;
        }
        
        protected void btn_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_yemekListesi);
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_yemekListesi);
        }

        protected void btnYemekSec_Click(object sender, EventArgs e)
        {
            try
            {
                if(gununYemegiTabloRowControl())
                {
                    sqlclass.baglantiAc();
                    string sqlquery = "DELETE FROM tbl_gununyemegi";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.ExecuteNonQuery();

                    string sqlqueryekle = "INSERT INTO tbl_gununyemegi " +
                        "(gununyemegi_ad,gununyemegi_malzeme,gununyemegi_tarif,gununyemegi_puan,gununyemegi_resim,gununyemegi_tarih) " +
                        "SELECT yemek_ad,yemek_malzeme,yemek_tarif,yemek_puan,yemek_resim,yemek_tarih FROM tbl_yemekler " +
                        "WHERE yemek_id = @Param1;";
                    SqlCommand komutekleme = new SqlCommand(sqlqueryekle, sqlclass.baglanti);
                    komutekleme.Parameters.AddWithValue("@Param1", getYemekId(sender));
                    komutekleme.ExecuteNonQuery();
                    lblsonuc.Text = "Yemek Seçildi";
                }
                else
                {
                    sqlclass.baglantiAc();
                    string sqlqueryekle = "INSERT INTO tbl_gununyemegi " +
                        "(gununyemegi_ad,gununyemegi_malzeme,gununyemegi_tarif,gununyemegi_puan,gununyemegi_resim,gununyemegi_tarih) " +
                        "SELECT yemek_ad,yemek_malzeme,yemek_tarif,yemek_puan,yemek_resim,yemek_tarih FROM tbl_yemekler " +
                        "WHERE yemek_id = @Param1;";
                    SqlCommand komutekleme = new SqlCommand(sqlqueryekle, sqlclass.baglanti);
                    komutekleme.Parameters.AddWithValue("@Param1", getYemekId(sender));
                    komutekleme.ExecuteNonQuery();
                    lblsonuc.Text = "Yemek Seçildi";
                }


            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }
    }
}