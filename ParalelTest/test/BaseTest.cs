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
    protected string emailDefault = "Nguyen102500@gmail.com";
    protected string passwordDefault = "PuQ8a6eg!ptG";

    protected static readonly List<string> products = new List<string>
    {
      "Molsion Eyewear - Sunglasses - MS3070",
      "Gucci Eyewear - Sunglasses - GG1681S",
      "Exfash Eyewear - Sunglasses - EF54710"
    };

    protected void LogIn(string email, string password, bool result)
    {
      Report.LogInfo($"Starting LogIn test on thread {Thread.CurrentThread.ManagedThreadId}");
      try { 
        var logInPage = homePage.GoToLogInPage();
        logInPage.LogIn(email, password);
        Thread.Sleep(1000);

        //bool isLoggedIn = homePage.IsUserLoggedIn(email);
        //Report.LogInfo($"Thread {Thread.CurrentThread.ManagedThreadId} - Is user logged in: {isLoggedIn} - For email: {email}");

        //Assert.That(isLoggedIn, Is.EqualTo(result));
      }
      catch (Exception ex)
      {
        Report.LogFail($"LogIn method failed: {ex.Message}");
        throw;
      }
    }

    [SetUp]
    public void Setup()
    {
      var threadId = Thread.CurrentThread.ManagedThreadId;
      var testName = TestContext.CurrentContext.Test.Name;

      Console.WriteLine($"[Thread {threadId}] Starting setup for test: {testName}");
      Report.LogInfo($"Starting setup on thread {threadId} for test: {testName}");
      try
      {
        DriverManager.InitDriver();
        var driver = DriverManager.GetDriver();

        Report.InitReport();
        Report.CreateTest(TestContext.CurrentContext.Test.Name);
        Report.LogInfo($"Test '{testName}' started on thread {threadId}");

        driver.Manage().Window.Maximize(); 
        driver.Navigate().GoToUrl("https://matkinhshady.com/");
        driver.Manage().Cookies.DeleteAllCookies();
        Report.LogInfo($"[Thread {threadId}] Navigated to: https://matkinhshady.com/");


        basePage = new BasePage();
        homePage = new HomePage();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Thread {threadId}] Setup failed: {ex.Message}");
        Report.LogFail($"Setup failed: {ex.Message}");

        // Cleanup on setup failure
        try
        {
          DriverManager.QuitDriver();
        }
        catch (Exception cleanupEx)
        {
          Console.WriteLine($"[Thread {threadId}] Cleanup after setup failure failed: {cleanupEx.Message}");
        }

        throw;
      }
    }

    [TearDown]
    public void TearDown()
    {
      var threadId = Thread.CurrentThread.ManagedThreadId;
      var testName = TestContext.CurrentContext.Test.Name;

      Console.WriteLine($"[Thread {threadId}] Starting teardown for test: {testName}");

      try
      {
        EndTest();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Thread {threadId}] EndTest failed: {ex.Message}");
        Report.LogFail($"EndTest failed: {ex.Message}");
      }

      try
      {
        Report.FlushReport();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Thread {threadId}] FlushReport failed: {ex.Message}");
      }

      try
      {
        Report.Cleanup();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Thread {threadId}] Report cleanup failed: {ex.Message}");
      }

      try
      {
        DriverManager.QuitDriver();
        Console.WriteLine($"[Thread {threadId}] Driver quit successfully");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[Thread {threadId}] QuitDriver failed: {ex.Message}");
      }

      Console.WriteLine($"[Thread {threadId}] Teardown completed for test: {testName}");
    }
    private void EndTest()
    {
      var threadId = Thread.CurrentThread.ManagedThreadId;
      var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
      var message = TestContext.CurrentContext.Result.Message;

      switch (testStatus)
      {
        case TestStatus.Passed:
          Report.LogPass("Test passed");
          Console.WriteLine($"[Thread {threadId}] Test PASSED");
          break;

        case TestStatus.Failed:
          Report.LogFail($"Test failed: {message}");
          Console.WriteLine($"[Thread {threadId}] Test FAILED: {message}");

          try
          {
            if (DriverManager.IsDriverInitialized())
            {
              var screenshot = ScreenshotUtility.TakeScreenshotAsBase64();
              Report.LogScreenShot("Screenshot is logged at", screenshot);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine($"[Thread {threadId}] Screenshot capture failed: {ex.Message}");
          }
          break;

        case TestStatus.Skipped:
          Report.LogInfo($"Test skipped: {message}");
          Console.WriteLine($"[Thread {threadId}] Test SKIPPED: {message}");
          break;

        default:
          Report.LogInfo("Test completed with unknown status");
          Console.WriteLine($"[Thread {threadId}] Test completed with UNKNOWN status");
          break;
      }

      Report.LogInfo($"Test ended on thread {threadId}");
    }
  }
}