using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YemekSite
{
    public class Mesajlar
    {
        public string mesajlar_adsoyad { get; private set; }
        public string mesajlar_mail { get; private set; }
        public string mesajlar_konu { get; private set; }
        public string mesajlar_mesaj { get; private set; }


        public Mesajlar(string mesajlar_adsoyad,string mesajlar_mail,string mesajlar_konu,string mesajlar_mesaj)
        {
            this.mesajlar_adsoyad = mesajlar_adsoyad;
            this.mesajlar_mail = mesajlar_mail;
            this.mesajlar_konu = mesajlar_konu;
            this.mesajlar_mesaj = mesajlar_mesaj;
        }

        public override string ToString()
        {
            return $"{mesajlar_adsoyad};{mesajlar_mail};{mesajlar_konu};{mesajlar_mesaj}";
        }


    }
}