using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using sleeniumTest.Helper;
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

        public void GetOrderDetails()
        {
            //Wait until table is visible 
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("my-orders-table")));

            //Get products in table
            var products = _chechoutShippingMethod.FindElements(By.TagName("tbody"));
            Console.WriteLine("Number of products: "+products.Count());
            
            //Products details
            foreach(var product in products)
            {
                string productName = product.FindElement(By.CssSelector(".col.name")).Text;
                Console.WriteLine("product name: " + productName);
            }


            //Footer detail
            //var subtotal = _chechoutShippingMethod.FindElement(By.ClassName("subtotal"));
            //var sub = subtotal.FindElement(By.ClassName("amount")).Text;
            var subtotalFooter = _chechoutShippingMethod.FindElement(By.CssSelector("tfoot"));
            string sub = subtotalFooter.FindElement(By.ClassName("subtotal")).FindElement(By.ClassName("price")).Text;
            var ship = subtotalFooter.FindElement(By.ClassName("shipping")).FindElement(By.ClassName("price"));
            var grand = subtotalFooter.FindElement(By.ClassName("grand_total")).FindElement(By.ClassName("price"));

            //NumberFormatInfo MyNFI = new NumberFormatInfo();
            //MyNFI.NegativeSign = "-";
            //MyNFI.CurrencyDecimalSeparator = ".";
            //MyNFI.CurrencyGroupSeparator = ",";
            //MyNFI.CurrencySymbol = "$";

            //decimal d = decimal.Parse("$45.00", NumberStyles.Currency, MyNFI);

            decimal d = Parser.CurrencyStringToDecimal(sub);
            Console.WriteLine("subtotal: " + d +  ",shipping: " + ship.Text + " grand_total: " + grand.Text);
            //Console.WriteLine("subtotal: "+ /*sub + */ " ,,shipping: "+ shipping+ " grand_total: "+ grand_total);
        }

    }
}
