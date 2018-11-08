using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TheDailyBugle.Models;

namespace TheDailyBugle.Services
{
    class ComicService : IComicService
    {
        private string ROOT_URL = "https://www.arcamax.com/thefunnies/";

        public List<Series> GetSeriesList()
        {
            var web = new HtmlWeb();
            var doc = web.Load(ROOT_URL);
            doc.GetElementbyId
        }
    }
}
