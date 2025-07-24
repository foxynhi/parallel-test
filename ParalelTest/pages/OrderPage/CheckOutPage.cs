using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Utilities;

namespace ParalelTest.Pages.OrderPage
{
  public class CheckOutPage : BasePage
  {
    private readonly By phoneInp = By.XPath("//input[@id='phone']");
    private readonly By addressInp = By.XPath("//input[@id='address']");
    private readonly By addressDropDown = By.XPath("//div[@id='react-select-5-listbox']");
    private readonly By cityInp = By.XPath("//input[@id='fulladdress']");
    private readonly By shippingMethodCOD = By.XPath("//div[contains(text(),'Thanh toán khi giao hàng (COD)')]");
    private readonly By submitBtn = By.XPath("(//button[@id='place_order'])[1]");


    public bool IsOnCheckOutPage()
    {
      return WaitUtility.WaitForElementToBeVisible(By.XPath("//div[contains(text(),'Thông tin giao hàng')]")).Displayed;
    }
    public SuccessOrderPage CheckOutCart(string phoneNumber, string address)
    {
      try
      {
        WaitUtility.WaitForElementToBeVisible(phoneInp);
        ActionsUtility.ScrollTo(phoneInp);
        Clear(phoneInp);
        SendKeys(phoneInp, phoneNumber);
        ActionsUtility.ScrollTo(addressInp);
        Clear(addressInp);
        SendKeys(addressInp, address);
        WaitUtility.WaitForElementToBeVisible(addressDropDown);
        Find(addressDropDown).FindElement(By.XPath("./div[1]")).Click();

        ActionsUtility.ScrollTo(shippingMethodCOD);
        Click(shippingMethodCOD);
        ActionsUtility.ScrollTo(submitBtn);
        Click(submitBtn);
        return new SuccessOrderPage();
      }
      catch (Exception ex)
      {
        Report.LogFail($"Error during checkout: {ex.Message}");
        return null;
      }
    }
  }
}
