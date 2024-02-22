using Allure.Net.Commons;
using ArtNowTestingFramework;
using NUnit.Allure.Core;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UltimateWebsiteBreakingGigachadTestingMachine
{
    [AllureNUnit]
    public class Test_Tests : Common.TestBase
    {
        [Test]
        public void Test()
        {
            var page = HomePage.Enter();
            page.ClickLeftMenuItem("Батик");
        }
    }
}
