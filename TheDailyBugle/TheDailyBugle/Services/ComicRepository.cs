using Newtonsoft.Json;
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
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = JsonConvert.SerializeObject(currentSeries);
            }
            else
            {
                // DO NOTHING!!!!!!!!
            }
            Application.Current.SavePropertiesAsync();
        }

        public List<ComicTitle> GetSubscriptionList()
        {
            if (!Application.Current.Properties.ContainsKey(SUBSCRIPTION_PROPERTY_STRING))
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = "";
            return JsonConvert.DeserializeObject<List<ComicTitle>>(Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] as string);
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
                    Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = JsonConvert.SerializeObject(currentSubscriptions);
                    Application.Current.SavePropertiesAsync().Wait();
                }
            }
            else
            {
                Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = "";
                SaveSubscription(comicTitle);
            }
        }
    }
}