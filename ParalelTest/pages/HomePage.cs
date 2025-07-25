using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ParalelTest.Base;
using ParalelTest.Pages.AccountPage;
using ParalelTest.Pages.ProductCategoryPage;
using ParalelTest.Utilities;
using System.Xml.Linq;

namespace ParalelTest.Pages

{
  public class HomePage : BasePage
  {
    private readonly By accountBtn = By.XPath("//span[@class='icon-account']");
    private readonly By loggedInHeader = By.XPath("//h1[contains(text(),'Tài khoản của bạn')]");
    private readonly By liKinhMat = By.XPath("//body/div[1]/header[1]/div[1]/div[2]/div[1]/nav[1]/ul[1]/li[4]");
    private readonly By subMenuKMNu = By.XPath("//body/div[1]/header[1]/div[1]/div[2]/div[1]/nav[1]/ul[1]/li[4]/ul[1]/li[2]");
    private readonly By cartBtn = By.XPath("//span[@id='site-cart-handle']");
    private readonly By viewCartBtn = By.XPath("//a[contains(text(),'Xem giỏ hàng')]");
    private readonly By errorMsg = By.XPath("//li[contains(text(),'Thông tin đăng nhập không hợp lệ.')]");



    public LogInPage GoToLogInPage()
    {
      Report.LogInfo("Navigating to Log In Page.");
      WaitUtility.WaitForElementToBeClickable(accountBtn, 10);
      Click(accountBtn);
      return new LogInPage();

    }

    public bool IsUserLoggedIn(string email)
    {
      try
      {
        var loginCompleted = WaitUtility.Wait(5).Until(d =>
        {
          var hasEmailElement = d.FindElements(loggedInHeader).Any(e => e.Displayed);
          var hasErrorElement = d.FindElements(errorMsg).Any(e => e.Displayed);
          return hasEmailElement || hasErrorElement;
        });
        Report.LogInfo("Login state check: " + loginCompleted);

        if (loginCompleted)
        {
          Thread.Sleep(500);

          var emailElement = driver.FindElements(loggedInHeader).FirstOrDefault(e => e.Displayed);
          if (emailElement != null)
          {
            Report.LogInfo($"Logged in successfully");
            return true;
          }

          var errorElement = driver.FindElements(errorMsg).FirstOrDefault(e => e.Displayed);
          if (errorElement != null)
          {
            Report.LogInfo($"Login failed. Error message: {errorElement.Text}");
            return false;
          }
        }
        Report.LogFail("Neither login success nor error message element found after timeout");
        return false;
      }
      catch (Exception ex)
      {
        Report.LogFail("Error checking login state" + ex);
        return false;
      }
    }
    public KinhMatNuPage GoToKinhMatNu()
    {
      try
      {
        Report.LogInfo("Navigating to Kính Mắt Nữ.");
        WaitUtility.WaitForElementToBeClickable(liKinhMat);
        ActionsUtility.MoveToElement(liKinhMat);
        Report.LogInfo("Click Kinh Mat Nu submenu");
        WaitUtility.WaitForElementToBeClickable(subMenuKMNu);
        Click(subMenuKMNu);
        return new KinhMatNuPage();
      }
      catch (NoSuchElementException)
      {
        Console.WriteLine("Navigation elements not found.");
        return null;
      }
    }
    public void ClickCartButton()
    {
      Report.LogInfo("Clicking on Cart button.");
      WaitUtility.WaitForElementToBeClickable(cartBtn);
      Click(cartBtn);
    }
    public CartPage GoToCartPage()
    {
      Report.LogInfo("Navigate to View Cart Page");
      ClickCartButton();
      WaitUtility.WaitForElementToBeClickable(viewCartBtn).Click();
      return new CartPage();
    }
  }
}
