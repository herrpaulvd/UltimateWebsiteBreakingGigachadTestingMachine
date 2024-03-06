using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNowTestingFramework
{
    /// <summary>
    /// Any page containing paintings.
    /// Favorties page has no own class, so it's presented via this.
    /// </summary>
    public class PaintingsContainingPage : ArtNowWebPage
    {
        private WebDriverWait waiter = new(Driver, TimeSpan.FromSeconds(10));

        protected PaintingsContainingPage(params string[] titleElements)
            : base(titleElements) { }

        // unclassified means that the page we are looking is not specified,
        // but it's definetly a page containg paintings
        // e.g. it is called to create favorites page class
        // for which we shall now the only thing
        // that it contains paintings we've added
        [AllureStep]
        internal static PaintingsContainingPage EnterUnclassified(string whatPage, params string[] titleElements)
        {
            AllureApi.SetStepName($"Enter " + whatPage);
            return new PaintingsContainingPage(titleElements);
        }

        // click - shall we click or not
        // returns either this page or a painting page if clicked
        [AllureStep]
        public ArtNowWebPage FindPainting(string name, bool click)
        {
            AllureApi.SetStepName($"Check that the painting '{name}' is presented in the search results"
                + (click ? " and click it" : ""));

            int staleAttempts = 10;
            IWebElement? paintingNameDiv = null;
            while (staleAttempts --> 0)
            {
                try // because of strange chromium-based browser behaviour
                {
                    // !!! SOMEHOW contains(text(), '{name}') DOES NOT WORK in this case
                    // hypothesis: <br> element breaks the logic this function works according to
                    waiter.Until(d => (paintingNameDiv =
                        Driver.FindElements(By.XPath("//div[@itemprop='name']")).FirstOrDefault(e => e.Text.Contains(name))) is not null);
                    Assert.True(paintingNameDiv is not null);
                }
                catch(StaleElementReferenceException)
                {
                    staleAttempts = 0;
                }
            }
            
            if (click)
            {
                paintingNameDiv!.Click();
                return PaintingPage.Enter(name);
            }
            return this;
        }

        // is often used
        private IWebElement GetFirstPaintingDiv()
            => Find("//div[@class='post'][1]");

        [AllureStep]
        public PaintingsContainingPage CheckFirstPaintingByName(string name)
        {
            AllureApi.SetStepName($"Check that the 1st painting has name '{name}'");
            var paintingDiv = GetFirstPaintingDiv();
            var paintingDivNameElement = Find(".//div[@itemprop='name']", paintingDiv);
            Assert.True(paintingDivNameElement.Text.Contains(name));
            return this;
        }

        // Get pure href to a painting.
        // In different pages, hrefs are in different format.
        // There are different args after '?'.
        // Somewhere the protocol (https://) is specified,
        // somewhere isn't.
        // But href is a good key to identify paintings,
        // if we are looking for a way to compare two.
        private static string CutHref(string href)
        {
            int artnowIndex = href.IndexOf("artnow");
            if (artnowIndex == -1) artnowIndex = 0;
            int qmarkIndex = href.IndexOf('?');
            if (qmarkIndex == -1) qmarkIndex = href.Length;
            return href[artnowIndex..qmarkIndex];
        }

        [AllureStep("Add the 1st painting to favorites")]
        public PaintingsContainingPage AddFirstPaintingToFavorites(out string href)
        {
            var paintingDiv = GetFirstPaintingDiv();
            var paintingA = Find(".//a[1]", paintingDiv);
            href = CutHref(paintingA.GetAttribute("href"));
            Find(".//div[@class='heart']", paintingDiv).Click();
            return this;
        }

        [AllureStep("Check that the 1st painting is the expected one")]
        public PaintingsContainingPage CheckFirstPainting(string href)
        {
            var paintingDiv = GetFirstPaintingDiv();
            Find($".//a[contains(@href, '{href}')]", paintingDiv);
            return this;
        }

        [AllureStep("Add the first painting to the cart")]
        public CartDialogWindow AddFirstPaintingToCart(out string href, out string price)
        {
            var paintingDiv = GetFirstPaintingDiv();
            var paintingA = Find(".//a[1]", paintingDiv);
            href = CutHref(paintingA.GetAttribute("href"));
            var paintingPrice = Find(".//div[@class='price']", paintingDiv);
            price = paintingPrice.Text;
            Find(".//div[@class='oclick']", paintingDiv).Click();
            return CartDialogWindow.SetFocus();
        }
    }
}
