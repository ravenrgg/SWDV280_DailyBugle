using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TheDailyBugle.Models
{
    public class User : Destination
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        public static string Select()
        {
            return @"SELECT [UserId]
                  ,[Email]
                  FROM [dbo].[Users]";
        }

        public override int Insert(IDbConnection connection)
        {
            var sql = @"INSERT INTO [dbo].[Users]
                       ([Email])
                        VALUES
                       (@email);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
            return connection.Query<int>(sql, new
            {
                email = Email
            }).Single();
        }
    }
}
