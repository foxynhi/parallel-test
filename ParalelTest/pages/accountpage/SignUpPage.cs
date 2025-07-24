using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Utilities;

namespace ParalelTest.Pages.AccountPage
{
  public class SignUpPage : BasePage
  {
    private readonly By title = By.XPath("//h1[contains(text(),'Tạo tài khoản')]");
    private readonly By lastNameInp = By.XPath("//input[@id='last_name']");
    private readonly By firstNameInp = By.XPath("//input[@id='first_name']");
    private readonly By femaleRadBtn = By.XPath("//input[@id='radio1']");
    private readonly By maleRadBtn = By.XPath("//input[@id='radio2']");
    private readonly By DOBInp = By.XPath("//input[@id='birthday']");
    private readonly By emailInp = By.XPath("//input[@id='email']");
    private readonly By passwordInp = By.XPath("//input[@id='password']");
    private readonly By signUpBtn = By.XPath("//input[@type='submit']");
    private readonly By errorMessage = By.XPath("//div[@class='errors']//li");


    public bool VerifySignUpPage()
    {
      //Report.LogInfo("Verifying Sign Up page is displayed.");
      WaitUtility.WaitForElementToBeVisible(title);
      return Find(title).Displayed;
    }
    public bool SignUp(string lastName, string firstName, string email, string password, string sex = "male", string DOB = "01/01/2000")
    {
      SendKeys(lastNameInp, lastName);
      SendKeys(firstNameInp, firstName);
      if (sex.ToLower().Equals("male"))
      {
        JSUtility.ClickJS(maleRadBtn);
      }
      else
      {
        JSUtility.ClickJS(femaleRadBtn);
      }
      JSUtility.ScrollTo(DOBInp);
      SendKeys(DOBInp, DOB);
      SendKeys(emailInp, email);
      SendKeys(passwordInp, password);

      try
      {
        WaitUtility.WaitForElementToBeClickable(signUpBtn);
        Click(signUpBtn);
        WaitUtility.WaitForElementToBeVisible(errorMessage, 2);
        Report.LogFail($"Sign Up failed with error: {Find(errorMessage).Text}");
        return false;
      }
      catch (NoSuchElementException)
      {
        Report.LogPass("Sign Up successfully");
      }
      catch (WebDriverTimeoutException)
      {
        Report.LogPass("Sign Up successfully, no error message displayed");
      }
      return true;
    }
  }
}
