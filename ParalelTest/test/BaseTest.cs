using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Pages;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  [TestFixture]
  [Parallelizable(ParallelScope.All)]
  public class BaseTest
  {
    protected IWebDriver driver;
    protected BasePage basePage;
    protected HomePage homePage;
    protected string email = "Nguyen102500@gmail.com";
    protected string password = "PuQ8a6eg!ptG";
    protected void LogIn()
    {
      //Report.LogInfo("Starting LogIn test");

      var logInPage = homePage.GoToLogInPage();
      logInPage.LogIn(email, password);
      Assert.That(homePage.IsUserLoggedIn(email), Is.True);
    }

    [SetUp]
    public void Setup()
    {
      try
      {
        DriverManager.InitDriver();
        var driver = DriverManager.GetDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://matkinhshady.com/");
        Report.InitReport();
        Report.CreateTest(TestContext.CurrentContext.Test.MethodName);
        basePage = new BasePage();
        homePage = new HomePage();
      }
      catch (Exception ex)
      {
          Console.WriteLine($"Setup failed: {ex.Message}");
          throw; // Re-throw to fail the test
      }
    }

    [TearDown]
    public void TearDown()
    {
      try
      {
        EndTest();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"EndTest failed: {ex.Message}");
      }
      try
      {
        Report.FlushReport();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"FlushReport failed: {ex.Message}");
      }

      try
      {
        DriverManager.QuitDriver();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"QuitDriver failed: {ex.Message}");
      }
    }
    private void EndTest()
    {
      var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
      var message = TestContext.CurrentContext.Result.Message;

      switch (testStatus)
      {
        case TestStatus.Passed:
          Report.LogPass("Test passed");
          break;
        case TestStatus.Failed:
          Report.LogFail($"Test failed: {message}");
          //Report.LogScreenShot("Screenshot is logged at", ScreenshotUtility.TakeScreenshotAsBase64());
          //ScreenshotUtility.TakeScreenshotToFile(TestContext.CurrentContext.Test.MethodName);
          break;
        case TestStatus.Skipped:
          Report.LogInfo($"Test skipped: {message}");
          break;
        default:
          Report.LogInfo("Test completed with unknown status");
          break;
      }
      Report.LogInfo("Test ended");
    }
  }
}