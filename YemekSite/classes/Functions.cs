using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Label = System.Web.UI.WebControls.Label;


namespace YemekSite
{
    public class Functions
    {
        public static bool IsValidEmail(string email)
        {
            // Düzenli ifade (regex) kullanarak email geçerliliğini kontrol etme
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex regex = new Regex(pattern);

            // Geçerli email uzantıları
            string[] validDomains = { "gmail.com", "hotmail.com", "edu" };

            // Email geçerliliğini ve uzantısını kontrol et
            if (regex.IsMatch(email))
            {
                foreach (string domain in validDomains)
                {
                    if (email.EndsWith(domain, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string ValidateForm(Dictionary<Control, string> controlsToValidate)
        {
            if (ValidateFormList(controlsToValidate).Count == 1)
            {
                return ValidateFormList(controlsToValidate)[0];
            }
            
            else if(ValidateFormList(controlsToValidate).Count > 1)
            {
                return "Boş alanları doldurun.";
            }

            return string.Empty;
        }

        public static List<string> ValidateFormList(Dictionary<Control, string> controlsToValidate)
        {
            List<string> errorList = new List<string>();

            foreach (var control in controlsToValidate)
            {
                if (control.Key is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                {
                    string errorString = $"{control.Value} boş olamaz.";
                    errorList.Add(errorString);
                }
                else if (control.Key is DropDownList dropDownList && (dropDownList.SelectedIndex == -1 || dropDownList.SelectedIndex == 0))
                {
                    string errorString = $"{control.Value} seçilmelidir.";
                    errorList.Add(errorString);
                }
                else if (control.Key is System.Web.UI.WebControls.Image image && string.IsNullOrEmpty(image.ImageUrl))
                {
                    string errorString = "Lütfen bir resim yükleyin.";
                    errorList.Add(errorString);
                }
                // Diğer kontrol türlerini de ekleyebilirsiniz
            }

            return errorList;
        }

        public static void ClearForm(List<Control> controlsToClear)
        {
            foreach (var control in controlsToClear)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                else if (control is System.Web.UI.WebControls.Image image)
                {
                    image.ImageUrl = string.Empty;
                }
                else if (control is DropDownList dropDownList)
                {
                    dropDownList.SelectedIndex = -1;
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
                // Diğer kontrol türlerini de ekleyebilirsiniz
            }
        }

        public static bool passwordControl(string password, string passwordAgain)
        {
            return password == passwordAgain;
        }

        public static void resimYukleme(FileUpload fileUpload, Label outputLabel, HiddenField hiddenImage)
        {
            if (fileUpload.HasFile)
            {
                HttpPostedFile postedFile = fileUpload.PostedFile;
                string fileName = Path.GetFileName(postedFile.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                // Check for valid file extensions
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".bmp")
                {
                    // Check file size (e.g., limit to 10 MB)
                    if (postedFile.ContentLength <= 10485760) // 10 MB limit
                    {
                        try
                        {
                            // Convert file to base64 string
                            using (Stream stream = postedFile.InputStream)
                            {
                                using (BinaryReader binaryReader = new BinaryReader(stream))
                                {
                                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                    hiddenImage.Value = $"data:image/{fileExtension.Substring(1)};base64," + base64String;
                                    outputLabel.Text = "Resim yüklendi.";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            outputLabel.Text = "Resim yükleme hatası: " + ex.Message;
                        }
                    }
                    else
                    {
                        outputLabel.Text = "Dosya boyutu 10 MB'yi aşamaz.";
                    }
                }
                else
                {
                    outputLabel.Text = "Sadece .jpg, .jpeg, .png ve .bmp dosyaları yüklenebilir.";
                }
            }
            else
            {
                outputLabel.Text = "Lütfen bir dosya seçin.";
            }
        }

        public static void panelGoster(Panel panel)
        {
            panel.Visible = true;
        }

        public static void panelGizle(Panel panel)
        {
            panel.Visible = false;
        }

        
    }
}