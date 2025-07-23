using OpenQA.Selenium;

namespace ParalelTest.Utilities
{
  public class JSUtility : Utility
  {
    private static IJavaScriptExecutor JSExecutor => (IJavaScriptExecutor)driver;

    public static void ScrollTo(By locator)
    {
      JSExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(locator));
    }
    public static void ClickJS(By locator)
    {
      JSExecutor.ExecuteScript("arguments[0].click();", driver.FindElement(locator));
    }
  }
}
