using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace YemekSite
{
    public class Tarif
    {

        public Tarif(string adi, string malzeme, string yapilis, string resim, string oneren_kisi, string mail,bool onay)
        {
            this.adi = adi;
            this.malzeme = malzeme;
            this.yapilis = yapilis;
            this.resim = resim;
            this.oneren_kisi = oneren_kisi;
            this.mail = mail;
            this.onay = onay;
        }
        private string adi { get; set; }
        private string malzeme { get; set; }
        private string yapilis { get; set; }
        private string resim { get;  set; }
        private string oneren_kisi { get; set; }
        private string mail { get; set; }
        
        private bool onay { get; set; }


        public string getTarifAdi()
        {
            return adi;
        }
        public string getTarifMalzeme()
        {
            return malzeme;
        }
        public string getTarifYapilis()
        {
            return yapilis;
        }
        public string getTarifResim()
        {
            return resim;
        }
        public string getTarifOnerenKisi()
        {
            return oneren_kisi;
        }
        public string getTarifOnerenMail()
        {
            return mail;
        }

        public bool getOnay()
        {
            return onay;
        }



       


    }
}