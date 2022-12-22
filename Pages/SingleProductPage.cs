using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class SingleProductPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "product-addtocart-button")]
        private IWebElement _addSingleProductToCart;
        public SingleProductPage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickAddToCart()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.ElementExists(By.Id("product-addtocart-button")));
            _addSingleProductToCart.Click();
            GetAlertMessage();
            
        }
    }
}
