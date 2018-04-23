using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace TheDailyBugle.Models
{
    public class ComicTitle : Destination
    {

        public int ComicTitleId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
        public bool IsSubscribed { get; set; } = true;

        public static string Select()
        {
            return @"SELECT [ComicTitleId]
                  ,[Name]
                  ,[Url]
                  ,[IconUrl]
                  ,[IsSubscribed]
                    FROM[dbo].[ComicTitles]";
        }

        public override int Insert(IDbConnection connection)
        {
            var sql = @"INSERT INTO [dbo].[ComicTitles]
                       ([Name]
                       ,[Url]
                       ,[IconUrl]
                       ,[IsSubscribed])
                        VALUES
                       (@name = Name,
                       ,@url = Url,
                       ,@iconUrl = IconUrl,
                       ,@isSubscribed = IsSubscribed)";
            return connection.Query<int>(sql, new
            {
                name = Name,
                url = Url,
                iconUrl = IconUrl,
                isSubscribed = IsSubscribed
            }).Single();
        }
    }
}