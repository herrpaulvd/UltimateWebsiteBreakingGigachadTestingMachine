using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UltimateWebsiteBreakingGigachadTestingMachine
{
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

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}