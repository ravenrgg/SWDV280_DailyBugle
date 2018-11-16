using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDailyBugle.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TheDailyBugle.Resources;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

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
                    IconUrl = (string)child["IconUrl"],

                });
            }
            return new List<ComicTitle>();
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
            var document = GetPageDocument(documentUrl);

            var pictureContainer = document.DocumentNode.Descendants("picture")
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
