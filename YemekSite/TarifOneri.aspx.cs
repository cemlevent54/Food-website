using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HtmlAgilityPack;
using System.Drawing;
using System.Data;
using System.Web;

namespace YemekSite
{
    public partial class TarifOneri1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.Visible = false;
        }

        

        

        SqlClass sqlclass = new SqlClass();

        protected void btn_onay_Click(object sender, EventArgs e)
        {
            try
            {
                
                string validationMessage = ValidateForm();
                if (string.IsNullOrEmpty(validationMessage))
                {
                    string tarif_ad = txtBox_tarifAdi.Text;
                    string tarif_malzeme = txtBox_tarifMalzemeleri.Text;
                    string tarif_yapilis = txtBox_tarifYapilis.Text;
                    string tarif_resim = HiddenField1.Value;
                    string tarif_oneren_kisi = txtBox_tarifOnerenKisi.Text;
                    string tarif_oneren_mail = txtBox_Mail.Text;
                    bool onay = false;
                    Tarif yeni_tarif = new Tarif(tarif_ad, tarif_malzeme, tarif_yapilis, tarif_resim, tarif_oneren_kisi, tarif_oneren_mail,onay);
                    tarifEkle(yeni_tarif);

                    ClearForm();
                }
                else
                {
                    Label1.Text = validationMessage;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        

        private void tarifEkle(Tarif tarif)
        {
            try
            {
                sqlclass.baglantiAc();
                string sqlquery = "INSERT INTO tbl_tarif (tarif_ad, tarif_malzeme, tarif_yapilis, tarif_resim, tarif_sahip, tarif_sahipmail,tarif_onay) " +
                                  "VALUES (@param1, @param2, @param3, @param4, @param5, @param6,@param7)";
                SqlCommand komut = new SqlCommand(sqlquery, sqlclass.baglanti);

                komut.Parameters.Add("@param1", SqlDbType.NVarChar).Value = tarif.getTarifAdi();
                komut.Parameters.Add("@param2", SqlDbType.NVarChar).Value = tarif.getTarifMalzeme();
                komut.Parameters.Add("@param3", SqlDbType.NVarChar).Value = tarif.getTarifYapilis();

                byte[] resim = Convert.FromBase64String(tarif.getTarifResim().Split(',')[1]);
                komut.Parameters.Add("@param4", SqlDbType.Image).Value = resim;

                komut.Parameters.Add("@param5", SqlDbType.NVarChar).Value = tarif.getTarifOnerenKisi();
                komut.Parameters.Add("@param6", SqlDbType.NVarChar).Value = tarif.getTarifOnerenMail();

                komut.Parameters.AddWithValue("@param7", tarif.getOnay());
                komut.ExecuteNonQuery();
                Label1.Text = "Tarif başarıyla alındı.";
            }
            catch (Exception ex)
            {
                Label1.Text = "Veritabanı hatası: " + ex.Message;
            }
            finally
            {
                sqlclass.baglantiKapat();
            }
        }

        protected void txtBox_Mail_TextChanged(object sender, EventArgs e)
        {
            if (!Functions.IsValidEmail(txtBox_Mail.Text))
            {
                Label1.Text = "Geçersiz e-posta adresi.";
            }
            else
            {
                Label1.Text = "";
            }
        }

        private string ValidateForm()
        {

            if (string.IsNullOrEmpty(txtBox_tarifAdi.Text))
            {
                return "Tarif adı boş olamaz.";
            }

            if (string.IsNullOrEmpty(txtBox_tarifMalzemeleri.Text))
            {
                return "Tarif malzemeleri boş olamaz.";
            }

            if (string.IsNullOrEmpty(txtBox_tarifYapilis.Text))
            {
                return "Tarif yapılışı boş olamaz.";
            }

            if (string.IsNullOrEmpty(txtBox_tarifOnerenKisi.Text))
            {
                return "Tarif öneren kişi adı boş olamaz.";
            }

            if (string.IsNullOrEmpty(txtBox_Mail.Text) || !Functions.IsValidEmail(txtBox_Mail.Text))
            {
                return "Geçerli bir e-posta adresi giriniz.";
            }

            

            return string.Empty;
        }

        private void ClearForm()
        {
            txtBox_tarifAdi.Text = "";
            txtBox_tarifMalzemeleri.Text = "";
            txtBox_tarifYapilis.Text = "";
            txtBox_tarifOnerenKisi.Text = "";
            txtBox_Mail.Text = "";
            //imgBox_image.ImageUrl = ""; // image
        }

        protected void btn_Resim_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName).ToLower();
            if (fileExtension.ToLower() == ".jpg" || 
                fileExtension.ToLower() == ".jpeg" || 
                fileExtension.ToLower() == ".png" ||
                fileExtension.ToLower() == ".bmp")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                HiddenField1.Value = "data:image/png;base64," + base64String;
                Label1.Text = "Resim yüklendi.";
                Image1.Visible = true;
                resimyukle();
            }
            else
            {
                Label1.Text = "Sadece .jpg, .jpeg ve .png dosyaları yüklenebilir.";
            }
        }

        public void resimyukle()
        {
            Image1.ImageUrl = HiddenField1.Value;
        }
    }
}
