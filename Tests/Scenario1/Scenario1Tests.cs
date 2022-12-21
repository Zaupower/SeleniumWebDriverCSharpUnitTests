using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using sleeniumTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sleeniumTest.Models;
using sleeniumTest.Helper;

namespace sleeniumTest.Tests.Scenario1
{
    public class Scenario1Tests
    {
        private static IWebDriver _driver;
        private static MainPage _mainPage;
        private CreateUserModel _userModel;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("headless");
            //_driver = new ChromeDriver(chromeOptions);
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);//Used to retry get elements in cases where the page/objet
                                                                               //need time to load, it trys every 50ms until time defined
            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men.html");
            _mainPage = new MainPage(_driver);

            //SetUp user

        }
        [OneTimeSetUp]

        public void OneTimeSetup()
        {
            GenerateRandomUser generateRandomUser = new GenerateRandomUser();
            _userModel = generateRandomUser.getRandomUser();
        }

        [Test, Order(1)]
        public void test1_NewUser()
        {
            
            CreateAccountPage createAccountPage = _mainPage.ClickCreateAnAccountButton();

            createAccountPage.CreateAnAccount(_userModel);

            ProductPage productPage = _mainPage.OpenWatchesNavigationButton();
            IWebElement watch = productPage.GetProductInfo("Dash Digital Watch");
            IWebElement watch2 = productPage.GetProductInfo("Clamber Watch");
            productPage.AddProductToCart(watch);
            productPage.AddProductToCart(watch2);
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
            string shippingPrice = checkoutPage.AddShippingMethod();
            
            checkoutPage.ClickNextPage();
            checkoutPage.ClickSaveOrder();
            string orderNumber = checkoutPage.GetOrderNumber();

            checkoutPage.ClickContinueShopping();
            CostumerPage costumerPage = _mainPage.ClickMyAccountButton();
            costumerPage.ClickMyOrders();
            //List<string> ordersIds = 
            costumerPage.GetMyOrdersIds();
            costumerPage.ClickOrder(orderNumber);
            //Console.WriteLine("Order Sizes: "+ordersIds.Count());
            //Console.WriteLine(ordersIds.ToString());

            Assert.AreEqual("You added Dash Digital Watch to your shopping cart.", actual);
            
        }

        [Test, Order(2)]
        public void test1_AlreadyCreatedUser()
        {
            CustomerLoginPage customerLoginPage = _mainPage.ClickSignInButton();
            customerLoginPage.LogIn(_userModel.Email, _userModel.Password);
            ProductPage productPage = _mainPage.OpenWatchesNavigationButton();
            IWebElement watch = productPage.GetProductInfo("Dash Digital Watch");
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
            checkoutPage.AddShippingMethod();
            checkoutPage.ClickNextPage();
            checkoutPage.ClickSaveOrder();
            Assert.AreEqual("You added Dash Digital Watch to your shopping cart.", actual);
        }

        [TearDown]
        public void TearDown()
        {
            //_driver.Quit();
        }
    }
}
