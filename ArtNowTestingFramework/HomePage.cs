using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace ArtNowTestingFramework
{
    public sealed class HomePage : PaintingsContainingPage
    {
        private HomePage()
            : base("Купить картины современных художников и другие произведения от 1000р") { }

        [AllureStep("Enter home page")]
        public static HomePage Enter()
        {
            Driver.Url = "https://artnow.ru/";
            return new();
        }

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
