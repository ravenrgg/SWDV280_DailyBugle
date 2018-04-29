using System;
using System.Collections.Generic;
using System.Text;

namespace TheDailyBugle
{
    public class Database
    {
        public static string ConnectionString()
        {
            return "Server = tcp:thedailybugle.database.windows.net,1433; Initial Catalog = The Daily Bugle; Persist Security Info = False; User ID = dbadmin; Password = 1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}
