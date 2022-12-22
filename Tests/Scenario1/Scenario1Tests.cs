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
using System.Globalization;
using NUnit.Framework.Internal;
using Newtonsoft.Json;

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
            _userModel = GenerateRandomUser.GetNewUser();
        }

        [Test]
        public void test1_NewUser()
        {
            Product dashDigitalWatch = new Product{ ProductName = "Dash Digital Watch"};
            Product clamberlWatch = new Product{ProductName = "Clamber Watch"};
            List<Product> productList = new List<Product>();
            productList.Add(dashDigitalWatch);
            productList.Add(clamberlWatch);

            List<decimal> expectedTotalPrice = new List<decimal>();

            CreateAccountPage createAccountPage = _mainPage.ClickCreateAnAccountButton();

            createAccountPage.CreateAnAccount(_userModel);

            ProductPage productPage = _mainPage.OpenWatchesNavigationButton();

            foreach(var product in productList)
            {
                string productPrice = productPage.AddProductToCart(product.ProductName);
                expectedTotalPrice.Add(Parser.CurrencyStringToDecimal(productPrice));

            }
            CheckoutPage checkoutPage = _mainPage.ProccedToCheckout();
            
            checkoutPage.InputAddress(GenerateAddress.GetAddress());

            string shippingPrice = checkoutPage.AddShippingMethod();
            decimal shipping = Parser.CurrencyStringToDecimal(shippingPrice);

            checkoutPage.ClickNextPage();
            checkoutPage.ClickSaveOrder();

            string orderNumber = checkoutPage.GetOrderNumber();

            checkoutPage.ClickContinueShopping();
            CostumerPage costumerPage = _mainPage.ClickMyAccountButton();
            costumerPage.ClickMyOrders();
            OrderDetailsPage orderPage =  costumerPage.ClickOrder(orderNumber);


            OrderDetails expectedOrder = new OrderDetails
            {
                 Products = productList,
                 GrandTotal = expectedTotalPrice.Sum() + shipping,
                 ShippingAndHandling = shipping,
                 SubTotal = expectedTotalPrice.Sum(),

            };
            OrderDetails actualOrder = orderPage.GetOrderDetails();

            var expectedJson = JsonConvert.SerializeObject(expectedOrder);
            var actualJson = JsonConvert.SerializeObject(actualOrder);
            Assert.AreEqual(expectedJson, actualJson);
        }

        

        [Test, Order(2)]
        public void test1_AlreadyCreatedUser()
        {
            Product dashDigitalWatch = new Product { ProductName = "Dash Digital Watch" };
            Product clamberlWatch = new Product { ProductName = "Clamber Watch" };
            List<Product> productList = new List<Product>();
            productList.Add(dashDigitalWatch);
            productList.Add(clamberlWatch);

            List<decimal> expectedTotalPrice = new List<decimal>();

            var costumerLoginPage = _mainPage.ClickSignInButton();

            CreateUserModel registeredUser = GenerateRandomUser.GetRegisteredUser();

            costumerLoginPage.LogIn(registeredUser.Email, registeredUser.Password);

            ProductPage productPage = _mainPage.OpenWatchesNavigationButton();

            foreach (var product in productList)
            {
                string productPrice = productPage.AddProductToCart(product.ProductName);
                expectedTotalPrice.Add(Parser.CurrencyStringToDecimal(productPrice));

            }

            CheckoutPage checkoutPage = _mainPage.ProccedToCheckout();

            checkoutPage.InputAddress(GenerateAddress.GetAddress());

            string shippingPrice = checkoutPage.AddShippingMethod();
            decimal shipping = Parser.CurrencyStringToDecimal(shippingPrice);

            checkoutPage.ClickNextPage();
            checkoutPage.ClickSaveOrder();

            string orderNumber = checkoutPage.GetOrderNumber();

            checkoutPage.ClickContinueShopping();
            CostumerPage costumerPage = _mainPage.ClickMyAccountButton();
            costumerPage.ClickMyOrders();
            OrderDetailsPage orderPage = costumerPage.ClickOrder(orderNumber);


            OrderDetails expectedOrder = new OrderDetails
            {
                Products = productList,
                GrandTotal = expectedTotalPrice.Sum() + shipping,
                ShippingAndHandling = shipping,
                SubTotal = expectedTotalPrice.Sum(),

            };
            OrderDetails actualOrder = orderPage.GetOrderDetails();

            var expectedJson = JsonConvert.SerializeObject(expectedOrder);
            var actualJson = JsonConvert.SerializeObject(actualOrder);

            Assert.That(expectedJson, Is.EqualTo(actualJson));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
