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

namespace TheDailyBugle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TitlePage : ContentPage
	{
		public TitlePage ()
		{
			InitializeComponent ();

            // get user id from local storage (this will be set by the on start event)
            var userId = (int)Application.Current.Properties["userId"];
            userLabel.Text = "user: " + userId.ToString();

            // get a list of user subscriptions
            List<Subscription> subscriptions;
            using (IDbConnection source = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                source.Open();
                subscriptions = source.Query<Subscription>(
                    Subscription.Select())
                    .Where(s => s.UserId.Equals(userId))
                    .ToList();
            }

            // get a list of unsubscribed comicTitles
            List<ComicTitle> unsubscribedComicTitles;
            using (IDbConnection source = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                source.Open();
                unsubscribedComicTitles = source.Query<ComicTitle>(
                    ComicTitle.Select())
                    .Where(ct => !subscriptions.Any(s => s.ComicTitleId == ct.ComicTitleId))
                    .ToList();
            }
            
            //databind list sources
            subbedComicsTitles.ItemsSource = subscriptions;
            unSubbedComicsTitles.ItemsSource = unsubscribedComicTitles;
        }

        void OnAddComicButtonClicked(object sender, EventArgs args)
        {
            //hide add comic button
            //unhide unsubbed comics
            //hide subbed comics?
        }

        void OnDeleteClicked(object sender, EventArgs args)
        {
            //// code to remove a comic subscription
            //using (IDbConnection source = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            //{
            //    // get the subscription using the bound comicTitleId
            //    var subscriptionToDelete = subscriptions.FirstOrDefault(s => s.ComicTitleId.Equals(comicTitleId));

            //    // delete the subscription
            //    subscriptionToDelete.Update(source, false);

            //    // remove from subscription list
            }

            void OnSubbedComicTapped(object sender, ItemTappedEventArgs e)
        {
            //go to comic page
        }

        void OnUnsubbedComicTapped(object sender, ItemTappedEventArgs e)
        {
            var subscription = new Subscription
            {
                UserId = (int)Application.Current.Properties["userId"],
                IsActive = true,
                ComicTitleId =  1 // get bound comicTitleId
            };

            using (IDbConnection source = new SqlConnection("Server=tcp:thedailybugle.database.windows.net,1433;Initial Catalog=The Daily Bugle;Persist Security Info=False;User ID=dbadmin;Password=1231!#ASDF!a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                // assign the subscriptionId to the primary key that will be returned
                subscription.SubscriptionId = subscription.Insert(source);
            }

            ((ListView)sender).SelectedItem = null; // de-select the row

            //hide unsubbed comics
            //unhide add comic button
            //unhide subbed comics?
        }
    }
}