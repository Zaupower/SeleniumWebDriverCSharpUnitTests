using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using sleeniumTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sleeniumTest.Models;

namespace sleeniumTest.Tests.Scenario1
{
    public class Scenario1Tests
    {
        private static IWebDriver _driver;
        private static MainPage _mainPage;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("headless");
            //_driver = new ChromeDriver(chromeOptions);
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);//Used to retry get elements in cases where the page/objet
                                                                               //need time to load, it trys every 50ms until time defined
            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men.html");
            _mainPage = new MainPage(_driver);
        }

        [Test]
        public void test1()
        {
            //Open main page
                CustomerLoginPage customerLoginPage = _mainPage.ClickSignInButton();
            //Login
            customerLoginPage.LogIn("moderya7@gmail.com", "test_password1");
            //click Gear.Wathces
            ProductPage productPage = _mainPage.OpenWatchesNavigationButton();
            IWebElement watch = productPage.GetProductInfo("Dash Digital Watch");
            //Add to cart 'Dash Digital Watch'
            productPage.AddProductToCart(watch);
            var actual = productPage.GetAlertMessage();
            CheckoutPage checkoutPage = _mainPage.ProccedToCheckout();
            Random rn = new Random();
            
            AddressModel address = new AddressModel
            {
                FirstName = "Marcelo",
                LastName = "Carvalhgo",
                StreetAddress = "Rua de Real",
                City = "San Antonio",
                Region = "57",
                PostalCode = "12345-6789",
                Country = "US",
                TelephoneNumeber = rn.Next(90000, 999999).ToString(),
        };
            checkoutPage.InputAddress(address);
            checkoutPage.AddCheckoutMethod();
            Assert.AreEqual("You added Dash Digital Watch to your shopping cart.", actual);
            //Go to checkout
            //Fill address 
            //Place Order
            //Get order number
            //Open 'My Account' page
            //Open 'My Orders'
            //Go to my orders, find the placed order and click View Order
            //Check all order details are correct
        }

        [TearDown]
        public void TearDown()
        {
            //_driver.Quit();
        }
    }
}
