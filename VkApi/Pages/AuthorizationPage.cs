using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Threading;
using Aquality.Selenium.Configurations;

namespace VkApi
{
    public class AuthorizationPage : BaseAppForm
    {
        private ITextBox PasswordInput => ElementFactory.GetTextBox(By.XPath("//input[@id='index_pass']"), "Password");
        private ITextBox EmailInput => ElementFactory.GetTextBox(By.XPath("//input[@id='index_email']"), "Email");
        private IButton AcceptButton => ElementFactory.GetButton(By.XPath("//button[@id='index_login_button']"), "Accept button");

        public AuthorizationPage() : base(By.XPath("//div[@class='game view']"), "Information page")
        {
        }

        public void TypePassword()
        {
            PasswordInput.ClearAndType("PuV6j_.2&$m9h?UYY");
        }

        public void TypeEmail()
        {
            EmailInput.ClearAndType("+375291660762");
        }

        public void ClickAcceptButton()
        {
            AcceptButton.ClickAndWait();
        }
    }
}
