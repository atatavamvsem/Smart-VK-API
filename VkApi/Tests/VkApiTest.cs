using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;

namespace VkApi
{
    internal class VkApiTests : BaseTest
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;
        private string userId = TestData.GetString("userId");

        private string postId;
        private string randomString = GenerateRandom.GenerateEnglishString(Int32.Parse(TestData.GetString("lenghtRandomString")));
        private string otherString = GenerateRandom.GenerateEnglishString(Int32.Parse(TestData.GetString("lenghtRandomString")));
        private string randomComment = GenerateRandom.GenerateEnglishString(Int32.Parse(TestData.GetString("lenghtRandomString")));

        [Test]
        public void ApiUiTest()
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
                Assert.AreEqual(TestData.GetString("postAuthor"), userPage.GetPostAuthor(postId), "Wrong author");
                Assert.AreEqual(randomString, userPage.GetPostText(postId), "Text diferents");
            });

            HTTPUtils.ChangePost(otherString, postId);

            userPage.ClickImage(postId);
            userPage.ClickActionMore();
            HTTPUtils.DownloadPhoto(userPage.GetUrl());
            userPage.ClickCloseButton();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(otherString, userPage.GetPostText(postId), "Text diferents");
                Assert.IsTrue(FileUtil.CompareImages(Image.FromFile(TestData.GetString("imageUpload")), Image.FromFile(TestData.GetString("imageDownload"))));
            });

            HTTPUtils.CreateComment(randomComment, postId);
            userPage.ShowMoreComments(postId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(randomComment, userPage.GetComment(postId), "Text diferents");
                Assert.AreEqual(userId, userPage.GetCommentAuthor(postId), "Wrong author");
            });

            userPage.ClickLikeButton(postId);
            Assert.AreEqual(userId, HTTPUtils.GetLikes(postId), "Wrong author");
            HTTPUtils.DeleatePost(postId);
            Assert.IsFalse(userPage.GetPost(postId));
        }
    }
}