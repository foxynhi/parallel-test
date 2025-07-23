using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace ParalelTest.Base
{
  public static class DriverManager
  {
    private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

    public static void InitDriver()
    {
      if (driver.Value == null)
      {
        driver.Value = new ChromeDriver();
      }
    }

    public static IWebDriver GetDriver()
    {
      return driver.Value;
    }

    public static void QuitDriver()
    {
      if (driver.IsValueCreated)
      {
        driver.Value.Quit();
        driver.Value.Dispose();
        driver.Value = null;
      }
    }
  }
}