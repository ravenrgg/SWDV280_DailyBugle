using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace TheDailyBugle.Models
{
    [Serializable]
    public class ComicTitle
    {
        public int ComicTitleId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
    }
}