using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using RestSharp.Serialization.Json;
using VkApi;
using Aquality.Selenium.Browsers;
using System.Resources;

namespace VkApi
{
    internal class BaseTest
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;

        [SetUp]
        public void Setup()
        {
            AqualityServices.Browser.GoTo(TestData.GetString("URL"));
            HTTPUtils.CreateRestClient(TestData.GetString("apiUrl"));
        }

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }
    }
}