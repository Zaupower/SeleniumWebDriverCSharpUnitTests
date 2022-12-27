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

namespace sleeniumTest.Tests.Scenario2
{
    public class Scenario2Tests
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
        public void Test()
        {
            CreateAccountPage createAccountPage = _mainPage.ClickCreateAnAccountButton();

            _userModel.Email = "";
            createAccountPage.CreateAnAccount(_userModel);
            string emailInputError = createAccountPage.GetEmailAddressError();
            Assert.That(emailInputError.Equals("This is a required field."));
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
