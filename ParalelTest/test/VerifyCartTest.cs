using NUnit.Framework;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  public class VerifyCartTest : BaseTest
  {
    //[Test]
    public void VerifyCart()
    {
      Report.LogInfo("Verify Cart items");
      LogIn(emailDefault, passwordDefault, true);
      var cartPage = homePage.GoToCartPage();
      Assert.That(cartPage.IsOnCartPage, Is.True);

      var cartItems = cartPage.GetCartItems();
      if (cartItems == null)
      {
        Report.LogInfo("No items found in the cart.");
        return;
      }
      cartItems.ForEach(item =>
      {
        Assert.That(products.Contains(item), Is.True, $"Item '{item}' is not in the expected product list.");
      });
    }
  }
}
