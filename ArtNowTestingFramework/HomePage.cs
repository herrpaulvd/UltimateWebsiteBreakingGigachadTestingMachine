using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace ArtNowTestingFramework
{
    /// <summary>
    /// Home page at "artnow.ru"
    /// </summary>
    public sealed class HomePage : PaintingsContainingPage
    {
        private HomePage()
            : base("Купить картины современных художников и другие произведения от 1000р")
            => CheckBrowser();

        [AllureStep("Enter home page")]
        public static HomePage Enter()
        {
            Driver.Url = "https://artnow.ru/";
            return new();
        }

        /// <summary>
        /// Click the item in the left bar to go to a specific section
        /// </summary>
        /// <param name="text">The item name</param>
        /// <param name="expectedTitle">What we expect to see in the title of the section page</param>
        /// <returns></returns>
        [AllureStep]
        public SectionPage ClickLeftMenuItem(string text, string expectedTitle)
        {
            AllureApi.SetStepName($"Click on menu item \"{text}\"");
            ClickByText("Показать еще...", 2);
            ClickByText(text, 1);
            return SectionPage.Enter(text, expectedTitle);
        }
    }
}
