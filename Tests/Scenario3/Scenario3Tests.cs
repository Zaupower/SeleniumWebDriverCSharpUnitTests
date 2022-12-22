using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using sleeniumTest.Helper;
using sleeniumTest.Models;
using sleeniumTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace sleeniumTest.Tests.Scenario3
{
    public class Scenario3Tests
    {
        private static IWebDriver _driver;
        private static MainPage _mainPage;
        private CreateUserModel _userModel;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            _driver = new ChromeDriver(chromeOptions);
            //_driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);//Used to retry get elements in cases where the page/objet
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
        public void Test()
        {
            var costumerLoginPage = _mainPage.ClickSignInButton();
            CreateUserModel registeredUser = GenerateRandomUser.GetRegisteredUser();
            costumerLoginPage.LogIn(registeredUser.Email, registeredUser.Password);
            ProductPage productPage = _mainPage.OpenGearNavigationButton();
            productPage.ClickSubcategoryBags();
            productPage.AddProductsToCartByNumber(2);
            SingleProductPage singleProduct = productPage.ClickOnProductByIndex(2);
            singleProduct.ClickAddToCart();

            int cartCounter = _mainPage.GetCartCounter();
            Console.WriteLine("Cart Counter: "+cartCounter);
            Assert.That(cartCounter, Is.EqualTo(3));
        }
        [TearDown]
        public void TearDown()
        {
           _driver.Quit();
        }
    }
}