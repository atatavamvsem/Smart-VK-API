using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Threading;
using Aquality.Selenium.Configurations;
using System.Resources;
using Aquality.Selenium.Forms;

namespace VkApi
{
    public class AuthorizationPage : Form
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;
        private ITextBox PasswordInput => ElementFactory.GetTextBox(By.XPath("//input[@id='index_pass']"), "Password");
        private ITextBox EmailInput => ElementFactory.GetTextBox(By.XPath("//input[@id='index_email']"), "Email");
        private IButton AcceptButton => ElementFactory.GetButton(By.XPath("//button[@id='index_login_button']"), "Accept button");

        public AuthorizationPage() : base(By.XPath("//div[@class='game view']"), "Information page")
        {
        }

        public void TypePassword()
        {
            PasswordInput.ClearAndType(TestData.GetString("password"));
        }

        public void TypeEmail()
        {
            EmailInput.ClearAndType(TestData.GetString("login"));
        }

        public void ClickAcceptButton()
        {
            AcceptButton.ClickAndWait();
        }
    }
}
