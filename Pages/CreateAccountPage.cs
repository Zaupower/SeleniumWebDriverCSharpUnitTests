using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace sleeniumTest.Pages
{
    public class CreateAccountPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "base")]
        private IWebElement _createAccountTitle;

        [FindsBy(How = How.Id, Using = "firstname")]
        private IWebElement _firstNameInput;

        [FindsBy(How = How.Id, Using = "lasttname")]
        private IWebElement _lastNameInput;

        [FindsBy(How = How.Id, Using = "email_address")]
        private IWebElement _emailInput;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _passwordInput;

        [FindsBy(How = How.Id, Using = "password-confirmation")]
        private IWebElement _passwordConfirmationInput;

        public CreateAccountPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetCreateAccountTitle()
        {
            return _createAccountTitle.Text;
        }

    }
}