using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace VkApi
{
    public class FeedPage : BaseAppForm
    {
        private IButton MyPageButton => ElementFactory.GetButton(By.XPath("//li[@id='l_pr']"), "MyPage button");

        public FeedPage() : base(By.XPath("//div[@id='feed_rmenu']"), "Feed page")
        {
        }

        public void ClickMyPageButton()
        {
            MyPageButton.ClickAndWait();
        }
    }
}
