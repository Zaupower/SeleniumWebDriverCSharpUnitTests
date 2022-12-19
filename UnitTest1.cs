using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V106.Runtime;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using sleeniumTest.Pages;
using System.Collections.Generic;

namespace sleeniumTest
{
    public class Tests
    {

        private static IWebDriver _driver;
        private static MainPage _mainPage;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            _driver = new ChromeDriver(chromeOptions);
            //_driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);//Used to retry get elements in cases where the page/objet
                                                                               //need time to load, it trys every 50ms until time defined
            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men.html");
            _mainPage = new MainPage(_driver);
        }

        [Test] 
        public void LogoutUSer_CheckSignInButtonText_isSignIn()
        {         
            string actual = _mainPage.GetSignInButtonText();
            Assert.AreEqual("Sign In", actual);
        }

        [Test]
        public void LogoutUSer_CheckCreateAccountTitleText_IsCorrect()
        {
            CreateAccountPage createAccountPage = _mainPage.ClickCreateAnAccountButton();
            string actual = createAccountPage.GetCreateAccountTitle();

            Assert.AreEqual("Create New Customer Account", actual);
        }

        [Test]
        public void MainPage_LoginByValidUser_WelcomeMessageIsCorrect()
        {
            var costumerLoginPage =_mainPage.ClickSignInButton();
            string email = "marcelomascfb22@gmail.com";
            string password = "Qwerty1234";
            costumerLoginPage.LogIn(email, password);

            string actual = _mainPage.GetWelcomeMessage();

            Assert.AreEqual("Welcome, Marcelo Carvalho!", actual);
        }

        [Test]
        public void ValidUser_OpenGear_ProductListIsCorrect()
        {
            var costumerLoginPage = _mainPage.ClickSignInButton();

            string email = "marcelomascfb22@gmail.com";
            string password = "Qwerty1234";
            costumerLoginPage.LogIn(email, password);

            var productPage = _mainPage.OpenGearNavigationButton();

            productPage.ScrollToProducts();

            IEnumerable<string> actual = productPage.GetProductInfoNames();
            IEnumerable<string> expected = new[]
            {
                "Fusion Backpack",
                "Push It Messenger Bag",
                "Affirm Water Bottle",
                "Sprite Yoga Companion Kit"
            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void LogoutUser_AddProductToCart_AlertIsCorrect()
        {
            ProductPage productPage = _mainPage.OpenGearNavigationButton();

            productPage.AddFirstProductToCart();

            var actual = productPage.GetAlertMessage();

            Assert.AreEqual("You added Fusion Backpack to your shopping cart.", actual);
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}