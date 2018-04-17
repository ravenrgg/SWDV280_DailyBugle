using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDailyBugle.Services;
using Xamarin.Forms;
using TheDailyBugle.Models;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using TheDailyBugle.Database;

namespace TheDailyBugle
{
	public partial class MainPage : ContentPage
	{
        private readonly IComicParserService _comicParserService;
		public MainPage()
		{
			InitializeComponent();

            _comicParserService = new ComicParserService();
            var comics = _comicParserService.GetComics(0, 0);

            foreach (var comic in comics)
            {

            }

            var titles = _comicParserService.GetComicTitles();

            ComicTitleDatabase database = new ComicTitleDatabase();
            foreach (var title in titles)
            {
                database.AddComicTitle(title);
            }

            var comicTitle = database.GetComicTitle(0);
        }
    }
}
