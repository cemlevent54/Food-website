using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace YemekSite
{
    public class SqlClass
    {
        private string connectionString = "Data Source=DESKTOP-0PCHDQV;Initial Catalog=Db_YemekSitesi;Integrated Security=True;Encrypt=False";
        public SqlConnection baglanti;

        public SqlClass()
        {
            baglanti = new SqlConnection(connectionString);
        }

        public SqlConnection baglantiAc()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            return baglanti;
        }

        public void baglantiKapat()
        {
            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }
    }
}