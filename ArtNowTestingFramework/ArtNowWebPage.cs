using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using NUnit.Allure.Attributes;
using Allure.Net.Commons;

namespace ArtNowTestingFramework
{
    public abstract class ArtNowWebPage : TestBase
    {
        protected static void ClickByXPath(string xpath)
            => Driver.FindElement(By.XPath(xpath)).Click();

        protected static void ClickByCssSelector(string selector)
            => Driver.FindElement(By.CssSelector(selector)).Click();

        [AllureStep]
        protected static void ClickByText(string text, int parentN = 0)
        {
            AllureApi.SetStepName($"Click on the element being the ancestor #{parentN} of the element containing text '{text}'");
            ClickByXPath($"//*[contains(text(), '{text}')]" + "/..".Repeat(parentN));
        }

        [AllureStep]
        protected static void CheckTitle(string title)
        {
            AllureApi.SetStepName("Check that the page title is " + title);
            Driver.FindElement(By.XPath($"//title[contains(text(), '{title}')]"));
        }
    }
}
