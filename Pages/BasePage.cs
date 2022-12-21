using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sleeniumTest.Pages
{
    public class BasePage
    {
        [FindsBy(How = How.Id, Using = "ui-id-6")]
        private IWebElement _gearCategoryButton;
        
        [FindsBy(How = How.Id, Using = "ui-id-27")]
        private IWebElement _watchesCategoryButton;

        protected readonly IWebDriver _driver;
        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public ProductPage OpenGearNavigationButton()
        {
            _gearCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Gear"));

            return new ProductPage(_driver);
        }

        public ProductPage OpenWatchesNavigationButton()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(_gearCategoryButton);
            actions.Perform();

            WebDriverWait waitForButton = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            _watchesCategoryButton = waitForButton.Until(ExpectedConditions.ElementToBeClickable(_watchesCategoryButton));
            _watchesCategoryButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((driver) => driver.Title.StartsWith("Watches"));

            return new ProductPage(_driver);
        }

        public void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(element);
            actions.Perform();
        }
    }
}
