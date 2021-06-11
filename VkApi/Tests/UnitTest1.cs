using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System.Collections.Generic;
using Tests;
using VkApi.DataEntities;

namespace VkApi
{
    internal class Tests : BaseTest
    {
        [Test]
        public void Test1()
        {
            var infoPage = new AuthorizationPage();
            infoPage.TypePassword();
            infoPage.TypeEmail();
            infoPage.ClickAcceptButton();
            var feedPage = new FeedPage();
            feedPage.ClickAcceptButton();

            HTTPUtils.CreatePostPostsRequest();
            ResponseId listPosts = HTTPUtils.CreateResponse<ResponseId>();
            var userPage = new UserPage();

            Assert.IsTrue(userPage.GetPost(listPosts.PostId.ToString()));
        }

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                //AqualityServices.Browser.Quit();
            }
        }
    }
}