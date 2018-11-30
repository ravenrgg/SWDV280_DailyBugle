using System;
using System.Collections.Generic;
using System.Linq;
using TheDailyBugle.Models;
using HtmlAgilityPack;
using TheDailyBugle.Resources;
using Newtonsoft.Json.Linq;
using System.IO;
using TheDailyBugle.Clients;

namespace TheDailyBugle.Services
{
    public class ComicParserService : IComicParserService
    {
        public List<ComicTitle> GetComicTitles()
        {
            List<ComicTitle> comicTitles = new List<ComicTitle>();
            var jobj = JArray.Parse(AllComicTitles.ALL_COMICS);
            foreach (var child in jobj.Children())
            {
                comicTitles.Add(new ComicTitle
                {
                    Name = (string)child["Name"],
                    Url = (string)child["Url"],
                    IconUrl = (string)child["IconUrl"]
                });
            }
            return comicTitles;
        }

        public List<Comic> GetComics(string comicUrl, int count)
        {
            var now = DateTime.Now;

            var comics = new List<Comic>();
            var daysToAdd = 0;
            for (int i = 0; i < count; i++)
            {
                Comic comic;

                var url = comicUrl.Remove(0, 1).Split('/')[0];
                do
                {
                    var comicDate = now.AddDays(daysToAdd);
                    comic = GetComic($"{"http://www.gocomics.com/"}/{url}", comicDate);
                    daysToAdd--;

                } while (comic == null);

                comics.Add(comic);
            }

            return comics;
        }

        private Comic GetComic(string url, DateTime comicDate)
        {

            var documentUrl = $"{url}/{comicDate.Year}/{comicDate.Month.ToString("00")}/{comicDate.Day.ToString("00")}";

            HtmlDocument hdoc = new HtmlDocument();
            CookieWebClient wc = new CookieWebClient();

            Stream read = null;

            try
            {
                read = wc.OpenRead(documentUrl);
            }
            catch (ArgumentException)
            {
                read = wc.OpenRead(Uri.EscapeUriString(documentUrl));
            }
            catch (HtmlWebException)
            {
                wc = new CookieWebClient();
                read = wc.OpenRead(documentUrl);
            }


            hdoc.Load(read, true);
            var pictureContainer = hdoc.DocumentNode.Descendants("picture")
                .FirstOrDefault(d => d.Attributes.Contains("class") &&
                                     d.Attributes["class"].Value.Contains("item-comic-image"));

            if (pictureContainer == null)
            {
                return null;
            }

            var imageUrl = pictureContainer.SelectNodes(".//img")
                    .FirstOrDefault()
                    .Attributes["src"]
                    .Value;

            var comic = new Comic
            {
                ImageUrl = imageUrl,
                PublishDate = comicDate
            };

            return comic;
        }

        private HtmlDocument GetPageDocument(string url)
        {
            var web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
