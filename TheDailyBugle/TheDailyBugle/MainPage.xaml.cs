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


            using (IDbConnection target = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                //Insert titles into database
                foreach (var title in titles)
                {
                    //title.Insert(target);
                }
            }

            // Use this code to grab titles on other pages
            using (IDbConnection source = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                // Execute select query returns all items in table
                source.Open();
                var comicTitles = source.Query<ComicTitle>
                    (ComicTitle.Select())
                    .ToList();

                // Get subscribed comic titles
                var subscribedTitles = comicTitles
                    .Where(c => c.IsSubscribed);

                // Get individual comic
                var comic = comicTitles
                    .FirstOrDefault(c => c.Name.Equals("Name of comic.."));
            }
        }
    }
}
