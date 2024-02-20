using Allure.Net.Commons;
using NUnit.Allure.Core;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UltimateWebsiteBreakingGigachadTestingMachine
{
    [AllureNUnit]
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new FirefoxDriver();
        }

        [Test]
        public void Test()
        {
            driver.Url = "http://www.google.co.in";
        }

        [Test]
        public void FailPlease()
        {
            driver.Url = "ssh://git@unicornland.us:3388";
        }

        [TearDown]
        public void CloseBrowser()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var filename = TestContext.CurrentContext.Test.MethodName + "_screenshot_" + DateTime.Now.Ticks + ".png";
                var path = $"{AllureLifecycle.Instance.ResultsDirectory}\\{filename}";
                screenshot.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
                AllureApi.AddAttachment(filename, "image/png", path);
            }
            driver.Close();
        }
    }
}