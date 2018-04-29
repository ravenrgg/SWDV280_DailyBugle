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
        private IComicParserService _comicParserService;
        

        public ComicPage(ComicTitle comicTitle)
        {
            InitializeComponent();

            ComicImplementation(comicTitle);
        }

        private int currentComicIndex { get; set; } // holds the index of the current comic that is being displayed
        private Comic wow { get; set; }
        private Image ComicImage { get; set; }
        private List<Comic> comics { get; set; }
        private ComicTitle comicTitle;

       private void ComicImplementation(ComicTitle comicTitle)
        {
            wow = new Comic();

            // this will give you a list of 5 comics based on the comic title, where comics[comics.Count - 1] holds the latest comic and comics[0] holds the oldest comic
            // We can play around with the number of comics being pulled from the website.

            _comicParserService = new ComicParserService();
            comics = _comicParserService.GetComics(comicTitle.Url, 5);

            // initialize currentComicIndex with the last comic listed in comics list
            currentComicIndex = comics.Count - 1;

            //display comic
            backgroundImage.Source = new UriImageSource()
            {
                Uri = new Uri(comics[currentComicIndex].ImageUrl),
                CachingEnabled = true
            };

            next.IsEnabled = false;

            comicTitleLabel.Text = comicTitle.Name;


        }

        private void OnPrevClicked(object sender, EventArgs args)
        {
            // code for the previous button click event
            if (currentComicIndex > 0)
            {

                currentComicIndex--;
                next.IsEnabled = true;

                backgroundImage.Source = new UriImageSource()
                {
                    Uri = new Uri(comics[currentComicIndex].ImageUrl)
                };

             
            }
            if (currentComicIndex.Equals(0))
            {
                
                previous.IsEnabled = false;
                
            }

        }

        public void OnNextClicked(object sender, EventArgs args)
        {

            // code for next button click event
            if (currentComicIndex < comics.Count() - 1)
            {
                currentComicIndex++;
                next.IsEnabled = true;
                previous.IsEnabled = true;

                backgroundImage.Source = new UriImageSource()
                {
                    Uri = new Uri(comics[currentComicIndex].ImageUrl)
                };

          

            }
            if (currentComicIndex.Equals(comics.Count() - 1))
            {
                next.IsEnabled = false;
                previous.IsEnabled = true;
            }


        }

        void DisplayTitles(object sender, EventArgs args)
        {
            //go to title page
            Navigation.PushAsync(new TitlePage());
        }
    }

}