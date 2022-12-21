using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using sleeniumTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class CheckoutPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "firstname")]
        private IWebElement _firstNameInput;

        [FindsBy(How = How.Name, Using = "lastname")]
        private IWebElement _lastNameInput;

        [FindsBy(How = How.Name, Using = "street[0]")]
        private IWebElement _streetInput;

        [FindsBy(How = How.Name, Using = "city")]
        private IWebElement _cityInput;

        [FindsBy(How = How.Name, Using = "region_id")]
        private IWebElement _regionDropdown;

        [FindsBy(How = How.Name, Using = "postcode")]
        private IWebElement _postCodeInput;

        [FindsBy(How = How.Name, Using = "country_id")]
        private IWebElement _countryDropdown;
        
        [FindsBy(How = How.Name, Using = "telephone")]
        private IWebElement _telephoneInput;

        [FindsBy(How = How.CssSelector, Using = "button.action.action-show-popup")]
        private IWebElement _newAddressButton;

        [FindsBy(How = How.ClassName, Using = "table-checkout-shipping-method")]
        private IList<IWebElement> _chechoutShippingMethod;

        private By _shippingMethod = By.CssSelector("td.col.col-method");

        private By _newAddress = By.CssSelector("button.action.action-show-popup");

        [FindsBy(How = How.CssSelector, Using = "button.button.action.continue.primary")]
        private IWebElement _nextPage;

        [FindsBy(How = How.CssSelector, Using = "button.action.primary.action-save-address")]
        private IWebElement _saveAddress;

        private string _texasRegion = "57";
        private string _USCountry = "US";

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }
            
        internal void InputAddress
            (AddressModel address)
        {
            //Verify if button.action.action-show-popup exists
            //if true click it 
            //Wait for new address to be clicable
            //Wait for list of shipping methods to be clickable

            WebDriverWait waitShipping = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waitShipping.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("table-checkout-shipping-method")));

            IEnumerable<IWebElement> _newAddrBtn = _driver.FindElements(_newAddress);
            
            if(_newAddrBtn.Count() == 0)
            {
                InputFirstName(address.FirstName);
                InputLastName(address.LastName);
                InputStreet(address.StreetAddress);
                InputCity(address.City);
                InputRegion(address.Region);
                InputPostCode(address.PostalCode);
                InputCountry(address.Country);
                InputTelephone(address.TelephoneNumeber);

                AddCheckoutMethod();
                _nextPage.Click();
            }
            else
            {
                WebDriverWait waitButton = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var openAddressButton = waitButton.Until(ExpectedConditions.ElementToBeClickable(_newAddress));
                openAddressButton.Click();
                
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
                wait.Until(ExpectedConditions.ElementExists(By.ClassName("modal-popup")));
                
                InputFirstName(address.FirstName);
                InputLastName(address.LastName);
                InputStreet(address.StreetAddress);
                InputCity(address.City);
                InputRegion(address.Region);
                InputPostCode(address.PostalCode);
                InputCountry(address.Country);
                InputTelephone(address.TelephoneNumeber);
                //Close addr page
                _saveAddress.Click();
                //wait for shipping methods
                AddCheckoutMethod();
                _nextPage.Click();
            }


            //else

        }
        public void InputFirstName(string firstName)
        {
            _firstNameInput.SendKeys(firstName);
        }
        public void InputLastName(string lastName)
        {
            _lastNameInput.SendKeys(lastName);
        }
        public void InputStreet(string email)
        {
            _streetInput.SendKeys(email);
        }
        public void InputCity(string email)
        {
            _cityInput.SendKeys(email);
        }

        public void InputRegion(string region)
        {
            SelectElement dropDown = new SelectElement(_regionDropdown);
            dropDown.SelectByValue(region);
        }

        public void InputPostCode(string email)
        {
            _postCodeInput.SendKeys(email);
        }

        public void InputCountry(string country)
        {
            SelectElement dropDown = new SelectElement(_countryDropdown);
            dropDown.SelectByValue(country);
        }
        public void InputTelephone(string telephoneNumber)
        {
            _telephoneInput.SendKeys(telephoneNumber);
        }

        internal void AddCheckoutMethod()
        {
            WebDriverWait waitShipping = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            waitShipping.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("table-checkout-shipping-method")));



            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            wait.Until((_) => _chechoutShippingMethod.Select(i => i.FindElements(_shippingMethod).Count() > 0));

            IEnumerable<IWebElement> checkoutMethods = _chechoutShippingMethod
                .Select(i => i.FindElement(_shippingMethod));

            checkoutMethods.First().Click();
        }
    }
}
