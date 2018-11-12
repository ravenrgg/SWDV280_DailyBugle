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
        private string ROOT_URL = "https://www.arcamax.com";

        public List<Series> GetSeriesList()
        {
            List<Series> seriesList = new List<Series>();
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(ROOT_URL + "/comics/");
            HtmlNodeCollection allcomics = doc.DocumentNode.SelectNodes("//div[contains(@class,'comic-icon-cell')]");
            foreach (HtmlNode comic in allcomics)
            {
                seriesList.Add(new Series
                {
                    Title = comic.ChildNodes[1].Attributes[1].Value,
                    ImageUrl = comic.SelectSingleNode("//img").Attributes[1].Value,
                    Url = comic.ChildNodes[1].Attributes[0].Value
                });
            }
            return seriesList;
        }

        public string GetDailyStrip(Series series)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(ROOT_URL + series.Url);
            return ROOT_URL + doc.GetElementbyId("comic-zoom").Attributes[1].Value;
        }
    }
}
