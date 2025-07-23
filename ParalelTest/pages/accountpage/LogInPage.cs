using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Utilities;

namespace ParalelTest.Pages.AccountPage
{
  public class LogInPage : BasePage
  {
    private readonly By emailInp = By.Id("customer_email");
    private readonly By passwordInp = By.Id("customer_password");
    private readonly By logInBtn = By.XPath("//input[@value='Đăng nhập']");
    private readonly By SignUpBtn = By.XPath("//a[contains(text(),'Đăng ký')]");
    private readonly By addressInfo = By.XPath("//a[contains(text(),'Danh sách địa chỉ')]");


    public void LogIn(string email, string password)
    {
      WaitUtility.WaitForElementToBeVisible(emailInp);
      //ExtentReporting.LogInfo($"Entering {email} and {password} for login.");
      SendKeys(emailInp, email);
      SendKeys(passwordInp, password);
      WaitUtility.WaitForElementToBeClickable(logInBtn);
      //ExtentReporting.LogInfo("Clicking the Log In button.");
      Click(logInBtn);
    }

    public SignUpPage GoToSignUpPage()
    {
      //ExtentReporting.LogInfo("Navigating to Sign Up page.");
      WaitUtility.WaitForElementToBeClickable(SignUpBtn);
      Click(SignUpBtn);
      return new SignUpPage();
    }
  }
}
