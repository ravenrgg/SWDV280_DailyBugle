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

            hideComicsButton.IsVisible = false;
            comicsTitles.IsVisible = false;
            _comicParser = new ComicParserService();

            //ComicRepository repo = new ComicRepository();
            //repo.SaveSubscription(_comicParser.GetComicTitles()[0]);
            //repo.GetSubscriptionList();
            //repo.DeleteSubscription(_comicParser.GetComicTitles()[0]);

            List<Subscription> subscriptions;
            //using (IDbConnection source = new SqlConnection(Database.ConnectionString()))
            //{

            ////    //source.Open();

            ////    //// get user subscriptions
            ////    //subscriptions = source.Query<Subscription>(
            ////    //    Subscription.Select())
            ////    //    .Where(s => s.UserId.Equals(8) && s.IsActive)
            ////    //    .Distinct()
            ////    //    .ToList();

            ////    //// get all comics
            ////    //comicTitles = source.Query<ComicTitle>(
            ////    //    ComicTitle.Select())
            ////    //    .Distinct()
            ////    //    .OrderBy(ct => ct.Name)
            ////    //    .ToList();

            ////    //// get subscribed comics
            ////    //subscribedComicTitles = new ObservableCollection<ComicTitle>(comicTitles
            ////    //    .Where(ct => subscriptions.Any(s => s.ComicTitleId == ct.ComicTitleId))
            ////    //    .Distinct());
            //}
            comicTitles = new List<ComicTitle>();
            subscribedComicTitles = new ObservableCollection<ComicTitle>();



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

            //using (IDbConnection source = new SqlConnection(Database.ConnectionString()))
            //{
            ////    //    source.Open();

            ////    //    var subscription = source.Query<Subscription>(Subscription.Select())
            ////    //        .FirstOrDefault(s => s.ComicTitleId.Equals(comicTitle.ComicTitleId) &&
            ////    //                             s.UserId.Equals(8) &&
            ////    //                             s.IsActive);

            ////    //    // delete the subscription
            ////    //    subscription.Update(source, false);

            ////    //    // remove from subscription list
            ////    //    subscribedComicTitles.Remove(comicTitle);
            //}
            //comicTitles = new List<ComicTitle>();
            subscribedComicTitles = new ObservableCollection<ComicTitle>();

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

            var subscription = new Subscription
            {
                UserId = 8,//(int)Application.Current.Properties["userId"],
                IsActive = true,
                ComicTitleId = comicTitle.ComicTitleId
            };

            //using (IDbConnection source = new SqlConnection(Database.ConnectionString()))
            //{
            //    // assign the subscriptionId to the primary key that will be returned
            //    //subscription.SubscriptionId = subscription.Insert(source);
            //}

            ((ListView)sender).SelectedItem = null; // de-select the row

            var newTitle = comicTitles
                .FirstOrDefault(ct => ct.ComicTitleId.Equals(subscription.ComicTitleId));

            subscribedComicTitles.Add(newTitle);
            UpdateDataBinding();

            Device.BeginInvokeOnMainThread(() => {
                DisplayAlert("Success", $"{newTitle.Name} has been added!", "OK");
            });
        }

        private void UpdateDataBinding()
        {
            subscribredComicTitles.ItemsSource = subscribedComicTitles;

            comicsTitles.ItemsSource = comicTitles
                .Where(ct => !subscribedComicTitles.Any(s => s.ComicTitleId == ct.ComicTitleId));
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

        private List<Subscription> GetUserSubscriptions(int userId)
        {
            //using (IDbConnection source = new SqlConnection(Database.ConnectionString()))
            //{
            //  //  source.Open();
            //  //  var subscriptions = source.Query<Subscription>(
            //   //     Subscription.Select())
            //   //     .Where(s => s.UserId.Equals(userId))
            //    //    .ToList();

            //    return new List<Subscription>();
            //}
            return new List<Subscription>();
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