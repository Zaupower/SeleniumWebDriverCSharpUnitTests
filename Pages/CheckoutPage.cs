using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class CheckoutPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "region_id")]
        private IWebElement _regionDropdown;

        private string _texas = "57";


        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }
               
        public CustomerLoginPage ClickSignInButton()
        {
            SelectElement dropDown = new SelectElement(_driver.FindElement(By.Name("region_id")));
            dropDown.SelectByValue(_texas);
            return new CustomerLoginPage(_driver);
        }
        //SelectElement dropDown = new SelectElement(_driver.FindElement(By.Id("select-demo")));
        //dropDown.SelectByValue(dayOfTheWeek);
        //string actualText = driver.FindElement(By.CssSelector(".selected-value.text-size-14")).Text;


        //Assert.True(actualText.Contains(dayOfTheWeek), $"The expected day of the week {dayOfTheWeek} was not selected. The actual text was: {actualText}.");
    }
}
