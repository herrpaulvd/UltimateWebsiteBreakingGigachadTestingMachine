using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace ArtNowTestingFramework
{
    public class HomePage : Common.TestBase
    {
        internal HomePage() { }

        public static HomePage Enter()
        {
            var result = new HomePage();
            Driver.Url = "https://artnow.ru/";
            return result;
        }

        [AllureStep]
        public HomePage ClickLeftMenuItem(string text)
        {
            AllureApi.SetStepName($"Click on menu item \"{text}\"");
            ClickByText("Показать еще...", 2);
            ClickByText(text, 1);
            MakeScreenshot();
            return this;
        }
    }
}
