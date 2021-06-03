using Aquality.Selenium.Browsers;
using NUnit.Framework;

namespace VkApi
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            AqualityServices.Browser.GoTo("https://vk.com/");
            //AqualityServices.Browser.Maximize();
        }

        [Test]
        public void Test1()
        {
            var infoPage = new AuthorizationPage();
            infoPage.TypePassword();
            infoPage.TypeEmail();
            infoPage.ClickAcceptButton();
        }

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                //AqualityServices.Browser.Quit();
            }
        }
    }
}