using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Application.Current.Properties[SUBSCRIPTION_PROPERTY_STRING] = JsonConvert.SerializeObject(GetSubscriptionList().Where(p => p.Name != comicTitle.Name));
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
                    Application.Current.SavePropertiesAsync();
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