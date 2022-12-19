using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using sleeniumTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Tests.Tests
{
    public class CreateUserTests
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
        public void LogoutUser_CheckCreateAccountTitleText_IsCorrect()
        {
            CreateAccountPage createAccountPage = _mainPage.ClickCreateAnAccountButton();
            string actual = createAccountPage.GetCreateAccountTitle();

            Assert.AreEqual("Create New Customer Account", actual);
        }

        [Test]
        public void LogoutUser_CreateValidAccount_CreateAccountSuccessMessageIsCorrect()
        {
            CreateAccountPage createAccountPage = _mainPage.ClickCreateAnAccountButton();
            string firstName = "fisrtName_test";
            string lastName = "lastName_test";
            string email = "email_test3@gmail.com";
            string password = "Pasword_1";
            string passwordConfirmation = password;
            CostumerPage costumerPage = createAccountPage.CreateAnAccount(firstName,lastName,email, password, passwordConfirmation);

            string actual = costumerPage.GetCreateAccountSuccessMessage();

            Assert.AreEqual("Thank you for registering with Fake Online Clothing Store.", actual);
        }
    }
}
