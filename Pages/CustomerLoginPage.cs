using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class CustomerLoginPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "login[username]")]
        private IWebElement _emailInput;

        [FindsBy(How = How.Name, Using = "login[password]")]
        private IWebElement _passwordInput;

        [FindsBy(How = How.Id, Using = "send2")]
        private IWebElement _SignInFormButton;

        public CustomerLoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void LogIn(string email, string password)
        {
            InputEmail(email);
            InputPassword(password);
            ClickSignInButton();
        }
        public void InputEmail(string email)
        {
            _emailInput.SendKeys(email);
        }

        public void InputPassword(string password)
        {
            _passwordInput.SendKeys(password);
        }

        public void ClickSignInButton()
        {
            _SignInFormButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => !driver.Title.StartsWith("Costumer Login "));
        }
    }
}
