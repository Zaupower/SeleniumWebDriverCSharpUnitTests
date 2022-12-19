using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace sleeniumTest.Pages
{
    public class CreateAccountPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "base")]
        private IWebElement _createAccountTitle;
        public CreateAccountPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetCreateAccountTitle()
        {
            return _createAccountTitle.Text;
        }

    }
}