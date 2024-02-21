using Allure.Net.Commons;
using NUnit.Allure.Core;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UltimateWebsiteBreakingGigachadTestingMachine
{
    [AllureNUnit]
    public class Tests : Common.TestBase
    {
        [Test]
        public void Test()
        {
            Driver.Url = "http://www.google.co.in";
        }

        [Test]
        public void FailPlease()
        {
            Driver.Url = "ssh://git@unicornland.us:3388";
        }
    }
}
