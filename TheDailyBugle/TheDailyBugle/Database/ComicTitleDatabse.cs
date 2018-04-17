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
            return (from t in _connection.Table<ComicTitle>()
                    select t).ToList();
        }

        public ComicTitle GetComicTitle(int id)
        {
            return _connection.Table<ComicTitle>().FirstOrDefault(t => t.ComicTitleId == id);
        }

        public void DeleteComicTitle(int id)
        {
            _connection.Delete<ComicTitle>(id);
        }

        public void AddComicTitle(string name, string url, string iconUrl, bool hasSubscribers)
        {
            var newThought = new ComicTitle
            {
                Name = name,
                Url = url,
                IconUrl = iconUrl,
                HasSubscribers = hasSubscribers
            };

            _connection.Insert(newThought);
        }
    }
}
