using System;
using System.Collections.Generic;
using System.Text;
using TheDailyBugle.Models;
using Xamarin.Forms;

namespace TheDailyBugle.Services
{
    class ComicRepository
    {
        private string SUBSCRIPTION_PROPERTY_STRING = "Subscriptions";

        public void DeleteSubscription(ComicTitle comicTitle)
        {
            List<ComicTitle> currentSeries = GetSubscriptionList();
            if (currentSeries.Contains(comicTitle))
            {
                currentSeries.Remove(comicTitle);
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = currentSeries;
            }
            else
            {
                // DO NOTHING!!!!!!!!
            }
        }

        public List<ComicTitle> GetSubscriptionList()
        {
            return Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] as List<ComicTitle>;
        }

        public void SaveSubscription(ComicTitle comicTitle)
        {
            if (Application.Current.Properties.ContainsKey(SUBSCRIPTION_PROPERTY_STRING))
            {
                List<ComicTitle> currentSubscriptions = GetSubscriptionList();
                if (currentSubscriptions == null) currentSubscriptions = new List<ComicTitle>();
                if (currentSubscriptions.Contains(comicTitle))
                {
                    // alert whatchu talkin about willis?
                }
                else
                {
                    currentSubscriptions.Add(comicTitle);
                    Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = currentSubscriptions;
                }
            } else
            {
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = "";
                SaveSubscription(comicTitle);
            }
        }
    }
}
