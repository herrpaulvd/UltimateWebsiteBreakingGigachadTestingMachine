using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace ArtNowTestingFramework
{
    public class HomePage : ArtNowWebPage
    {
        private HomePage() { }

        [AllureStep("Enter home page")]
        public static HomePage Enter()
        {
            var result = new HomePage();
            Driver.Url = "https://artnow.ru/";
            CheckTitle("Купить картины современных художников и другие произведения от 1000р");
            return result;
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
