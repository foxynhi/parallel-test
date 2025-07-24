using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Pages.AccountPage;
using ParalelTest.Pages.ProductCategoryPage;
using ParalelTest.Utilities;

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
      //Report.LogInfo("Navigating to Log In Page.");
      WaitUtility.WaitForElementToBeClickable(accountBtn, 10);
      Click(accountBtn);
      return new LogInPage();

    }

    public bool IsUserLoggedIn(string email)
    {
      try
      {
        WaitUtility.WaitForElementToBeVisible(loggedInEmail, 10);
        //Report.LogInfo($"Checking if user is logged in with email: '{email}'");
        return Find(loggedInEmail).Text.Equals(email, StringComparison.OrdinalIgnoreCase);
      }
      catch (NoSuchElementException)
      {
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
        //ActionsUtility.MoveToElement(subMenuKMNu);
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
