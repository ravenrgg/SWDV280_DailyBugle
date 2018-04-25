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
    public partial class ComicPage : ContentPage
    {
        private readonly IComicParserService _comicParserService;

        public ComicPage()
        {
            InitializeComponent();

            ComicImplementation();
        }



        private int currentComicIndex { get; set; } // holds the index of the current comic that is being displayed
        private string comics { get; set; } //

        public Image ComicImage { get; set; }



        private void ComicImplementation()
        {
            Comic wow = new Comic();
            // get comic title id from local storage (this will be set by Andrew)
            //var comicTitleId = (int)Application.Current.Properties["comicTitleId"];
            var comicTitleId = 14;

            ComicTitle comicTitle = null; // holds comic title that will be display on the page

            // get comic title from database
            using (IDbConnection source = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                comicTitle = source.Query<ComicTitle>
                    (ComicTitle.Select())
                    .FirstOrDefault(ct => ct.ComicTitleId.Equals(comicTitleId));
            }

            // this will give you a list of 5 comics based on the comic title, where comics[comics.Count - 1] holds the latest comic and comics[0] holds the oldest comic
            // We can play around with the number of comics being pulled from the website.

            ComicParserService _comicParserService = new ComicParserService();
            var comics = _comicParserService.GetComics(comicTitle.Url, 5);

            // initialize currentComicIndex with the last comic listed in comics list
            currentComicIndex = comics.Count - 1;

            //display comic
            backgroundImage.Source = new UriImageSource()
            {
                Uri = new Uri(comics[currentComicIndex].ImageUrl),
                CachingEnabled = true
            };

        }

        public void OnPrevClicked(object sender, EventArgs args)
        {

            // code for the previous button click event
            if (currentComicIndex > 0)
            {
                currentComicIndex--;
            }
            if (currentComicIndex.Equals(0))
            {
                previous.BackgroundColor = Color.Default;
            }
        }

        public void OnNextClicked(object sender, EventArgs args)
        {

            // code for next button click event
            if (currentComicIndex < comics.Count() - 1)
            {
                currentComicIndex++;
            }
            if (currentComicIndex.Equals(comics.Count() - 1))
            {
                next.BackgroundColor = Color.Default;
            }
        }
    }

}