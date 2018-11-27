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
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace TheDailyBugle
{
    public partial class TitlePage : ContentPage
    {
        private readonly IEnumerable<ComicTitle> comicTitles;
        private ObservableCollection<ComicTitle> subscribedComicTitles;
        private IComicParserService _comicParser;

        public TitlePage()
        {
            InitializeComponent();

            ComicRepository cr = new ComicRepository();
            List<ComicTitle> subscriptions;
            _comicParser = new ComicParserService();

            hideComicsButton.IsVisible = false;
            comicsTitles.IsVisible = false;

            // get user subscriptions
            subscriptions = cr.GetSubscriptionList();
            if (subscriptions == null)
            {
                subscriptions = new List<ComicTitle>();
            }
            comicTitles = _comicParser.GetComicTitles();
            if (comicTitles == null)
            {
                comicTitles = new List<ComicTitle>();
            }

            // get subscribed comics
            subscribedComicTitles = new ObservableCollection<ComicTitle>(subscriptions);
            UpdateDataBinding();
        }

        void OnToggleSettings(object sender, EventArgs args)
        {
            ToggleSettings();
        }

        void ToggleSettings()
        {
            comicsTitles.IsVisible = !comicsTitles.IsVisible;
            addComicsButton.IsVisible = !addComicsButton.IsVisible;
            hideComicsButton.IsVisible = !hideComicsButton.IsVisible;
            subscribredComicTitles.IsVisible = !subscribredComicTitles.IsVisible;
        }

        void OnDeleteClicked(object sender, EventArgs args)
        {
            var button = sender as Button;
            var comicTitle = button.Parent.BindingContext as ComicTitle;

            ComicRepository cr = new ComicRepository();
            cr.DeleteSubscription(comicTitle);
            subscribedComicTitles = new ObservableCollection<ComicTitle>(cr.GetSubscriptionList());

            comicsTitles.ItemsSource = comicTitles
                .Where(ct => !subscribedComicTitles.Any(s => s.ComicTitleId == ct.ComicTitleId));

            
        }

        public void DisplayCommic(object sender, ItemTappedEventArgs e)
        {
            //go to comic page
            var comicTitle = e.Item as ComicTitle;
            Navigation.PushAsync(new ComicPage(comicTitle));
        }

        void OnUnsubbedComicTapped(object sender, ItemTappedEventArgs e)
        {
            var comicTitle = e.Item as ComicTitle;
            ComicRepository cr = new ComicRepository();
            cr.SaveSubscription(comicTitle);
            UpdateDataBinding();

            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Success", $"{comicTitle.Name} has been added!", "OK");
            });
        }

        private void UpdateDataBinding()
        {

            ComicRepository cr = new ComicRepository();
            subscribredComicTitles.ItemsSource = cr.GetSubscriptionList();

            comicsTitles.ItemsSource = comicTitles
                .Where(ct => !subscribedComicTitles.Any(s => s.Name == ct.Name));
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Settings", "Cancel", null, "Add Comics", "Random Comic");

            if (action.Equals("Add Comics"))
            {
                ToggleSettings();
            }
            else if (action.Equals("Random Comic"))
            {
                Random random = new Random();
                var randomIndex = random.Next(comicTitles.Count());

                var comicTitle = comicTitles.ToList()[randomIndex];
                await Navigation.PushAsync(new ComicPage(comicTitle));
            }
        }

        private List<ComicTitle> GetUserSubscriptions(int userId)
        {
            ComicRepository cr = new ComicRepository();
            return cr.GetSubscriptionList();
        }

        private User CreateUser(string email)
        {
            // using (IDbConnection target = new SqlConnection(Database.ConnectionString()))
            // {
            ////     var user = new User
            ////     {
            // //        Email = email
            //     };

            //  target.Open();
            //  user.Insert(target);

            return new User
            {
                Email = "test@test.com"
            };
            // }
        }
    }
}