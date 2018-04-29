using System;
using System.Collections.Generic;
using System.Linq;
using TheDailyBugle.Services;
using Xamarin.Forms;
using TheDailyBugle.Models;
using Dapper;

namespace TheDailyBugle
{
    public partial class ComicPage : ContentPage
    {
        private IComicParserService _comicParserService;

        private int currentComicIndex { get; set; } // holds the index of the current comic that is being displayed
        private Comic wow { get; set; }
        private Image ComicImage { get; set; }
        private List<Comic> comics { get; set; }
        private DateTime dt { get; set; }


        public ComicPage(ComicTitle comicTitle)
        {
            InitializeComponent();

            const int COMIC_COUNT = 5; // holds the number of comics that will be received.
            wow = new Comic();

            _comicParserService = new ComicParserService();
            comics = _comicParserService.GetComics(comicTitle.Url, COMIC_COUNT);

            currentComicIndex = 0;


            comicTitleLabel.Text = comicTitle.Name;

            next.IsEnabled = false;

            dateTimeAndBackgroundImage();
        }

        private void dateTimeAndBackgroundImage ()
        {

            //display comic
            backgroundImage.Source = new UriImageSource()
            {
                Uri = new Uri(comics[currentComicIndex].ImageUrl),
                CachingEnabled = true
            };

            //Gets date time and converts it to a different pattern
            comicDateLabel.Text = comics[currentComicIndex].PublishDate.ToString("MM/dd");

        }

        //Buttons Start
        private void OnPrevClicked(object sender, EventArgs args)
        {
            // code for the previous button click event
            if (currentComicIndex < comics.Count() - 1)
            {

                currentComicIndex++;

                next.IsEnabled = true;

                dateTimeAndBackgroundImage();

            }

            if (currentComicIndex.Equals(comics.Count() - 1))
            {
                
                previous.IsEnabled = false;
                
            }
        }

        public void OnNextClicked(object sender, EventArgs args)
        {
            // code for next button click event
            if (currentComicIndex > 0)
            {

                currentComicIndex--;

                next.IsEnabled = true;
        
                previous.IsEnabled = true;

                dateTimeAndBackgroundImage();

            }

            if (currentComicIndex.Equals(0))
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