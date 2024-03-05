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
        /// Subscenario: goto "Вышитые картины"
        /// and find (and maybe click) "Городской пейзаж"
        /// </summary>
        private ArtNowWebPage TramwayOpening(bool click)
            => HomePage
            .Enter()
            .ClickLeftMenuItem("Вышитые картины",
                "Купить вышитые картины в интернет магазине ArtNow")
            .MakeSectionRelatedQuery("Городской пейзаж")
            .FindPainting("Трамвайный путь", click)
            ;

        /// <summary>
        /// Scenario #2.1
        /// </summary>
        [Test]
        public void Do_2_1()
            => TramwayOpening(false);

        /// <summary>
        /// Scenario #2.2
        /// </summary>
        [Test]
        public void Do_2_2()
            => TramwayOpening(true)
            .Assume<PaintingPage>()
            .CheckDetailsField("Стиль", "Реализм")
            ;

        /// <summary>
        /// Scenario #2.3
        /// </summary>
        [Test]
        public void Do_2_3()
            => HomePage
            .Enter()
            .ClickLeftMenuItem("Батик",
                "Купить картины в технике батик в интернет магазине ArtNow")
            .AddFirstPaintingToFavorites(out var href)
            .ClickFavorties()
            .CheckFirstPainting(href)
            ;
    }
}
