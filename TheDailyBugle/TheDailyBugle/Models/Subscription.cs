using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TheDailyBugle.Models
{
    public class Subscription : Destination
    {
        public int SubscriptionId { get; set; }
        public int ComicTitleId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;

        public static string Select()
        {
            return @"";
        }

        public override int Insert(IDbConnection connection)
        {
            var sql = @"
                    SELECT CAST(SCOPE_IDENTITY() as int)";
            return connection.Query<int>(sql, new
            {

            }).Single();
        }
    }
}
