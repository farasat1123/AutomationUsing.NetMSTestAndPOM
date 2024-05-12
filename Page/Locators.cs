using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationUsingMSTest.Page
{
    public static class Locators
    {
        public static By Username = By.Id("user-name");
        public static By Password = By.Id("password");
        public static By LoginBtn = By.Id("login-button");
        public static By SideMenuBtn = By.ClassName("bm-burger-button");
        public static By LogoutBtn = By.Id("logout_sidebar_link");

        public static By FirstName = By.Id("first-name");
        public static By LastName = By.Id("last-name");
        public static By ZipCode = By.Id("postal-code");
        public static By ContinueButton = By.Id("continue");
        public static By AddToCartByProduct(string productId) => By.XPath($"//button[contains(@id,'{productId}')][contains(@class,'btn') or contains(@class,'BTN')]");
        public static By ButtonByText(string buttonText) => By.XPath($"//button[text()='{buttonText}'][contains(@class,'btn') or contains(@class,'BTN')]");
        public static By TextByText(string text) => By.XPath($"//*[text()='{text}']");
        public static By AnchorByText(string anchorText) => By.XPath($".//div[text()='{anchorText}']");
        public static By Cart = By.ClassName("shopping_cart_link");
        public static By CartNumber = By.ClassName("shopping_cart_badge");
    }
}