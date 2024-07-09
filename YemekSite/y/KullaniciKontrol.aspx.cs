using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YemekSite.y
{
    public partial class KullaniciKontrol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Functions.panelGizle(pnl_kullanicilar);
                kullanicilariGetir();
            }
        }

        protected void btn_goster_Click(object sender, EventArgs e)
        {
            Functions.panelGoster(pnl_kullanicilar);
            kullanicilariGetir();
        }

        protected void btn_gizle_Click(object sender, EventArgs e)
        {
            Functions.panelGizle(pnl_kullanicilar);
        }

        SqlClass sqlclass = new SqlClass();
        DataTable dt = new DataTable();
        private void kullanicilariGetir()
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_kullanicilar";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                
                dt.Load(oku);
                dt.Columns.Add("kullanici_fotograf_base64", typeof(string));


                // resim eklemek için base64 stringe çevirme
                foreach (DataRow row in dt.Rows)
                {
                    if (row["kullanici_fotograf"] != DBNull.Value && row["kullanici_fotograf"] is byte[])
                    {
                        byte[] resimBytes = (byte[])row["kullanici_fotograf"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        row["kullanici_fotograf_base64"] = "data:image/jpg;base64," + base64String;
                    }
                }
                dtlist_kullanicilar.DataSource = dt;
                dtlist_kullanicilar.DataBind();
                oku.Close();


            }catch(Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
            finally
            {
                sqlclass.baglanti.Close();
            }
        }
        private string getKullaniciId(object sender)
        {
            LinkButton btn = (LinkButton)sender;
            return btn.CommandArgument;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "DELETE FROM tbl_kullanicilar WHERE kullanici_id=@kullanici_id";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@kullanici_id",getKullaniciId(sender));
                komut.ExecuteNonQuery();
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/y/KullaniciKontrolDetay.aspx?kullanici_id=" + getKullaniciId(sender));
        }
    }
}