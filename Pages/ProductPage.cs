﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class ProductPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "product-item-info")]
        private IList<IWebElement> _productInfoElementCollection;

        [FindsBy(How = How.ClassName, Using = "messages")]
        private IWebElement _alertMessage;

        private By _productInfoNames = By.ClassName("product-item-link");

        private By _toCartButton = By.ClassName("tocart");

        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        public void ScrollToProduct(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(element);
            actions.Perform();
        }

        
        public IWebElement GetProductInfo(string productName)
        {
            IWebElement element = _productInfoElementCollection.Where(i => i.FindElement(_productInfoNames).Text == productName).First();
            return element;
        }

        public void AddProductToCart(IWebElement targetProduct)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(targetProduct);
            actions.Perform();

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

            productAddToCartButton.Click();

            //wait untill cart proccess finish
            GetAlertMessage();
        }

        public string GetAlertMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(4));

            wait.Until((_) => _alertMessage.Text.StartsWith("You added "));

            IWebElement alert = _alertMessage;

            return alert.Text;
        }

        public void ScrollToProducts()
        {
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(_productInfoElementCollection.First());
            actions.Perform();
        }
        public void AddFirstProductToCart()
        {
            IWebElement targetProduct = _productInfoElementCollection.First();

            Actions actions = new Actions(_driver);
            actions.MoveToElement(targetProduct);
            actions.Perform();

            IWebElement productAddToCartButton = targetProduct.FindElement(_toCartButton);

            productAddToCartButton.Click();
        }
        public IEnumerable<string> GetProductInfoNames()
        {
            IEnumerable<IWebElement> productInfoNames = _productInfoElementCollection
                .Select(i => i.FindElement(_productInfoNames));

            IEnumerable<string> actual = productInfoNames.Select(i => i.Text);
            return actual;
        }

    }
}
