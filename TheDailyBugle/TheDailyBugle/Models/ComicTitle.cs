using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TheDailyBugle.Models
{
    public class ComicTitle : Destination
    {
        public int ComicTitleId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public bool HasSubscribers { get; set; }

        public static string Select()
        {
            throw new NotImplementedException();
        }

        public override int Insert(IDbConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}