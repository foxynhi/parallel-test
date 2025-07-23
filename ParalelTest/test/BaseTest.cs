using NUnit.Framework;
using OpenQA.Selenium;
using ParalelTest.Base;
using ParalelTest.Pages;

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
      //ExtentReporting.LogInfo("Starting LogIn test");

      var logInPage = homePage.GoToLogInPage();
      logInPage.LogIn(email, password);
      Assert.That(homePage.IsUserLoggedIn(email), Is.True);
    }

    [SetUp]
    public void Setup()
    {
      DriverManager.InitDriver();
      var driver = DriverManager.GetDriver();
      driver.Manage().Window.Maximize();
      driver.Navigate().GoToUrl("https://matkinhshady.com/");

      basePage = new BasePage();
      homePage = new HomePage();
    }

    [TearDown]
    public void TearDown()
    {
      DriverManager.QuitDriver();
    }
  }
}