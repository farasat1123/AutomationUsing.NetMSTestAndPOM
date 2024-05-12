using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationUsingMSTest.Page
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        protected IWait<IWebDriver> wait;


        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        #region Methods

        protected void WaitForElementToBePresent(By element)
        {
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
        }


        internal void ButtonClick(string buttonCaption)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(Locators.ButtonByText(buttonCaption)));
        }

        public void ClickContinueButton()
        {
            driver.FindElement(Locators.ContinueButton).Click();
        }


        public void PerformCheckout(string firstNameData, string lastNameData, string zipCodeData)
        {
            driver.FindElement(Locators.FirstName).SendKeys(firstNameData);
            driver.FindElement(Locators.LastName).SendKeys(lastNameData);
            driver.FindElement(Locators.ZipCode).SendKeys(zipCodeData);
            driver.FindElement(Locators.ContinueButton).Click();
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
                    // Log or handle the exception appropriately
                    Console.WriteLine($"Exception occurred: {a.Message}");
                    return false;
                }
            }
            return driver.FindElements(Locators.TextByText(text)).Count == 1;
        }


        #endregion
    }
}
