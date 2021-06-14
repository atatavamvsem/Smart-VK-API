using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Resources;
using System.Text;
using Tests;
using VkApi.DataEntities;

namespace VkApi
{
    internal class Tests : BaseTest
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;
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

            //Assert.IsTrue(userPage.GetPost(listPosts.PostId.ToString()));


            //HTTPUtils.UploadPhoto(listPosts.Response.PostId.ToString());

            userPage.ClickImage();

            HTTPUtils.DownloadPhoto();
            

        }

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }
    }
}