using Allure.Net.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Common
{
    /// <summary>
    /// The main class to perform tests
    /// </summary>
    public class TestBase
    {
        [ThreadStatic]
        private static FirefoxDriver driver;
        protected static IWebDriver Driver => driver;

        protected void MakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var filename = $"{TestContext.CurrentContext.Test.MethodName}_screenshot_{DateTime.Now.Ticks}.png";
            var path = $"{AllureLifecycle.Instance.ResultsDirectory}\\{filename}";
            screenshot.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            AllureApi.AddAttachment(filename, "image/png", path);
        }

        protected void ClickByXPath(string xpath)
            => Driver.FindElement(By.XPath(xpath)).Click();

        protected void ClickByCssSelector(string selector)
            => Driver.FindElement(By.CssSelector(selector)).Click();

        protected void ClickByText(string text, int parentN = 0)
            => ClickByXPath($"//*[contains(text(), '{text}')]" + "/..".Repeat(parentN));

        [SetUp]
        public void StartBrowser() => driver = new();

        [TearDown]
        public void StopBrowser()
        {
            // if any error, make screenshot
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                MakeScreenshot();
            driver.Close();
        }
    }
}
