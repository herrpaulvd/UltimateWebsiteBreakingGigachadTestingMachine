using ArtNowTestingFramework;
using NUnit.Allure.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:LevelOfParallelism(4)] // N of threads to run tests
// if unspecified, max available
namespace UltimateWebsiteBreakingGigachadTestingMachine
{
    /// <summary>
    /// The tests itself
    /// </summary>
    [AllureNUnit] // allure support
    [Parallelizable(ParallelScope.All)] // ||-support
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(EdgeDriver))] // chromium based too, so it's like Chrome
    public class Tests<TBrowserDriver> : Common.TestBase
        where TBrowserDriver : IWebDriver, new()
    {
        public Tests() : base(typeof(TBrowserDriver)) { }

        /// <summary>
        /// Subscenario: goto "Вышитые картины"
        /// and find (and maybe click) "Городской пейзаж"
        /// </summary>
        private ArtNowWebPage TramwayOpening(bool click)
            => HomePage
            .Enter()
            .ClickLeftMenuItem("Вышитые картины",
                "Купить вышитые картины")
            .MakeSectionRelatedQuery("Городской пейзаж")
            .FindPainting("Трамвайный путь", click)
            ;


        [Test]
        public void Fail_2_1()
            => HomePage
            .Enter()
            .ClickLeftMenuItem("Вышитые картины",
                "Купить вышитые картины")
            .MakeSectionRelatedQuery("Городской пейзаж")
            .FindPainting("Трамвайный путъ", false)
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
                "Купить картины в технике батик")
            .AddFirstPaintingToFavorites(out var href)
            .ClickFavorties()
            .CheckFirstPainting(href)
            ;

        private const string q_2_4 = "Жираф";
        /// <summary>
        /// Scenario #2.4
        /// </summary>
        [Test]
        public void Do_2_4()
            => HomePage
            .Enter()
            .MakeQuery(q_2_4)
            .CheckFirstPaintingByName(q_2_4)
            ;

        /// <summary>
        /// Scenario #2.5
        /// </summary>
        [Test]
        public void Do_2_5()
            => HomePage
            .Enter()
            .ClickLeftMenuItem("Ювелирное искусство",
                "Купить авторские ювелирные украшения")
            .AddFirstPaintingToCart(out var href, out var price)
            .ClickGotoCartButton()
            .CheckItem(href, price)
            ;
    }
}
