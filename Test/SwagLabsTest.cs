using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationWithMSTest.Tests;
using AutomationWithMSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationUsingMSTest;
using AutomationUsingMSTest.Page;
using AutomationUsingMSTest.Pages;
using SeleniumExtras.WaitHelpers;

namespace AutomationWithMSTest.Tests
{
    [TestClass]
    public class SwagLabsTest : BaseClass
    {
        [TestMethod]
        [Priority(1)]
        public void PageTestCase()
        {

            InventoryPage inventoryPage = new InventoryPage(driver);
            Assert.AreEqual("https://www.saucedemo.com/inventory.html", driver.Url, "Inventory URL Mismatch");
            Assert.IsTrue(inventoryPage.CheckForText("Sauce Labs Backpack"), "Item not present");
            inventoryPage.AddToCartByName("Sauce Labs Bolt T-Shirt");
            inventoryPage.AddToCartByName("Sauce Labs Fleece Jacket");
            inventoryPage.AnchorClick("Sauce Labs Backpack");
            Assert.AreEqual("https://www.saucedemo.com/inventory-item.html?id=4", driver.Url, "URL Mismatch");
            inventoryPage.ButtonClick("Add to cart");
            Assert.IsTrue(inventoryPage.CheckForText("Remove"), "Button not present");


        }

        [TestMethod]
        [Priority(2)]
        public void AddToCart()
        {
            InventoryPage inventoryPage = new InventoryPage(driver);
            inventoryPage.goToCart();
            Assert.AreEqual("https://www.saucedemo.com/cart.html", driver.Url, "URL Mismatch");
            Assert.IsTrue(inventoryPage.CheckForText("Checkout"), "Button not present");



        }

        [TestMethod]
        [Priority(3)]
        public void Checkout()
        {
            InventoryPage inventoryPage = new InventoryPage(driver);
            inventoryPage.goToCart();
            inventoryPage.ButtonClick("Checkout");
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-one.html", driver.Url, "URL Mismatch");



        }

        [TestMethod]
        [Priority(4)]
        public void CheckoutScreenErrorMessageDisplayed()
        {
            InventoryPage inventoryPage = new InventoryPage(driver);
            inventoryPage.goToCart();
            inventoryPage.ButtonClick("Checkout");
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-one.html", driver.Url, "URL Mismatch");

            // Initialize Checkout page
            CheckoutPage checkoutPage = new CheckoutPage(driver);

            checkoutPage.ClickContinueButton();
            // Custom explicit wait for error message visibility
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-test='error']")));
                IWebElement errorMessage = driver.FindElement(By.CssSelector(".error-message-container.error"));
                Assert.IsTrue(errorMessage.Displayed);
                Assert.IsTrue(errorMessage.Text.Contains("Error"));
            }
            catch (NoSuchElementException)
            {
                // Handle NoSuchElementException gracefully
                Console.WriteLine("Error message not found on the page.");
            }


        }


        [TestMethod]
        [Priority(5)]
        public void CheckoutInfo()
        {
            InventoryPage inventoryPage = new InventoryPage(driver);
            inventoryPage.goToCart();
            inventoryPage.ButtonClick("Checkout");
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-one.html", driver.Url, "URL Mismatch");


            // Create an instance of TestDataRepository
            testDataRepository = new TestDataRepository();

            // Get test data from CSV file
            var testDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CheckoutScreenTestData.csv");
            var CheckoutTestData = testDataRepository.GetCheckoutTestData(testDataFilePath);

            var testDataEntry = CheckoutTestData.FirstOrDefault();

            if (testDataEntry == null)
            {
                throw new InvalidOperationException("No test data found in the CSV file.");
            }

            // Initialize Checkout page
            CheckoutPage checkoutPage = new CheckoutPage(driver);


            // Perform checkout using test data
            checkoutPage.PerformCheckout(testDataEntry.firstName, testDataEntry.lastName, testDataEntry.zipCode);
            Assert.AreEqual("https://www.saucedemo.com/checkout-step-two.html", driver.Url, "URL Mismatch");
            checkoutPage.ButtonClick("Finish");
            Assert.AreEqual("https://www.saucedemo.com/checkout-complete.html", driver.Url, "URL Mismatch");
            checkoutPage.CheckForText("Thank you for your order!");
            Assert.IsTrue(inventoryPage.CheckForText("Back Home"), "Button not present");
            checkoutPage.ButtonClick("Back Home");


        }


        [TestMethod]
        [Priority(6)]

        public void WrongCredentialsErrorMessage()
        {


            // Initialize login page
            LoginPage loginPage = new LoginPage(driver);


            loginPage.logout();

            // Create an instance of TestDataRepository
            testDataRepository = new TestDataRepository();

            // Get test data from CSV file
            var testDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_data.csv");
            var testData = testDataRepository.GetLoginTestData(testDataFilePath);

            var testDataEntry = testData[6];

            if (testDataEntry == null)
            {
                throw new InvalidOperationException("No test data found in the CSV file.");
            }

            // Perform login using test data
            loginPage.PerformLogin(testDataEntry.Username, testDataEntry.Password);


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //  wait.Until(driver => driver.FindElement(By.Name("user-name")).Displayed);



            // Custom explicit wait for error message visibility
            wait.Until(driver =>
            {
                try
                {
                    IWebElement errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));
                    return errorMessage.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

            IWebElement errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.IsTrue(errorMessage.Text.Contains("Epic sadface"));
            Assert.AreEqual("https://www.saucedemo.com/", driver.Url, "URL Mismatch");

        }


    }
}