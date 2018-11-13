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

        public void DeleteSubscription(Series series)
        {
            List<Series> currentSeries = GetSubscriptionList();
            if (currentSeries.Contains(series))
            {
                currentSeries.Remove(series);
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = currentSeries;
            }
            else
            {
                // DO NOTHING!!!!!!!!
            }
        }

        public List<Series> GetSubscriptionList()
        {
            return Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] as List<Series>;
        }

        public void SaveSubscription(Series series)
        {
            if (Application.Current.Properties.ContainsKey(SUBSCRIPTION_PROPERTY_STRING))
            {
                List<Series> currentSubscriptions = GetSubscriptionList();
                if (currentSubscriptions == null) currentSubscriptions = new List<Series>();
                if (currentSubscriptions.Contains(series))
                {
                    // alert whatchu talkin about willis?
                }
                else
                {
                    currentSubscriptions.Add(series);
                    Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = currentSubscriptions;
                }
            } else
            {
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = "";
                SaveSubscription(series);
            }
        }
    }
}
