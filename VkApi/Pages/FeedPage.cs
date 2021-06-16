using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace VkApi
{
    public class FeedPage : BaseAppForm
    {
        private IButton MyPageButton => ElementFactory.GetButton(By.XPath("//li[@id='l_pr']"), "Accept button");

        public FeedPage() : base(By.XPath("//div[@id='feed_rmenu']"), "Information page")
        {
        }

        public void ClickMyPageButton()
        {
            MyPageButton.ClickAndWait();
        }
    }
}
