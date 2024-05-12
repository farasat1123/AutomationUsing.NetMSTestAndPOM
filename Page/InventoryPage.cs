using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationUsingMSTest.Page;

namespace AutomationUsingMSTest.Pages
{
    public class InventoryPage
    {
        private IWebDriver driver;
        protected IWait<IWebDriver> wait;


        public InventoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        #region Method
        protected void WaitForElementToBePresent(By element)
        {
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
        }

        internal void ButtonClick(string buttonCaption)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(Locators.ButtonByText(buttonCaption)));
        }

        internal void AnchorClick(string anchorText)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(Locators.AnchorByText(anchorText)));
        }

        internal bool CheckForText(string text, bool waitForElementToBeVisible = false)
        {
            if (waitForElementToBeVisible)
            {
                try
                {
                    WaitForElementToBePresent(Locators.TextByText(text));
                }
                catch (Exception a)
                {
                    Console.WriteLine($"Exception occurred: {a.Message}");
                    return false;
                }
            }
            return driver.FindElements(Locators.TextByText(text)).Count == 1;
        }

        internal void goToCart()
        {
            driver.FindElement(Locators.Cart).Click();
        }

        internal string GetCartNumber()
        {
            return driver.FindElement(Locators.CartNumber).Text;
        }

        internal void AddToCartByName(string productCaption)
        {
            productCaption = productCaption.ToLower().Replace(' ', '-');
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(Locators.AddToCartByProduct(productCaption)));
        }
        #endregion

    }
}
