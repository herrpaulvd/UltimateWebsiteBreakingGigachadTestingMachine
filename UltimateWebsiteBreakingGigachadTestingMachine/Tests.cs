using ArtNowTestingFramework;
using NUnit.Allure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateWebsiteBreakingGigachadTestingMachine
{
    /// <summary>
    /// The tests itself
    /// </summary>
    [AllureNUnit]
    public class Tests : Common.TestBase
    {
        /// <summary>
        /// Scenario #2.1
        /// </summary>
        [Test]
        public void Do_2_1()
        {
            HomePage
                .Enter()
                .ClickLeftMenuItem("Вышитые картины", "Купить вышитые картины в интернет магазине ArtNow")
                .MakeSectionRelatedQuery("Городской пейзаж")
                .FindPainting("Трамвайный путь")
                //.FindPainting("Гвоздец")
            ;
        }
    }
}
