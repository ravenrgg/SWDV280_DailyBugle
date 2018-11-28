using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheDailyBugle.Models;
using TheDailyBugle.Services;
using Xamarin.Forms;

namespace TheDailyBugle.Test.UnitTests
{
    [TestClass]
    public class ComicRepositoryTests
    {
        [ClassInitialize]
        public static void SetupApp(TestContext testContext)
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
        }

        [TestMethod]
        public void VerifyRepoSavesComicTitles()
        {
            // Setup
            ComicRepository cr = new ComicRepository();
            List<ComicTitle> comicList = new List<ComicTitle>();
            cr.DeleteAllSubscriptions();

            // Arrange
            for (var i = 0; i < 5; i++)
            {
                comicList.Add(new ComicTitle
                {
                    Name = "TestTitle" + i,
                    Url = "TestUrl" + i,
                    IconUrl = "TestIconUrl" + i
                });
            }

            // Act
            cr.SaveSubscription(comicList[0]);
            List<ComicTitle> comicTitlesFromStorage = cr.GetSubscriptionList();

            // Assert
            Assert.AreEqual(comicList[0].Name, comicTitlesFromStorage[0].Name);
        }
    }
}
