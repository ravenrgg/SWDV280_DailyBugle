using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TheDailyBugle.Services;
using TheDailyBugle.Models;
using TheDailyBugle.Database;
using SQLite.Net;

namespace TheDailyBugle
{
	public partial class MainPage : ContentPage
	{
        private readonly IComicParserService _comicParserService;
		public MainPage()
		{
			InitializeComponent();

            _comicParserService = new ComicParserService();
            var titles = _comicParserService.GetComicTitles();
            var comics = _comicParserService.GetComics(0, 0);

            var _comicTitleDatabase = new ComicTitleDatabase();

            //_comicTitleDatabase.AddComicTitle("test Name", "http://test.test.com", "http://test.test.com/image", false);
            //var foo = _comicTitleDatabase.GetComicTitle(0);

            foreach (var comic in comics)
            {

            }

            foreach (var title in titles)
            {

            }
        }
    }
}
