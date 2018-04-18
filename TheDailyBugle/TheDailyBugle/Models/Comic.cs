using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TheDailyBugle.Models
{
    public class Comic : Destination
    {
        public int ComicId { get; set; }
        public int ComicTitleId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }

        public static string Select()
        {
            return null;
        }

        public override int Insert(IDbConnection connection)
        {
            var sql = "";
            return connection.Query<int>(sql, new
            {
                comicTitleId = ComicTitleId,
                imageUrl = ImageUrl,
                publishDate = PublishDate
            }).Single();
        }
    }
}
