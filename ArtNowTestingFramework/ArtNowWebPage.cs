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
    public abstract class ArtNowWebPage : TestBase
    {
        protected IWebElement Find(string xpath, IWebElement? parent = null)
            => ((ISearchContext?)parent ?? Driver).FindElement(By.XPath(xpath));

        protected void ClickByText(string text, int parentN = 0)
            => Find($"//*[contains(text(), '{text}')]" + "/..".Repeat(parentN)).Click();

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

        public TPage Assume<TPage>() where TPage : ArtNowWebPage
            => (TPage)this;

        [AllureStep("Click favorites button")]
        public PaintingsContainingPage ClickFavorties()
        {
            Find("//img[@alt='Избранное']").Click();
            return PaintingsContainingPage.EnterUnclassified("favorites page", "Избранное");
        }
    }
}
