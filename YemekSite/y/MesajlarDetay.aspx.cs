using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Dynamic;

namespace YemekSite.Menuler
{
    public partial class MesajlarDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string mesaj_id = getMesajId();
                MesajVerileriniGetir(mesaj_id);
            }
        }
        SqlClass sqlclass = new SqlClass();

        private string getMesajId()
        {
            return Request.QueryString["mesajlar_id"];
        }
        private void MesajVerileriniGetir(string mesajid)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_mesajlar WHERE mesajlar_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", mesajid);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();

                foreach (DataRow row in dt.Rows)
                {
                    txtbox_adsoyad.Text = row["mesajlar_adsoyad"].ToString();
                    txtbox_mail.Text = row["mesajlar_mail"].ToString();
                    txtbox_konu.Text = row["mesajlar_konu"].ToString();
                    txtbox_mesaj.Text = row["mesajlar_icerik"].ToString();
                }


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