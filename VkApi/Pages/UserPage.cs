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

        private IButton ActionMoreButton => ElementFactory.GetButton(By.XPath("//a[@class='pv_actions_more']"), "ActionMore button");
        private IButton CloseButton => ElementFactory.GetButton(By.XPath("//div[contains(@class,'pv_close_btn')]"), "Close button");
        private IButton OpenOriginalButton => ElementFactory.GetButton(By.XPath("//a[@id='pv_more_act_download']"), "OpenOriginal button");

        public UserPage() : base(By.XPath("//img[@class='page_avatar_img']"), "User page")
        {
        }

        internal void ShowMoreComments(string postId)
        {
            IButton ShowMoreButton = ElementFactory.GetButton(By.XPath($"//div[@id='replies{userId}_{postId}']//a"), "ShowMore button");
            ShowMoreButton.ClickAndWait();
        }

        internal string GetPostAuthor(string postId)
        {
            ITextBox PostAuthorTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[@class='post_header_info']//a[@class='author']"), "PostAuthor textBox");
            return PostAuthorTextBox.GetAttribute("href");
        }

        internal string GetCommentAuthor(string postId)
        {
            ITextBox CommentAuthorTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[@class='reply_author']//a[@class='author']"), "CommentAuthor TextBox");
            return CommentAuthorTextBox.GetAttribute("data-from-id");
        }

        internal string GetPostText(string postId)
        {
            ITextBox usernameTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[contains(@class,'wall_post_text')]"), "Accept button");
            return usernameTextBox.GetText();
        }

        internal void ClickImage(string postId)
        {
            IButton imagePost = ElementFactory.GetButton(By.XPath($"//div[@id='wpt{userId}_{postId}']//a"), "imagePost");
            imagePost.ClickAndWait();
        }

        internal void ClickActionMore()
        {
            ActionMoreButton.ClickAndWait();
        }

        internal void ClickLikeButton(string postId)
        {
            IButton LikeButton = ElementFactory.GetButton(By.XPath($"//div[contains(@class,'like_wall{userId}_{postId}')]//a[contains(@class,'_like')]"), "Like button");
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
            ITextBox comment = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{postId}']//div[@class='reply_text']"), "comment");
            return comment.GetText();
        }

        internal bool GetPost(String listPosts)
        {
            try
            {
                var postTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='{userId}_{listPosts}']"), " ");
                return postTextBox.GetAttribute("class").Contains("unshown");
            }
            catch
            {
                return false;
            }
        }
    }
}
