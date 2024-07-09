using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Linq;

namespace YemekSite
{
    public class Kullanici
    {
        private string isim;
        private string soyisim;
        private string username;
        private string email;
        private string sifre;
        private string resim;
        private bool rememberMe;
        private string kullaniciTuru;
        private string telefonNumarasi;
        

        public Kullanici(string isim, string soyisim, string email, string sifre, string resim, bool rememberMe, string kullaniciTuru,string username,string telefonNumarasi)
        {
            this.Isim = isim;
            this.Soyisim = soyisim;
            this.Username = username;
            this.Email = email;
            this.Sifre = sifre;
            this.Resim = resim;
            this.RememberMe = rememberMe;
            this.KullaniciTuru = kullaniciTuru;
            this.TelefonNumarasi = telefonNumarasi;
        }

        public string Isim { get => isim; set => isim = value; }
        public string Soyisim { get => soyisim; set => soyisim = value; }
        public string Email { get => email; set => email = value; }
        public string Sifre { get => sifre; set => sifre = value; }
        public string Resim { get => resim; set => resim = value; }
        public bool RememberMe { get => rememberMe; set => rememberMe = value; }
        public string KullaniciTuru { get => kullaniciTuru; set => kullaniciTuru = value; }
        public string Username { get => username; set => username = value; }
        public string TelefonNumarasi { get => telefonNumarasi; set => telefonNumarasi = value; }

        public override string ToString()
        {
            return Isim + "," + Soyisim + "," + Username + "," + Email + "," + Sifre + "," + Resim + "," + (RememberMe ? "1" : "0") + "," + KullaniciTuru + "," + TelefonNumarasi;
        }

        public bool SifreKontrol(string sifreTekrar)
        {
            return Functions.passwordControl(Sifre,sifreTekrar);
        }

        public bool EmailKontrol()
        {
            return Functions.IsValidEmail(Email);
        }

        
    }
}