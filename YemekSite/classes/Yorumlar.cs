using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YemekSite
{
    public class Yorumlar
    {
        private string isimsoyisim;
        private string email;
        private string tarih;
        private bool onay;
        private string yorum;
        private string yemek_id;

        public Yorumlar(string isimsoyisim, string email, string tarih,bool onay, string yorum, string yemek_id )
        {
            this.IsimSoyisim = isimsoyisim;
            this.Email = email;
            this.Yorum = yorum;
            this.Tarih = tarih;
            this.Onay = onay;
            this.Yemek_id = yemek_id;
        }

        public string IsimSoyisim { get => isimsoyisim; set => isimsoyisim = value; }
        public string Email { get => email; set => email = value; }
        public string Yorum { get => yorum; set => yorum = value; }
        public string Tarih { get => tarih; set => tarih = value; }
        public bool Onay { get => onay; set => onay = value; }
        public string Yemek_id { get => yemek_id; set => yemek_id = value; }

        public override string ToString()
        {
            return IsimSoyisim + ";" + Email + ";" + Tarih + ";" + (Onay ? "1" : "0") + ";" + Yorum + ";" + Yemek_id;
        }
    }
}