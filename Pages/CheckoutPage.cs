using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using sleeniumTest.Helper;
using sleeniumTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        private IWebElement _chechoutShippingMethod;

        [FindsBy(How = How.ClassName, Using = "actions-toolbar")]
        private IList<IWebElement> _actionToolBar;

        //MEXI AQUI
        private By _shippingMethod = By.ClassName("radio");

        private By _newAddress = By.CssSelector("button.action.action-show-popup");

        [FindsBy(How = How.CssSelector, Using = "button.button.action.continue.primary")]
        private IWebElement _nextPage;

        [FindsBy(How = How.CssSelector, Using = "button.action.primary.action-save-address")]
        private IWebElement _saveAddress;

        [FindsBy(How = How.CssSelector, Using = "button.action.primary.checkout")]
        private IWebElement _saveOrder;

        [FindsBy(How = How.ClassName, Using = "checkout-success")]
        private IList<IWebElement> _checkOutSuccess;

        [FindsBy(How = How.CssSelector, Using = "a.action.primary.continue")]
        private IWebElement _continueShoppingButton;

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        } 
        public void InputAddress
            (AddressModel address)
        {

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
            }
            else
            {
                WebDriverWait waitButton = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var openAddressButton = waitButton.Until(ExpectedConditions.ElementToBeClickable(_newAddress));
                
                openAddressButton.Click();
                
                //Wait for popup address to exist
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

                waitShipping.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("modal-popup")));
                waitShipping.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("table-checkout-shipping-method")));
                waitShipping.Until(ExpectedConditions.ElementToBeClickable(By.Id("checkoutSteps")));

            }


            //else

        }

        public void ClickNextPage()
        {
            _nextPage.Click();
        }
        public string GetOrderNumber()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("checkout-success")));

            IEnumerable<IWebElement> checkoutMethods = _checkOutSuccess
                .Select(i => i.FindElement(By.TagName("strong")));
            IWebElement orderId = checkoutMethods.First();

            return orderId.Text;
        }
        public void ClickSaveOrder()
        {
            WebDriverWait wait5 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait5.Until(ExpectedConditions.ElementIsVisible(By.ClassName("billing-address-details")));

            WebDriverWait wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var bCheckout = wait2.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.action.primary.checkout")));
            
            ScrollToElement(bCheckout);
            
            bCheckout.Click();
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

        public decimal AddShippingMethod()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            var shipsMethods = GetShippingMethods(wait);

            string firstShipMethodPrice = shipsMethods.First().FindElement(By.CssSelector(".col.col-price")).Text;

            Console.WriteLine("firstShipMethodPrice: " + firstShipMethodPrice);
            
            bool staleness = wait.Until(ExpectedConditions.StalenessOf(shipsMethods.First().FindElement(By.CssSelector(".col.col-method"))));
            if (staleness)
            {
                shipsMethods = GetShippingMethods(wait);
                firstShipMethodPrice = shipsMethods.First().FindElement(By.CssSelector(".col.col-price")).Text;
            }
            var selectShippimentButton = shipsMethods.First().FindElement(By.CssSelector(".col.col-method"));
            selectShippimentButton.Click();

            return Parser.CurrencyStringToDecimal(firstShipMethodPrice);
        }

        internal void ClickContinueShopping()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(_continueShoppingButton)).Click();

        }

        private IEnumerable<IWebElement> GetShippingMethods(WebDriverWait wait)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("table-checkout-shipping-method")));

            var shippingMethodHead = _driver.FindElement(By.ClassName("table-checkout-shipping-method"));

            ScrollToElement(shippingMethodHead);

            var shippingdBody = shippingMethodHead.FindElement(By.TagName("tbody"));

            return shippingdBody.FindElements(By.TagName("tr"));
        }
    }
}
