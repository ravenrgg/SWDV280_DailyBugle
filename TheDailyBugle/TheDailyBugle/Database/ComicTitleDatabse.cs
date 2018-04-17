using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;
using TheDailyBugle.Models;

namespace TheDailyBugle.Database
{
    class ComicTitleDatabase
    {
        private SQLiteConnection _connection;

        public ComicTitleDatabase()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<ComicTitle>();
        }

        public IEnumerable<ComicTitle> GetComicTitle()
        {
            return _connection.Table<ComicTitle>()
                .ToList();
        }

        public ComicTitle GetComicTitle(int id)
        {
            return _connection.Table<ComicTitle>()
                .FirstOrDefault(t => t.ComicTitleId == id);
        }

        public List<ComicTitle> GetSubscribedComicTitles()
        {
            return _connection.Table<ComicTitle>()
                .Where(t => t.IsSubscribed)
                .ToList();
        }

        public void DeleteComicTitle(int id)
        {
            _connection.Delete<ComicTitle>(id);
        }

        public void AddComicTitle(ComicTitle comicTitle)
        {
            var newThought = new ComicTitle
            {
                Name = comicTitle.Name,
                Url = comicTitle.Url,
                IconUrl = comicTitle.IconUrl
            };

            _connection.Insert(newThought);
        }
    }
}
