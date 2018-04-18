using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SQLite.Net.Attributes;

namespace TheDailyBugle.Models
{
    public class ComicTitle : Destination
    {

        [PrimaryKey, AutoIncrement]
        public int ComicTitleId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public bool IsSubscribed { get; set; } = true;

        public static string Select()
        {
            return "";
        }

        public override int Insert(IDbConnection connection)
        {
            var sql = "";
            return connection.Query<int>(sql, new
            {
                name = Name,
                url = Url,
                iconUrl = IconUrl,
                IsSubscribed = false
            }).Single();
        }
    }
    }
}