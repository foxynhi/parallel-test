using NUnit.Framework;
using System;

namespace ParalelTest.Test
{
  public class SignUpTest : BaseTest
  {
    [Test]
    [Parallelizable(ParallelScope.Self)]
    public void SignUp()
    {
      //ExtentReporting.LogInfo("Starting SignUp test");
      var signUpPage = homePage.GoToLogInPage().GoToSignUpPage();
      signUpPage.VerifySignUpPage();

      string lastName = "Nguyen";
      string firstName = "Van A";
      string email = $"{lastName}{DateTime.Now:HHmmss}@gmail.com";
      string password = "PuQ8a6eg!ptG";
      var signUpResult = signUpPage.SignUp(lastName, firstName, email, password);
      //Assert.That(signUpResult, Is.True);
      //ExtentReporting.LogInfo($"SignUp completed with email: {email}, password: {password}");
      //Assert.That(homePage.IsUserLoggedIn(email), Is.True);
    }
  }
}
