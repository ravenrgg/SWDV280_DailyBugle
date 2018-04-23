using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDailyBugle.Models;
using HtmlAgilityPack;

namespace TheDailyBugle.Services
{
    public class ComicParserService : IComicParserService
    {

        public List<ComicTitle> GetComicTitles()
        {
            var document = GetPageDocument("http://www.gocomics.com/comics/a-to-z/");

            var links = document.DocumentNode.Descendants("a")
                .Where(d => d.Attributes["class"].Value.Contains("amu-media-item-link") && !d.InnerText.Equals(null))
                .ToList();

            var titles = new List<ComicTitle>();
            foreach (HtmlNode link in links)
            {
                var comicName = link.SelectSingleNode(".//h4")
                    .InnerText;

                var comicUrl = link.Attributes["href"].Value;

                var comicIconUrl = link.SelectNodes(".//img")
                    .FirstOrDefault()
                    .Attributes["src"]
                    .Value;

                titles.Add(new ComicTitle
                {
                    Name = comicName,
                    Url = comicUrl,
                    IconUrl = comicIconUrl
                });
            }

            // Insert Into Database?
            return titles;
        }

        public List<Comic> GetComics(string comicUrl, int count)
        {
            var now = DateTime.Now;

            var comics = new List<Comic>();
            var daysToAdd = 0;
            for (int i = 0; i < 5; i++)
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
