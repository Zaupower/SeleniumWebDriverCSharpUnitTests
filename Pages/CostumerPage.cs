using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
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
    public class CostumerPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "message-success")]
        private IWebElement _createAccountSuccessMessage;

        [FindsBy(How = How.LinkText, Using = "My Orders")]
        private IWebElement _myOrdersButton;

        [FindsBy(How = How.Id, Using = "my-orders-table")]
        private IWebElement _myOrdersTable;

        public CostumerPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetCreateAccountSuccessMessage()
        {
            return _createAccountSuccessMessage.Text;
        }

        public void ClickMyOrders()
        {
            _myOrdersButton.Click();
        }

        public IEnumerable<string> GetMyOrdersIds()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("my-orders-table")));

            List<IWebElement> orders = _myOrdersTable.FindElements(By.TagName("tr")).ToList();
            Console.WriteLine("Orders count: "+orders.Count());
            
            //Remove first item, it is description of table
            orders.RemoveAt(0);

            IEnumerable<string> ordersIds =
                orders.Select(i => i.FindElement(By.CssSelector(".col.id")).Text);
            return ordersIds;
        }

        public OrderDetailsPage ClickOrder(string orderId)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("my-orders-table")));

            List<IWebElement> orders = _myOrdersTable.FindElements(By.TagName("tr")).ToList();
            
            //Remove first item, it is description of table
            orders.RemoveAt(0);

            var clickOrder = orders.Where(i => i.FindElement(By.CssSelector(".col.id")).Text == orderId)
                .Select(i => i.FindElement(By.LinkText("View Order")));

            clickOrder.First().Click();

            return new OrderDetailsPage(_driver);
            
        }

    }
}
