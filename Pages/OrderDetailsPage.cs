using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using sleeniumTest.Helper;
using sleeniumTest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class OrderDetailsPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "my-orders-table")]
        private IWebElement _chechoutShippingMethod;
        
        public OrderDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        public OrderDetails GetOrderDetails()
        {
            OrderDetails order = new OrderDetails();

            //Wait until table is visible 
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("my-orders-table")));

            //Get products in table
            var products = _chechoutShippingMethod.FindElements(By.TagName("tbody"));
            Console.WriteLine("Number of products: "+products.Count());

            List<Product> productList = new List<Product>();

            //Products details
            foreach(var product in products)
            {
                Product newProduct = new Product();

                string productName = product.FindElement(By.CssSelector(".col.name")).Text;
                newProduct.ProductName = productName;
                productList.Add(newProduct);
            }
            order.Products = productList;

            var subtotalFooter = _chechoutShippingMethod.FindElement(By.CssSelector("tfoot"));
            string subtotal = subtotalFooter.FindElement(By.ClassName("subtotal")).FindElement(By.ClassName("price")).Text;
            string shippingCost = subtotalFooter.FindElement(By.ClassName("shipping")).FindElement(By.ClassName("price")).Text;
            string grandTotal = subtotalFooter.FindElement(By.ClassName("grand_total")).FindElement(By.ClassName("price")).Text;

            decimal dsutotal = Parser.CurrencyStringToDecimal(subtotal);
            decimal dshippingCost = Parser.CurrencyStringToDecimal(shippingCost);
            decimal dgrandTotal = Parser.CurrencyStringToDecimal(grandTotal);

            order.SubTotal = dsutotal;
            order.ShippingAndHandling = dshippingCost;
            order.GrandTotal = dgrandTotal;

            return order;

        }

    }
}
