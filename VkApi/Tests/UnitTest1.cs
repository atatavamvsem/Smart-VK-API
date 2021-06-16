using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using Tests;

namespace VkApi
{
    internal class Tests : BaseTest
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;
        private string userId = TestData.GetString("userId");

        private string postId;
        private string randomString = GenerateRandom.GenerateEnglishString(5);
        private string otherString = GenerateRandom.GenerateEnglishString(5);
        private string randomComment = GenerateRandom.GenerateEnglishString(5);

        [Test]
        public void Test1()
        {
            var infoPage = new AuthorizationPage();
            infoPage.TypePassword();
            infoPage.TypeEmail();
            infoPage.ClickAcceptButton();
            var feedPage = new FeedPage();
            feedPage.ClickMyPageButton();
            var userPage = new UserPage();
            postId = HTTPUtils.CreatePost(randomString);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("https://vk.com/id627657327", userPage.GetPostAuthor(postId), "Wrong author");
                Assert.AreEqual(randomString, userPage.GetPostText(postId), "Text diferents");
            });

            HTTPUtils.ChangePost(otherString, postId);

            userPage.ClickImage(postId);
            userPage.ClickActionMore();
            HTTPUtils.DownloadPhoto(userPage.GetUrl());
            userPage.ClickCloseButton();

            Image image1 = Image.FromFile("C:\\c.jpg");
            Image image2 = Image.FromFile("D:\\b.jpg");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(otherString, userPage.GetPostText(postId), "Text diferents");
                Assert.IsTrue(FileUtil.CompareImages(image1, image2));
            });

            HTTPUtils.CreateComment(randomComment, postId);
            userPage.ShowMoreComments(postId);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(randomComment, userPage.GetComment(postId), "Text diferents");
                Assert.AreEqual(userId, userPage.GetComentAuthor(postId), "Wrong author");
            });

            userPage.ClickLikeButton(postId);

            Assert.AreEqual(userId, HTTPUtils.GetLikes(postId), "Wrong author");
            HTTPUtils.DeleatePost(postId);
            Assert.IsFalse(userPage.GetPost(postId));
        }

        
    }
}