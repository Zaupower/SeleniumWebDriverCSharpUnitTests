using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace sleeniumTest.Pages
{
    public class CreateAccountPage : BasePage
    {
        [FindsBy(How = How.ClassName, Using = "base")]
        private IWebElement _createAccountTitle;

        [FindsBy(How = How.Id, Using = "firstname")]
        private IWebElement _firstNameInput;

        [FindsBy(How = How.Id, Using = "lastname")]
        private IWebElement _lastNameInput;

        [FindsBy(How = How.Id, Using = "email_address")]
        private IWebElement _emailInput;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _passwordInput;

        [FindsBy(How = How.Id, Using = "password-confirmation")]
        private IWebElement _passwordConfirmationInput;

        [FindsBy(How = How.ClassName, Using = "primary")]
        private IWebElement _submitNewUserButton;

        public CreateAccountPage(IWebDriver driver) : base(driver)
        {
        }


        public CostumerPage CreateAnAccount(
            string firstName, string lastName, 
            string email, string password, 
            string passwordConfirmation)
        {
            InputFirstName(firstName);
            InputLastName(lastName);
            InputEmail(email);
            InputPassword(password);
            InputPasswordConfirmation(passwordConfirmation);
            
            ClickSubmitNewUserButton();

            return new CostumerPage(_driver);
        }


        public string GetCreateAccountTitle()
        {
            return _createAccountTitle.Text;
        }
        public void InputFirstName(string firstName)
        {
            _firstNameInput.SendKeys(firstName);
        }
        public void InputLastName(string lastName)
        {
            _lastNameInput.SendKeys(lastName);
        }
        public void InputEmail(string email)
        {
            _emailInput.SendKeys(email);
        }
        public void InputPassword(string password)
        {
            _passwordInput.SendKeys(password);
        }
        public void InputPasswordConfirmation(string passwordConfirmation)
        {
            _passwordConfirmationInput.SendKeys(passwordConfirmation);
        }

        public void ClickSubmitNewUserButton()
        {
            _submitNewUserButton.Click();
        }

    }
}