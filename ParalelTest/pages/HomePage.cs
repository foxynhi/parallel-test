using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Pages.AccountPage;
using ParalelTest.Utilities;
using System;

namespace ParalelTest.Pages

{
  public class HomePage : BasePage
  {
    private readonly By accountBtn = By.XPath("//span[@class='icon-account']");
    private readonly By loggedInEmail = By.XPath("//p[@class='email ']");
    private readonly By liKinhMat = By.XPath("//body/div[1]/header[1]/div[1]/div[2]/div[1]/nav[1]/ul[1]/li[4]");
    private readonly By subMenuKMNu = By.XPath("//body/div[1]/header[1]/div[1]/div[2]/div[1]/nav[1]/ul[1]/li[4]/ul[1]/li[2]");
    private readonly By cartBtn = By.XPath("//span[@id='site-cart-handle']");
    private readonly By viewCartBtn = By.XPath("//a[contains(text(),'Xem giỏ hàng')]");



    public LogInPage GoToLogInPage()
    {
      //ExtentReporting.LogInfo("Navigating to Log In Page.");
      WaitUtility.WaitForElementToBeClickable(accountBtn, 10);
      Click(accountBtn);
      return new LogInPage();

    }

    public bool IsUserLoggedIn(string email)
    {
      try
      {
        WaitUtility.WaitForElementToBeVisible(loggedInEmail, 10);
        //ExtentReporting.LogInfo($"Checking if user is logged in with email: '{email}'");
        return Find(loggedInEmail).Text.Equals(email, StringComparison.OrdinalIgnoreCase);
      }
      catch (NoSuchElementException)
      {
        return false;
      }
    }
  }
}
