using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V106.Runtime;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace sleeniumTest
{
    public class Tests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);//Used to retry get elements in cases where the page/objet
                                                                               //need time to load, it trys every 50ms until time defined
            _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men.html");
        }

        [Test] 
        public void LogoutUSer_CheckSignInButtonText_isSignIn()
        {            
            IWebElement signInButton = _driver.FindElement(By.ClassName("authorization-link"));
            string actual = signInButton.Text;
            //_driver.Close();
            Assert.AreEqual("Sign In", actual);
        }

        [Test]
        public void MainPage_LoginByValidUser_WelcomeMessageIsCorrect()
        {
            IWebElement signInButton = _driver.FindElement(By.ClassName("authorization-link"));
            signInButton.Click();

            //_driver.Close();
            IWebElement emailInput = _driver.FindElement(By.Name("login[username]"));
            IWebElement passwordInput = _driver.FindElement(By.Name("login[password]"));
            emailInput.SendKeys("marcelomascfb22@gmail.com");
            passwordInput.SendKeys("Qwerty1234");

            signInButton = _driver.FindElement(By.Id("send2"));
            signInButton.Click();

            //ExplicitWaiter
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.ClassName("logged-in"), "Welcome, Marcelo Carvalho!"));
            
            IWebElement welcomeMessage = _driver.FindElement(By.ClassName("logged-in"));
             

            string actual = welcomeMessage.Text;

            Assert.AreEqual("Welcome, Marcelo Carvalho!", actual);
        }

        [Test]
        public void ValidUser_OpenGear_ProductListIsCorrect()
        {
            IWebElement signInButton = _driver.FindElement(By.ClassName("authorization-link"));
            signInButton.Click();

            IWebElement emailInput = _driver.FindElement(By.Name("login[username]"));
            IWebElement passwordInput = _driver.FindElement(By.Name("login[password]"));
            emailInput.SendKeys("marcelomascfb22@gmail.com");
            passwordInput.SendKeys("Qwerty1234");

            signInButton = _driver.FindElement(By.Id("send2"));
            signInButton.Click();

            IWebElement gearNavigationButton = _driver.FindElement(By.Id("ui-id-6"));
            gearNavigationButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(3));

            IEnumerable<IWebElement> productInfoElements = _driver.FindElements(By.ClassName("product-item-info"));
            IEnumerable<IWebElement> productInfoNames = productInfoElements.Select(i=> i.FindElement(By.ClassName("product-item-link")));

            Actions action = new Actions(_driver);
            action.MoveToElement(productInfoElements.First());
            action.Perform();
            IEnumerable<string>actual = productInfoNames.Select(i => i.Text);
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
            IWebElement gearNavigationButton = _driver.FindElement(By.Id("ui-id-6"));
            gearNavigationButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(3));

            IEnumerable<IWebElement> productInfoElements = _driver.FindElements(By.ClassName("product-item-info"));
            
            IWebElement tagertProduct = productInfoElements.First();

            Actions action = new Actions(_driver);
            action.MoveToElement(tagertProduct);
            action.Perform();

            IWebElement productAddToCartButton = tagertProduct.FindElement(By.ClassName("tocart"));

            productAddToCartButton.Click();
            //ExplicitWaiter
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.ClassName("message-success")));

            IWebElement alert = _driver.FindElement(By.ClassName("message-success"));

            Assert.AreEqual("You added Fusion Backpack to your shopping cart.", alert.Text);

            //Actions action = new Actions(_driver);
            //action.MoveToElement(productInfoElements.First());
        }
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}