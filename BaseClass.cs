using AutomationUsingMSTest.Pages;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutomationUsingMSTest
{
    public class BaseClass
    {
        protected static IWebDriver driver;
        protected TestDataRepository testDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            // Create an instance of TestDataRepository
            testDataRepository = new TestDataRepository();

            // Get test data from CSV file
            var testDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_data.csv");
            var testData = testDataRepository.GetLoginTestData(testDataFilePath);

            var testDataEntry = testData.FirstOrDefault();

            if (testDataEntry == null)
            {
                throw new InvalidOperationException("No test data found in the CSV file.");
            }

            // Initialize login page
            LoginPage loginPage = new LoginPage(driver);

            loginPage.NavigateToLoginPage();

            // Perform login using test data
            loginPage.PerformLogin(testDataEntry.Username, testDataEntry.Password);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}