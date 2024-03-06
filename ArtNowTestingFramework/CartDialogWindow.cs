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
    /// Dialog window appearing when clicking add-to-cart button
    /// </summary>
    public sealed class CartDialogWindow : WebElementContainer
    {
        private readonly IWebElement window;
        private CartDialogWindow()
        {
            window = Find("//div[@class='cmodal' and @id='cmodal']");
        }

        [AllureStep("Check that a dialog window appeared")]
        public static CartDialogWindow SetFocus()
            => new();

        [AllureStep("Click goto cart button")]
        public CartPage ClickGotoCartButton()
        {
            Find(".//button[@class='ok-button']", window).Click();
            return CartPage.Enter();
        }
    }
}
