using Allure.Net.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Common
{
    /// <summary>
    /// The base class to perform tests
    /// </summary>
    public class TestBase
    {
        private Type? TBrowserDriver;
        [ThreadStatic] // to support multithreading
        private static IWebDriver driver;
        protected static IWebDriver Driver => driver;

        public TestBase(Type? TBrowserDriver = null)
            => this.TBrowserDriver = TBrowserDriver;

        /// <summary>
        /// Save screeenshot with unique name
        /// </summary>
        protected void MakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var filename = $"{TestContext.CurrentContext.Test.MethodName}_screenshot_{DateTime.Now.Ticks}.png";
            var path = $"{AllureLifecycle.Instance.ResultsDirectory}\\{filename}";
            screenshot.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            AllureApi.AddAttachment(filename, "image/png", path);
        }

        [SetUp]
        public void StartBrowser()
            => driver = (IWebDriver)Activator.CreateInstance(TBrowserDriver!)!;

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
