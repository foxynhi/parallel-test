using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Utilities;
using ParalelTest.Pages.OrderPage;

namespace ParalelTest.Pages
{
  public class CartPage : BasePage
  {
    private readonly By title = By.XPath("//h1[contains(text(),'Giỏ hàng của bạn')]");
    private List<IWebElement> GetCartRows()
    {
      return driver.FindElements(By.XPath("//form[@id='cartformpage']//tbody/tr")).ToList();
    }
    public bool IsOnCartPage()
    {
      return WaitUtility.WaitForElementToBeVisible(title, 10).Displayed;
    }
    public List<string> GetCartItems()
    {
      Report.LogInfo("Retrieving items from the cart.");
      List<IWebElement> itemRows = driver.FindElements(By.XPath("//form[@id='cartformpage']//tbody/tr")).ToList();
      bool isCartEmpty = Find(By.XPath("//p[@class='count-cart']/span")).Text.Equals("0 sản phẩm");

      if (itemRows.Count == 0 || isCartEmpty)
      {
        Report.LogInfo("Cart is empty.");
        return null;
      }

      List<string> cartItems = new List<string>();
      foreach (var item in itemRows)
      {
        var itemName = item.FindElement(By.XPath(".//h3")).Text;
        cartItems.Add(itemName);
      }
      Report.LogInfo($"Number of items in cart: {cartItems.Count}");
      return cartItems;
    }
    public bool DeleteItemFromCart(string itemName)
    {
      Report.LogInfo($"Attempting to delete item '{itemName}' from cart.");
      try
      {
        By deleteButton = By.XPath($"//form[@id='cartformpage']//tbody/tr[.//h3[contains(text(), '{itemName}')]]/td[3]/a[1]");
        WaitUtility.WaitForElementToBeClickable(deleteButton, 10).Click();
        Report.LogInfo($"Item '{itemName}' deleted successfully.");
        return true;
      }
      catch (NoSuchElementException)
      {
        Report.LogFail($"Item '{itemName}' not found in the cart.");
        return false;
      }
      catch (WebDriverTimeoutException)
      {
        Report.LogFail($"Item '{itemName}' not found in the cart.");
        return false;
      }
    }
    public bool DeleteAllItemsFromCart()
    {
      var products = GetCartItems();

      foreach (var product in products)
      {
        if (!DeleteItemFromCart(product))
        {
          Report.LogFail($"Failed to delete item: {product}");
          return false;
        }
      }
      return true;
    }
    public CheckOutPage GoToCheckOut()
    {
      Report.LogInfo("Initiating checkout process.");
      By checkOutBtn = By.XPath("//button[@id='checkout']");
      ActionsUtility.ScrollTo(checkOutBtn);
      Click(checkOutBtn);
      return new CheckOutPage();
    }
  }
}
