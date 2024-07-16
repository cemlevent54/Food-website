using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Collections;
using System.Data.Common;

namespace YemekSite.Menuler
{
    public partial class TarifDetay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tarif_id = getTarifId();
                if (!string.IsNullOrEmpty(getTarifId()))
                {
                    tarif_onay = getTarifOnayStates(tarif_id);
                    if (tarif_onay == false)
                    {
                        btn_tarifOnayla.Visible = true;
                        btn_tarifGuncelle.Visible = true;
                        btn_tarifiEkle.Visible = false;
                    }
                    else if (tarif_onay == true)
                    {
                        btn_tarifOnayla.Visible = false;
                        btn_tarifGuncelle.Visible = true;
                        btn_tarifiEkle.Visible = true;
                    }

                }
                getAllInfosAboutTarif(tarif_id);
            }
        }

        string tarif_id = "";
        bool tarif_onay = false;
        SqlClass sqlclass = new SqlClass();
        private bool getTarifOnayStates(string tarif_id)
        {
            bool tarif_onay = false;
            sqlclass.baglantiAc();
            string sqlquery = "SELECT tarif_onay FROM tbl_tarif WHERE tarif_id = @Param1";
            SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
            komut.Parameters.AddWithValue("@Param1", tarif_id);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                tarif_onay = (bool)oku["tarif_onay"];
            }
            sqlclass.baglantiKapat();
            oku.Close();
            return tarif_onay;

        }
        private string getTarifId()
        {
            string tarif_id = Request.QueryString["tarif_id"];
            return tarif_id;
        }

        private void getAllInfosAboutTarif(string tarifid)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "SELECT * FROM tbl_tarif WHERE tarif_id=@Param1";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                komut.Parameters.AddWithValue("@Param1", tarifid);
                SqlDataReader oku = komut.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(oku);
                oku.Close();
                // Base64 string olarak saklayacağız
                dt.Columns.Add("tarif_resim_base64", typeof(string));


                // resim eklemek için base64 stringe çevirme
                foreach (DataRow row in dt.Rows)
                {
                    if (row["tarif_resim"] != DBNull.Value && row["tarif_resim"] is byte[])
                    {
                        byte[] resimBytes = (byte[])row["tarif_resim"];
                        string base64String = Convert.ToBase64String(resimBytes);
                        row["tarif_resim_base64"] = "data:image/jpg;base64," + base64String;
                    }
                }

                // 
                foreach (DataRow row in dt.Rows)
                {
                    txtbox_tarifad.Text = row["tarif_ad"].ToString();
                    txtbox_tarifmalzemeleri.Text = row["tarif_malzeme"].ToString();
                    txtbox_tarifyapilisi.Text = row["tarif_yapilis"].ToString();
                    txtbox_tarifsahibi.Text = row["tarif_sahip"].ToString();
                    txtbox_tarifsahibimail.Text = row["tarif_sahipmail"].ToString();
                    HiddenField1.Value = row["tarif_resim_base64"].ToString();


                }

                img_yemekresim_yukleme.ImageUrl = HiddenField1.Value;




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

        protected void btn_tarifGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                    {txtbox_tarifad, "Tarif adı boş bırakılamaz."},
                    {txtbox_tarifmalzemeleri, "Tarif malzemeleri boş bırakılamaz."},
                    {txtbox_tarifyapilisi, "Tarif yapılışı boş bırakılamaz."},
                    {txtbox_tarifsahibi, "Tarif sahibi boş bırakılamaz."},
                    {txtbox_tarifsahibimail, "Tarif sahibi mail boş bırakılamaz."}
                };

                if (Functions.ValidateForm(controlsToValidate) != "")
                {
                    lbl_mesaj.Text = Functions.ValidateForm(controlsToValidate);
                    return;
                }
                else
                {
                    string tarif_adi = txtbox_tarifad.Text.Trim();
                    string tarif_malzemeleri = txtbox_tarifmalzemeleri.Text.Trim();
                    string tarif_yapilisi = txtbox_tarifyapilisi.Text.Trim();
                    string tarif_sahibi = txtbox_tarifsahibi.Text.Trim();
                    string tarif_sahipmail = txtbox_tarifsahibimail.Text.Trim();
                    string resim = HiddenField1.Value;
                    string tarifid = getTarifId();
                    if (!Functions.IsValidEmail(tarif_sahipmail))
                    {
                        lbl_mesaj.Text = "Email alanı geçerli değil.";
                        return;
                    }
                    sqlclass.baglantiAc();
                    string sqlquery = "UPDATE tbl_tarif " +
                        "SET tarif_ad=@Param1,tarif_malzeme=@Param2,tarif_yapilis=@Param3,tarif_sahip=@Param4," +
                        "tarif_sahipmail=@Param5,tarif_resim=@Param8 WHERE tarif_id=@Param7";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", tarif_adi);
                    komut.Parameters.AddWithValue("@Param2", tarif_malzemeleri);
                    komut.Parameters.AddWithValue("@Param3", tarif_yapilisi);
                    komut.Parameters.AddWithValue("@Param4", tarif_sahibi);
                    komut.Parameters.AddWithValue("@Param5", tarif_sahipmail);
                    komut.Parameters.AddWithValue("@Param7", tarifid);
                    //// Convert base64 image string to byte array
                    byte[] k_resim = Convert.FromBase64String(resim.Split(',')[1]);
                    komut.Parameters.Add("@Param8", SqlDbType.Image).Value = k_resim;
                    komut.ExecuteNonQuery();
                    lbl_mesaj.Text = "Tarif güncellendi.";
                }

            }
            catch (Exception ex)
            {
                lbl_mesaj.Text = "Tarif güncellenemedi. Hata : " + ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
                List<Control> controlsToClear = new List<Control>()
                {
                    txtbox_tarifad,
                    txtbox_tarifmalzemeleri,
                    txtbox_tarifyapilisi,
                    txtbox_tarifsahibi,
                    txtbox_tarifsahibimail
                };
                Functions.ClearForm(controlsToClear);
            }
        }



        protected void btn_tarifOnayla_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                    {txtbox_tarifad, "Tarif adı boş bırakılamaz."},
                    {txtbox_tarifmalzemeleri, "Tarif malzemeleri boş bırakılamaz."},
                    {txtbox_tarifyapilisi, "Tarif yapılışı boş bırakılamaz."},
                    {txtbox_tarifsahibi, "Tarif sahibi boş bırakılamaz."},
                    {txtbox_tarifsahibimail, "Tarif sahibi mail boş bırakılamaz."}
                };

                if (Functions.ValidateForm(controlsToValidate) != "")
                {
                    lbl_mesaj.Text = Functions.ValidateForm(controlsToValidate);
                    return;
                }
                else
                {
                    string tarif_adi = txtbox_tarifad.Text;
                    string tarif_malzemeleri = txtbox_tarifmalzemeleri.Text;
                    string tarif_yapilisi = txtbox_tarifyapilisi.Text;
                    string tarif_sahibi = txtbox_tarifsahibi.Text;
                    string tarif_sahipmail = txtbox_tarifsahibimail.Text;
                    string tarifid = getTarifId();
                    bool tarifonayi = true;
                    if (!Functions.IsValidEmail(tarif_sahipmail))
                    {
                        lbl_mesaj.Text = "Email alanı geçerli değil.";
                        return;
                    }
                    sqlclass.baglantiAc();
                    string sqlquery = "UPDATE tbl_tarif SET tarif_ad=@Param1,tarif_malzeme=@Param2,tarif_yapilis=@Param3,tarif_sahip=@Param4,tarif_sahipmail=@Param5,tarif_onay=@Param6 WHERE tarif_id=@Param7";
                    SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut.Parameters.AddWithValue("@Param1", tarif_adi);
                    komut.Parameters.AddWithValue("@Param2", tarif_malzemeleri);
                    komut.Parameters.AddWithValue("@Param3", tarif_yapilisi);
                    komut.Parameters.AddWithValue("@Param4", tarif_sahibi);
                    komut.Parameters.AddWithValue("@Param5", tarif_sahipmail);
                    komut.Parameters.AddWithValue("@Param6", tarifonayi);
                    komut.Parameters.AddWithValue("@Param7", tarifid);
                    komut.ExecuteNonQuery();
                    lbl_mesaj.Text = "Tarif onaylandı.";
                }
                
                

            }
            catch (Exception ex)
            {
                lbl_mesaj.Text = "Tarif onaylanamadı. Hata : " + ex.Message.ToString();
            }
            finally
            {
                sqlclass.baglantiKapat();
                List<Control> controlsToClear = new List<Control>()
                {
                    txtbox_tarifad,
                    txtbox_tarifmalzemeleri,
                    txtbox_tarifyapilisi, 
                    txtbox_tarifsahibi, 
                    txtbox_tarifsahibimail
                };
                Functions.ClearForm(controlsToClear);
            }
        }

        protected void btn_tarifiEkle_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<Control, string> controlsToValidate = new Dictionary<Control, string>() {
                    {txtbox_tarifad, "Tarif adı boş bırakılamaz."},
                    {txtbox_tarifmalzemeleri, "Tarif malzemeleri boş bırakılamaz."},
                    {txtbox_tarifyapilisi, "Tarif yapılışı boş bırakılamaz."},
                    {txtbox_tarifsahibi, "Tarif sahibi boş bırakılamaz."},
                    {txtbox_tarifsahibimail, "Tarif sahibi mail boş bırakılamaz."}
                };

                if (Functions.ValidateForm(controlsToValidate) != "")
                {
                    lbl_mesaj.Text = Functions.ValidateForm(controlsToValidate);
                    return;
                }
                else
                {
                    string tarif_adi = txtbox_tarifad.Text;
                    string tarif_malzemeleri = txtbox_tarifmalzemeleri.Text;
                    string tarif_yapilisi = txtbox_tarifyapilisi.Text;
                    string tarif_sahibi = txtbox_tarifsahibi.Text;
                    string tarif_sahipmail = txtbox_tarifsahibimail.Text;
                    string tarifid = getTarifId();
                    //bool tarifonayi = true;
                    if (!Functions.IsValidEmail(tarif_sahipmail))
                    {
                        lbl_mesaj.Text = "Email alanı geçerli değil.";
                        return;
                    }
                    sqlclass.baglantiAc();

                    // tarif bilgisi çekme
                    string sqlquery = "SELECT * FROM tbl_tarif WHERE tarif_id=@Parametre_id";
                    SqlCommand komut1 = new SqlCommand(sqlquery, sqlclass.baglanti);
                    komut1.Parameters.AddWithValue("@Parametre_id", tarifid);
                    SqlDataReader oku = komut1.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(oku);
                    oku.Close();

                    // tarif resmi çekme
                    byte[] resimBytes = null;
                    if (dt.Rows.Count > 0 && dt.Rows[0]["tarif_resim"] != DBNull.Value && dt.Rows[0]["tarif_resim"] is byte[])
                    {
                        resimBytes = (byte[])dt.Rows[0]["tarif_resim"];
                    }


                    // tarifi atma
                    string sqlqueryekle = "INSERT INTO tbl_yemekler (yemek_ad,yemek_malzeme,yemek_tarif,yemek_puan,yemek_resim,kategori_id,yemek_onay,yemek_tarih) VALUES (@Param_ad, @Param_malzeme,@Param_tarif,@Param_puan,@Param_resim,@Param_kategoriId,@Param_onay,GETDATE())";
                    SqlCommand komutekle = new SqlCommand(sqlqueryekle, sqlclass.baglanti);
                    komutekle.Parameters.AddWithValue("@Param_ad", tarif_adi);
                    komutekle.Parameters.AddWithValue("@Param_malzeme", tarif_malzemeleri);
                    komutekle.Parameters.AddWithValue("@Param_tarif", tarif_yapilisi);
                    komutekle.Parameters.AddWithValue("@Param_puan", 0); // set by admin
                    komutekle.Parameters.AddWithValue("@Param_resim", resimBytes);
                    komutekle.Parameters.AddWithValue("@Param_kategoriId", 1); // temp kategori id
                    komutekle.Parameters.AddWithValue("@Param_onay", 0);
                    komutekle.Parameters.AddWithValue("@Param_tarih", DateTime.Now);
                    komutekle.ExecuteNonQuery();
                    lbl_mesaj.Text = "Tarif onaylandı.";

                    // tarifi tarif tablosundan silme
                    string sqlquerysil = "DELETE FROM tbl_tarif WHERE tarif_id=@Param1";
                    SqlCommand komutsil = new SqlCommand(sqlquerysil, sqlclass.baglanti);
                    komutsil.Parameters.AddWithValue("@Param1", tarifid);
                    komutsil.ExecuteNonQuery();
                    lbl_mesaj.Text = "Tarif silindi.";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message.ToString());
            }
            finally
            {
                sqlclass.baglantiKapat();
                List<Control> controlsToClear = new List<Control>()
                {
                    txtbox_tarifad,
                    txtbox_tarifmalzemeleri,
                    txtbox_tarifyapilisi,
                    txtbox_tarifsahibi,
                    txtbox_tarifsahibimail
                };
                Functions.ClearForm(controlsToClear);
            }
        }

        protected void btn_resimekle_Click(object sender, EventArgs e)
        {
            Functions.resimYukleme(FileUpload1, lbl_mesaj, HiddenField1);
            img_yemekresim_yukleme.ImageUrl = HiddenField1.Value;
        }
    }
}