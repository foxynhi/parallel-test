using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Utilities;

namespace ParalelTest.Pages.AccountPage
{
  public class AddressPage : BasePage
  {
    private readonly By title = By.XPath("//h1[contains(text(),'Thông tin địa chỉ')]");
    private readonly By addAddressBtn = By.XPath("//a[contains(text(),'Nhập địa chỉ mới')]");
    private readonly By lastNameInp = By.XPath("//input[@id='address_last_name_new']");
    private readonly By firstNameInp = By.XPath("//input[@id='address_first_name_new']");
    private readonly By address1Inp = By.XPath("//input[@id='address_address1_new']");
    private readonly By coutrySelect = By.XPath("//select[@id='address_country_new']");
    private readonly By phoneInp = By.XPath("//input[@id='address_phone_new']");
    private readonly By submitBtn = By.XPath("//input[@value='Thêm mới']");

    private By ViewFullName(string address) => By.XPath("//div[contains(@class,'address_table')][.//p[contains(text(), '" + address + "')]]//strong");
    private By ViewAddress(string address) => By.XPath("//div[contains(@class,'address_table')][.//p[contains(text(), '" + address + "')]]//div[@class='view_address']//div[8]/p[1]");
    private By ViewPhoneNumber(string address) => By.XPath("//div[contains(@class,'address_table')][.//p[contains(text(), '" + address + "')]]//div[@class='view_address']//div[11]/p[1]");

    public bool IsOnAddressPage()
    {
      Report.LogInfo("Checking if on Address page.");
      return WaitUtility.WaitForElementToBeVisible(title).Displayed;
    }
    public bool AddAdress(string lastName, string firstName, string address, string phoneNumber, string country = "Vietnam")
    {
      Report.LogInfo("Adding new address to user profile.");
      try
      {
        WaitUtility.WaitForElementToBeClickable(addAddressBtn);
        ActionsUtility.ScrollTo(addAddressBtn);
        Click(addAddressBtn);

        WaitUtility.WaitForElementToBeVisible(lastNameInp);
        ActionsUtility.ScrollTo(lastNameInp);
        Clear(lastNameInp);
        SendKeys(lastNameInp, lastName);
        Clear(firstNameInp);
        SendKeys(firstNameInp, firstName);
        Clear(address1Inp);
        SendKeys(address1Inp, address);
        ActionsUtility.ScrollTo(coutrySelect);
        SelectUtility.SelectByText(coutrySelect, country);
        Clear(phoneInp);
        SendKeys(phoneInp, phoneNumber);
        WaitUtility.WaitForElementToBeClickable(submitBtn);
        ActionsUtility.ScrollTo(submitBtn);
        Click(submitBtn);
        return true;
      }
      catch (Exception ex)
      {
        Report.LogInfo($"Failed to add address: {ex.Message}");
        return false;
      }
    }
    public string GetFullName(string address)
    {
      Report.LogInfo("Retrieving full name from address entry.");
      return WaitUtility.WaitForElementToBeVisible(ViewFullName(address)).Text;
    }
    public string GetAddress(string address)
    {
      Report.LogInfo("Retrieving address from address entry.");
      return WaitUtility.WaitForElementToBeVisible(ViewAddress(address)).Text;
    }
    public string GetPhoneNumber(string address)
    {
      Report.LogInfo("Retrieving phone number from address entry.");
      return WaitUtility.WaitForElementToBeVisible(ViewPhoneNumber(address)).Text;
    }

  }
}
