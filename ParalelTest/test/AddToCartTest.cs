using NUnit.Framework;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  [TestFixture]
  [Parallelizable(ParallelScope.Fixtures)]
  [NonParallelizable]
  public class AddToCartTest : BaseTest
  {
    private readonly string address = "123 Lí Thường Kiệt";
    private readonly string phoneNumber = "0987654321";

    private static IEnumerable<string> ProductData => products;

    [Test, TestCaseSource(nameof(ProductData)), Order(1)]
    public void BuyKinhMatNuTest(string product)
    {
      Report.LogInfo("Starting BuyKinhMatNuTest test");
      LogIn();

      var kinhMatNuPage = homePage.GoToKinhMatNu();
      Assert.That(kinhMatNuPage.IsOnKinhMatNuPage(), Is.True);

      var productPage = kinhMatNuPage.GoToProductPage(product);
      productPage.IsOnProductPage(product);
      bool isAdded = productPage.AddToCart();
      if (!isAdded)
      {
        productPage.ClickCartButton();
      }
      productPage.VerifyProductAddedToCart(product);
    }
    [Test, Order(2)]
    public void DeleteCartItemTest()
    {
      Report.LogInfo("Starting DeleteCartItemTest test");
      LogIn();
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
    [Test, Order(3)]
    public void VerifyCart()
    {
      Report.LogInfo("Verify Cart items");
      LogIn();
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
    [Test, Order(4)]
    public void CheckOutCartTest()
    {
      Report.LogInfo("Starting CheckOutCartTest test");
      LogIn();
      var cartPage = homePage.GoToCartPage();
      Assert.That(cartPage.IsOnCartPage, Is.True);
      if (cartPage.GetCartItems() == null)
      {
        Report.LogInfo("No items found in the cart. Skipping checkout.");
        return;
      }

      var checkOutPage = cartPage.GoToCheckOut();
      Assert.That(checkOutPage.IsOnCheckOutPage, Is.True);

      var successOrderPage = checkOutPage.CheckOutCart(phoneNumber, address);
      Report.LogInfo("Order number: " + successOrderPage.GetOrderNumber());
      Report.LogInfo("Total payment: " + successOrderPage.GetOrderTotal());
      Report.LogInfo("Payment method: " + successOrderPage.GetPaymentMethod());
      Report.LogInfo("Shipping method: " + successOrderPage.GetShippingMethod());
      Report.LogInfo("Shipping address: " + successOrderPage.GetShippingAddress());

      Report.LogInfo("CheckOutCartTest completed successfully.");
    }
  }
}
