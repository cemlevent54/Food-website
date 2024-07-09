using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace YemekSite
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                if (file.ContentType.StartsWith("image/"))
                {
                    try
                    {
                        // Dosyayı bellek içinde oku
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            byte[] fileData = ms.ToArray();

                            // Base64 encode the image data
                            string base64String = Convert.ToBase64String(fileData);

                            // Yanıtı JSON formatında döndür
                            context.Response.Write("{\"success\": true, \"fileData\": \"" + base64String + "\"}");
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Response.Write("{\"success\": false, \"message\": \"" + ex.Message + "\"}");
                    }
                }
                else
                {
                    context.Response.Write("{\"success\": false, \"message\": \"Only image files are allowed.\"}");
                }
            }
            else
            {
                context.Response.Write("{\"success\": false, \"message\": \"No file uploaded.\"}");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}