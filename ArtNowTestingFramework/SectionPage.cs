using Allure.Net.Commons;
using Common;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNowTestingFramework
{
    public class SectionPage : ArtNowWebPage
    {
        private string title;

        private SectionPage(string title) => this.title = title;

        [AllureStep]
        public static SectionPage Enter(string sectionName, string expectedTitle)
        {
            AllureApi.SetStepName($"Enter section '{sectionName}'s page");
            CheckTitle(expectedTitle);
            return new SectionPage(expectedTitle);
        }

        [AllureStep]
        public SectionPage MakeSectionRelatedQuery(string query)
        {
            AllureApi.SetStepName($"Search for '{query}'");
            var input = Driver.FindElement(By.XPath("//input[@id='searchstr']"));
            input.SendKeys(query + "\n");
            input.FindElement(By.XPath("../..//button")).Click();
            CheckTitle(title);
            return this;
        }

        [AllureStep]
        public SectionPage FindPainting(string name)
        {
            AllureApi.SetStepName($"Check that the painting '{name}' is presented in the search results");
            Driver.FindElement(By.XPath($"//img[contains(@alt, '{name}')]"));
            return this;
        }
    }
}
