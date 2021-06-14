using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VkApi.DataEntities;

namespace VkApi
{
    class UserPage : BaseAppForm
    {
        public UserPage() : base(By.XPath("//div[@class='game view']"), "Information page")
        {
        }

        internal bool GetPost(String listPosts)
        {
            try
            {
                var usernameTextBox = ElementFactory.GetTextBox(By.XPath($"//div[@data-post-id='627657327_{listPosts}']"), "Accept button");
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        internal void ClickImage()
        {
            IButton AcceptButton = ElementFactory.GetButton(By.XPath("//a[@href='/photo627657327_457241217']"), "Accept button");
            AcceptButton.ClickAndWait();
        }
    }
}
