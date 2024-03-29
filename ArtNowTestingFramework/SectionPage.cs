﻿using Allure.Net.Commons;
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
    /// <summary>
    /// Page of a specific section, e.g. 'Батик'
    /// </summary>
    public sealed class SectionPage : PaintingsContainingPage
    {
        private string title;

        private SectionPage(string title)
            : base(title) => this.title = title;

        [AllureStep]
        internal static SectionPage Enter(string sectionName, string expectedTitle)
        {
            AllureApi.SetStepName($"Enter section '{sectionName}'s page");
            return new(expectedTitle);
        }

        // means in the left bar
        [AllureStep]
        public SectionPage MakeSectionRelatedQuery(string query)
        {
            AllureApi.SetStepName($"Search for '{query}' in current section");
            var input = Find("//input[@id='searchstr']");
            input.SendKeys(query + "\n");
            Find("../..//button", input).Click();
            CheckTitle(title);
            return this;
        }
    }
}
