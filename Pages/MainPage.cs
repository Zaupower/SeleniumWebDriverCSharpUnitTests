using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace sleeniumTest.Pages
{
    public class MainPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "authorization-link")]
        private IWebElement _signInButton;

        [FindsBy(How = How.ClassName, Using = "logged-in")]
        private IWebElement _welcomeMessage;

        [FindsBy(How = How.LinkText, Using = "Create an Account")]
        private IWebElement _createAnAccount;
        
        [FindsBy(How = How.Id, Using = "top-cart-btn-checkout")]
        private IWebElement _checkoutButton;

        [FindsBy(How = How.CssSelector, Using = "a.action.showcart")]
        private IWebElement _cartButton;
        
        [FindsBy(How = How.Id, Using = "ui-id-1")]
        private IWebElement _cartBlock;

        [FindsBy(How = How.ClassName, Using = "messages")]
        private IWebElement _alertMessage;



        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        public CustomerLoginPage ClickSignInButton()
        {
            _signInButton.Click();

            return new CustomerLoginPage(_driver);
        }

        public CreateAccountPage ClickCreateAnAccountButton()
        {
            _createAnAccount.Click();

            return new CreateAccountPage(_driver);
        }
        public string GetSignInButtonText()
        {            
            return _signInButton.Text;
        }
        public string GetWelcomeMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _welcomeMessage.Text.StartsWith("Welcome, "));
            
            return _welcomeMessage.Text;
        }

        public void ProccedToCheckout()
        {
            ClickCartButton();
            WebDriverWait waitForButton = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waitForButton.Until(ExpectedConditions.ElementToBeClickable(_checkoutButton)).Click();
            

        }
        public void ClickCartButton()
        {
            _cartButton.Click();

        }

        public void ClickCheckoutButton()
        {
           
            _checkoutButton.Click();
        }

        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _alertMessage.Text.StartsWith("You added "));

            IWebElement alert = _alertMessage;

            return alert.Text;
        }
    }
}
