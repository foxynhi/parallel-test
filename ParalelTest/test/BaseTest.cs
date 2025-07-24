using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Pages;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  [TestFixture]
  public class BaseTest
  {
    protected IWebDriver driver;
    protected BasePage basePage;
    protected HomePage homePage;
    protected string email = "Nguyen102500@gmail.com";
    protected string password = "PuQ8a6eg!ptG";

    protected static readonly List<string> products = new List<string>
    {
      "Molsion Eyewear - Sunglasses - MS3070",
      "Gucci Eyewear - Sunglasses - GG1681S",
      "Exfash Eyewear - Sunglasses - EF54710"
    };

    protected void LogIn()
    {
      Report.LogInfo("Starting LogIn test");

      var logInPage = homePage.GoToLogInPage();
      logInPage.LogIn(email, password);
      Assert.That(homePage.IsUserLoggedIn(email), Is.True);
    }

    [SetUp]
    public void Setup()
    {
      Report.LogInfo($"Starting setup on thread {Thread.CurrentThread.ManagedThreadId}");
      try
      {
        DriverManager.InitDriver();
        var driver = DriverManager.GetDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://matkinhshady.com/");

        Report.InitReport();
        Report.CreateTest(TestContext.CurrentContext.Test.Name);
        Report.LogInfo($"Test started on thread {Thread.CurrentThread.ManagedThreadId}");

        basePage = new BasePage();
        homePage = new HomePage();
      }
      catch (Exception ex)
      {
        Report.LogFail($"Setup failed: {ex.Message}");
          throw;
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
        Report.LogFail($"EndTest failed: {ex.Message}");
      }
      try
      {
        Report.FlushReport();
      }
      catch (Exception ex)
      {
        Report.LogFail($"FlushReport failed: {ex.Message}");
      }
      try
      {
        Report.Cleanup();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Report cleanup failed: {ex.Message}");
      }
      try
      {
        DriverManager.QuitDriver();
      }
      catch (Exception ex)
      {
        Report.LogFail($"QuitDriver failed: {ex.Message}");
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
          Report.LogScreenShot("Screenshot is logged at", ScreenshotUtility.TakeScreenshotAsBase64());
          //ScreenshotUtility.TakeScreenshotToFile(TestContext.CurrentContext.Test.MethodName);
          break;
        case TestStatus.Skipped:
          Report.LogInfo($"Test skipped: {message}");
          break;
        default:
          Report.LogInfo("Test completed with unknown status");
          break;
      }
      Report.LogInfo($"Test ended on thread {Thread.CurrentThread.ManagedThreadId}");
    }
  }
}