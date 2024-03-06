using Allure.Net.Commons;
using NUnit.Allure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNowTestingFramework
{
    /// <summary>
    /// Cart page actually
    /// </summary>
    public sealed class CartPage : ArtNowWebPage
    {
        private CartPage() : base("Корзина покупок") { }

        [AllureStep("Enter cart page")]
        public static CartPage Enter()
            => new();

        [AllureStep("Check that the selected item is in the cart with the same price")]
        public CartPage CheckItem(string href, string price)
        {
            var paintingA = Find($"//div[@class='c_cell']//a[contains(@href, '{href}')]");
            Find($"../following-sibling::div[@class='shop']/div[@class='price' and text()='{price}']", paintingA);
            return this;
        }
    }
}
