using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDailyBugle.UWP;
using Xamarin.Forms;
using SQLite.Net;

[assembly: Dependency(typeof(SQLite_UWP))]

namespace TheDailyBugle.UWP
{
    class SQLite_UWP: ISQLite
    {
        public SQLite_UWP()
        {
        }

        #region ISQLite implementation

        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var fileName = "ComicTitles.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
            var connection = new SQLite.Net.SQLiteConnection(platform, path);

            return connection;
        }

        #endregion
    }
}
