using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using NUnit.Allure.Attributes;
using Allure.Net.Commons;
using System.IO;

namespace ArtNowTestingFramework
{
    /// <summary>
    /// Parent of all pages
    /// </summary>
    public abstract class ArtNowWebPage : WebElementContainer
    {
        /// <summary>
        /// Click on the n-th ancestor of an element containg the given text
        /// </summary>
        /// <param name="text">Text to find</param>
        /// <param name="ancestorN">Ancestor degree</param>
        protected void ClickByText(string text, int ancestorN = 0)
            => Find($"//*[contains(text(), '{text}')]" + "/..".Repeat(ancestorN)).Click();

        /// <summary>
        /// Checks whether page title contains these substrings
        /// </summary>
        /// <param name="titleElements">Substrings to search</param>
        [AllureStep]
        protected void CheckTitle(params string[] titleElements)
        {
            AllureApi.SetStepName("Check that the page title contains "
                + string.Join(',', titleElements.Select(t => $"'{t}'")));

            string singleChecker(string s) => $"contains(text(), '{s}')";
            const string separator = " and ";
            Find($"//title[{string.Join(separator, titleElements.Select(singleChecker))}]");
        }

        protected ArtNowWebPage(params string[] titleElements)
            => CheckTitle(titleElements);

        // just cast designed to be method for more beautiful test look
        public TPage Assume<TPage>() where TPage : ArtNowWebPage
            => (TPage)this;

        [AllureStep("Click favorites button")]
        public PaintingsContainingPage ClickFavorties()
        {
            Find("//img[@alt='Избранное']").Click();
            return PaintingsContainingPage.EnterUnclassified("favorites page", "Избранное");
        }

        /// <summary>
        /// Make global query on top-center search bar
        /// </summary>
        /// <param name="query">String to type</param>
        /// <returns></returns>
        [AllureStep]
        public PaintingsContainingPage MakeQuery(string query)
        {
            AllureApi.SetStepName($"Global search for '{query}'");
            var searchSpan = Find("//span[@class='search-bar']");
            Find(".//input[@type='text']", searchSpan).SendKeys(query);
            Find(".//button[@type='submit']", searchSpan).Click();
            return PaintingsContainingPage.EnterUnclassified("search results page", "Подбор картин и работ");
        }
    }
}
