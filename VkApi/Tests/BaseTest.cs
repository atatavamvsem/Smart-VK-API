using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using RestSharp.Serialization.Json;
using VkApi;
using Aquality.Selenium.Browsers;

namespace Tests
{
    internal class BaseTest
    {

        [SetUp]
        public void Setup()
        {
            AqualityServices.Browser.GoTo("https://vk.com/");
            HTTPUtils.CreateClient();
        }

    }
}