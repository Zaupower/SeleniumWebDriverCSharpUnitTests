using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Pages
{
    public class CostumerPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "message-success")]
        private IWebElement _createAccountSuccessMessage;
        
        public CostumerPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetCreateAccountSuccessMessage()
        {
            return _createAccountSuccessMessage.Text;
        }

    }
}
