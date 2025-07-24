using NUnit.Framework;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  [TestFixture]
  [Parallelizable(ParallelScope.Fixtures)]
  [NonParallelizable]
  public class AddAddressTest : BaseTest
  {

    private readonly string lastName = "Nguyen";
    private readonly string firstName = "Van A";
    private readonly string address = "321 An Thuong";
    private readonly string phoneNumber = "0123456789";

    [Test, Order(1)]
    public void AddAddress()
    {
      Report.LogInfo("Starting Add Address test");
      LogIn();
      var addressPage = homePage.GoToLogInPage().GoToAddressPage();
      Assert.That(addressPage.IsOnAddressPage(), Is.True, "Not on Address Page");

      bool addAddressResult = addressPage.AddAdress(lastName, firstName, address, phoneNumber);
      if (!addAddressResult)
      {
        Report.LogFail("Failed to add address");
      }
      Report.LogInfo($"Add Address successfully with name '{lastName} {firstName}', address '{address}', phone number '{phoneNumber}'");
    }

    [Test, Order(2)]
    public void VerifyAddedAddress()
    {
      Report.LogInfo("Starting Verify Added Address test");
      LogIn();
      var addressPage = homePage.GoToLogInPage().GoToAddressPage();
      Assert.That(addressPage.IsOnAddressPage(), Is.True, "Not on Address Page");

      Report.LogInfo($"Verifying name match: {lastName} {firstName}");
      Assert.That(addressPage.GetFullName(address), Does.Contain(lastName), "Full name does not match");
      Report.LogInfo($"Verifying added address match: {address}");
      Assert.That(addressPage.GetAddress(address), Does.Contain(address), "Address does not match");
      Report.LogInfo($"Verifying phone number match: {phoneNumber}");
      Assert.That(addressPage.GetPhoneNumber(address), Does.Contain(phoneNumber), "Phone number does not match");

      Report.LogInfo("Verify Added Address successfully");
    }
  }
}
