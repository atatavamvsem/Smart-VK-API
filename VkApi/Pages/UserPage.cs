using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using VkApi.DataEntities;

namespace VkApi
{
    class UserPage : BaseAppForm
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;
        private string userId = TestData.GetString("userId");

        private IButton ActionMoretButton => ElementFactory.GetButton(By.XPath("//a[@class='pv_actions_more']"), "ActionMoretButton button");
        private IButton CloseButton => ElementFactory.GetButton(By.XPath("//div[contains(@class,'pv_close_btn')]"), "CloseButton button");
        private IButton OpenOriginalButton => ElementFactory.GetButton(By.XPath("//a[@id='pv_more_act_download']"), "OpenOriginalButton button");

        public UserPage() : base(By.XPath("//img[@class='page_avatar_img']"), "User page")
        {
        }

        internal void ShowMoreComments(string postId)
        {
            IButton ShowMoreButton = ElementFactory.GetButton(By.XPath($"//div[@id='replies{userId}_{postId}']//a"), "Accept button");
            ShowMoreButton.ClickAndWait();
        }

        internal string GetPostAuthor(string postId)
        {
            ITextBox usernameTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[@class='post_header_info']//a[@class='author']"), "Accept button");
            return usernameTextBox.GetAttribute("href");
        }

        internal string GetComentAuthor(string postId)
        {
            ITextBox usernameTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[@class='reply_author']//a[@class='author']"), "Accept button");
            return usernameTextBox.GetAttribute("data-from-id");
        }

        internal string GetPostText(string postId)
        {
            ITextBox usernameTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[contains(@class,'wall_post_text')]"), "Accept button");
            return usernameTextBox.GetText();
        }

        internal void ClickImage(string postId)
        {
            IButton AcceptButton = ElementFactory.GetButton(By.XPath($"//div[@id='wpt{userId}_{postId}']//a"), "Accept button");
            AcceptButton.ClickAndWait();
        }

        internal void ClickActionMore()
        {
            ActionMoretButton.ClickAndWait();
        }

        internal void ClickLikeButton(string postId)
        {
            IButton LikeButton = ElementFactory.GetButton(By.XPath($"//div[contains(@class,'like_wall{userId}_{postId}')]//a[contains(@class,'_like')]"), "Accept button");
            LikeButton.ClickAndWait();
        }
        internal void ClickCloseButton()
        {
            CloseButton.ClickAndWait();
        }

        internal string GetUrl()
        {
            return OpenOriginalButton.GetAttribute("href");
        }

        internal string GetComment(string postId)
        {
            ITextBox comment = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[@class='reply_text']"), "Accept button");
            return comment.GetText();
        }

        internal bool GetPost(String listPosts)
        {
            try
            {
                var usernameTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{listPosts}']"), "Accept button");
                return usernameTextBox.GetAttribute("class").Contains("unshown");
            }
            catch
            {
                return false;
            }
        }
    }
}
