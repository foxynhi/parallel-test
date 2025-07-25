using NUnit.Framework;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  public class DeleteCartTest : BaseTest
  {

    //[Test]
    public void DeleteAllCartTest()
    {
      Report.LogInfo("Starting DeleteAllCartTest test");
      LogIn(emailDefault, passwordDefault, true);
      var cartPage = homePage.GoToCartPage();
      Assert.That(cartPage.IsOnCartPage, Is.True);

      bool deleteAllCartResult = cartPage.DeleteAllItemsFromCart();
      Assert.That(deleteAllCartResult, Is.True, "The items was not deleted from the cart.");

      Report.LogInfo("All items deleted successfully from the cart.");
    }

    //[Test]
    public void DeleteCartItemTest()
    {
      Report.LogInfo("Starting DeleteCartItemTest test");
      LogIn(emailDefault, passwordDefault, true);
      var cartPage = homePage.GoToCartPage();
      Assert.That(cartPage.IsOnCartPage, Is.True);

      var initialCart = cartPage.GetCartItems();
      if (initialCart == null)
      {
        Assert.Fail("No items found in the cart. Skipping deletion.");
        return;
      }
      int initialCount = initialCart.Count;

      bool deleteCartResult = cartPage.DeleteItemFromCart(products[0]);
      if (!deleteCartResult)
      {
        Assert.Fail("The item was not deleted from the cart.");
        return;
      }
      var newCart = cartPage.GetCartItems();
      if (newCart == null && deleteCartResult)
      {
        Report.LogInfo("Item deleted successfully from the cart. Cart is now empty");
        return;
      }
      Assert.That(newCart.Count, Is.EqualTo(initialCount - 1), "The item was not deleted from the cart.");

      Report.LogInfo("Item deleted successfully from the cart.");
    }
  }
}
