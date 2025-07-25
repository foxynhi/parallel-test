using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParalelTest.Base
{
  public static class DriverManager
  {
    private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

    public static void InitDriver()
    {
      if (driver.Value == null)
      {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-extensions");
        options.AddArgument("--disable-web-security");
        options.AddArgument("--remote-debugging-port=0"); // Use random port to avoid conflicts
        options.AddArgument("--window-size=1920,1080");

        // CRITICAL: Use incognito mode to prevent session sharing
        options.AddArgument("--incognito");

        // Disable shared memory and cache to ensure complete isolation
        options.AddArgument("--disable-shared-memory");
        options.AddArgument("--disable-background-networking");
        options.AddArgument("--disable-default-apps");
        options.AddArgument("--disable-sync");

        // Performance optimizations for parallel execution
        options.AddArgument("--disable-background-timer-throttling");
        options.AddArgument("--disable-backgrounding-occluded-windows");
        options.AddArgument("--disable-renderer-backgrounding");

        driver.Value = new ChromeDriver(options);
        driver.Value.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        driver.Value.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
      }
    }

    public static IWebDriver GetDriver()
    {
      if (driver.Value == null)
      {
        throw new InvalidOperationException("Driver not initialized. Call InitDriver() first.");
      }
      return driver.Value;
    }

    public static void QuitDriver()
    {
      if (driver.IsValueCreated && driver.Value != null)
      {
        try
        {
          driver.Value.Quit();
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error quitting driver: {ex.Message}");
        }
        finally
        {
          try
          {
            driver.Value.Dispose();
          }
          catch (Exception ex)
          {
            Console.WriteLine($"Error disposing driver: {ex.Message}");
          }
          finally
          {
            driver.Value = null;
          }
        }
      }
    }
    public static bool IsDriverInitialized()
    {
      return driver.IsValueCreated && driver.Value != null;
    }
  }
}