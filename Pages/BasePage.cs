using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sleeniumTest.Pages
{
    public class BasePage
    {
        [FindsBy(How = How.Id, Using = "ui-id-6")]
        private IWebElement _gearCategoryButton;
        
        [FindsBy(How = How.Id, Using = "ui-id-27")]
        private IWebElement _watchesCategoryButton;

        [FindsBy(How = How.ClassName, Using = "messages")]
        private IWebElement _alertMessage;

        [FindsBy(How = How.CssSelector, Using = "button.action.switch")]
        private IWebElement _dropdownSwitchButton;

        [FindsBy(How = How.Id, Using = "top-cart-btn-checkout")]
        internal IWebElement _checkoutButton;

        [FindsBy(How = How.CssSelector, Using = "a.action.showcart")]
        internal IWebElement _cartButton;

        protected readonly IWebDriver _driver;
        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public ProductPage OpenGearNavigationButton()
        {
            _gearCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            return new ProductPage(_driver);
        }

        public ProductPage OpenWatchesNavigationButton()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(_gearCategoryButton);
            actions.Perform();

            WebDriverWait waitForButton = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            _watchesCategoryButton = waitForButton.Until(ExpectedConditions.ElementToBeClickable(_watchesCategoryButton));
            _watchesCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Watches"));

            return new ProductPage(_driver);
        }

        public void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(element);
            actions.Perform();
        }

        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _alertMessage.Text.StartsWith("You added "));

            IWebElement alert = _alertMessage;

            return alert.Text;
        }

        public int GetCartCounter()
        {           
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".counter.qty")));            
            
            var cart = _driver.FindElement(By.CssSelector(".action.showcart"));
            
            wait.Until(ExpectedConditions.ElementToBeClickable(cart));
            
            ScrollToElement(cart);
            
            var counterCart = cart.FindElement(By.CssSelector(".counter.qty"));
            
            string counterCartText = counterCart.FindElement(By.CssSelector(".counter-number")).Text;
            
            return int.Parse(counterCartText);
        }
        public CostumerPage ClickMyAccountButton()
        {
            _dropdownSwitchButton.Click();
            WebDriverWait waitForButton = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement dropdown = waitForButton.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ul.header.links")));
            IWebElement myAccountButton = dropdown.FindElement(By.LinkText("My Account"));
            myAccountButton.Click();
            return new CostumerPage(_driver);
        }

        public void ClickCartButton()
        {
            ScrollToElement(_cartButton);
            _cartButton.Click();
        }
    }
}
