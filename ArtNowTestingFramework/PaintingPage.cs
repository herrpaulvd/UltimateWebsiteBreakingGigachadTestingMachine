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
    /// Page of a single painting
    /// </summary>
    public sealed class PaintingPage : ArtNowWebPage
    {
        private PaintingPage(params string[] titleElements) : base(titleElements) { }

        [AllureStep]
        internal static PaintingPage Enter(string paintingName)
        {
            AllureApi.SetStepName($"Enter page of painting '{paintingName}'");
            return new PaintingPage(paintingName, "купить на ArtNow.ru");
        }

        /// <summary>
        /// Check description for a specific key-value pair
        /// </summary>
        /// <param name="key">e.g. 'Стиль'</param>
        /// <param name="value">e.g. 'Реализм'</param>
        /// <returns></returns>
        [AllureStep]
        public PaintingPage CheckDetailsField(string key, string value)
        {
            AllureApi.SetStepName($"Check that '{key}' is equal to '{value}'");
            Find($"//span[contains(text(), {key})]/following-sibling::a[contains(text(), {value})]");
            return this;
        }
    }
}
