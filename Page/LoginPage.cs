using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationUsingMSTest.Page;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationUsingMSTest.Pages
{
    internal class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        /*
        #region Locators

        private By username = Locators.Username;
        private By password = Locators.Password;
        private By loginBtn = Locators.LoginBtn;
        private By SideMenuBtn = Locators.SideMenuBtn;
        private By logoutBtn = Locators.LogoutBtn;
        #endregion
         */
        #region Methods

        public void NavigateToLoginPage()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            Assert.AreEqual("https://www.saucedemo.com/", driver.Url, "URL Mismatch");
            Assert.AreEqual("Swag Labs", driver.Title);
        }

        public void PerformLogin(string userName, string password)
        {
            driver.FindElement(Locators.Username).SendKeys(userName);
            driver.FindElement(Locators.Password).SendKeys(password);
            driver.FindElement(Locators.LoginBtn).Click();
        }


        public void logout()
        {
            driver.FindElement(Locators.SideMenuBtn).Click();
            driver.FindElement(Locators.LogoutBtn).Click();
        }

        #endregion
    }
}
