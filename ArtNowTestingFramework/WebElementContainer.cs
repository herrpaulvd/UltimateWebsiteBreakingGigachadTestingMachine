using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using OpenQA.Selenium;

namespace ArtNowTestingFramework
{
    /// <summary>
    /// Parent of all containers of web elements, incl. pages
    /// </summary>
    public abstract class WebElementContainer : TestBase
    {
        /// <summary>
        /// Find element by XPath [with specified root]
        /// </summary>
        /// <param name="xpath">Locator</param>
        /// <param name="root">Root. If null, set to Driver</param>
        /// <returns></returns>
        protected IWebElement Find(string xpath, IWebElement? root = null)
            => ((ISearchContext?)root ?? Driver).FindElement(By.XPath(xpath));
    }
}
