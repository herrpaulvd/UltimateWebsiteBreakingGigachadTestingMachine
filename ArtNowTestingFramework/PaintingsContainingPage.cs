using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNowTestingFramework
{
    /// <summary>
    /// Any page containing paintings. To be inherited.
    /// </summary>
    public class PaintingsContainingPage : ArtNowWebPage
    {
        protected PaintingsContainingPage(params string[] titleElements)
            : base(titleElements) { }

        [AllureStep]
        internal static PaintingsContainingPage EnterUnclassified(string whatPage, params string[] titleElements)
        {
            AllureApi.SetStepName($"Enter " + whatPage);
            return new PaintingsContainingPage(titleElements);
        }

        [AllureStep]
        public ArtNowWebPage FindPainting(string name, bool click)
        {
            AllureApi.SetStepName($"Check that the painting '{name}' is presented in the search results"
                + (click ? " and click it" : ""));
            var painting = Find($"//img[contains(@alt, '{name}')]");
            if (click)
            {
                painting.Click();
                return PaintingPage.Enter(name);
            }
            return this;
        }

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
            var paintingDiv = Find("//div[@class='post'][1]");
            var paintingA = Find(".//a[1]", paintingDiv);
            href = CutHref(paintingA.GetAttribute("href"));
            Find(".//div[@class='heart']", paintingDiv).Click();
            return this;
        }

        [AllureStep("Check that the 1st painting is the expected one")]
        public PaintingsContainingPage CheckFirstPainting(string href)
        {
            var paintingDiv = Find("//div[@class='post'][1]");
            Find($".//a[contains(@href, '{href}')]", paintingDiv);
            return this;
        }
    }
}
