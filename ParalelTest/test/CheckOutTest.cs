using NUnit.Framework;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  public class CheckOutTest : BaseTest
  {
    private readonly string address = "123 Lí Thường Kiệt";
    private readonly string phoneNumber = "0987654321";

    //[Test]
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
