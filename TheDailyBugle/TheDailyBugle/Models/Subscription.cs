using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TheDailyBugle.Models
{
    public class Subscription : Destination
    {
        public int SubscriptionId { get; set; }
        public int ComicTitleId { get; set; }

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
