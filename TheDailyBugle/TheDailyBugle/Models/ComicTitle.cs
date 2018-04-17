using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace TheDailyBugle.Models
{
    public class ComicTitle
    {
        [PrimaryKey, AutoIncrement]
        public int ComicTitleId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public bool HasSubscribers { get; set; }

        public static string Select()
        {
            throw new NotImplementedException();
        }

        public static int Insert()
        {
            throw new NotImplementedException();
        }
    }
}