using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ParalelTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelTest.Utilities
{
  public class SelectUtility : Utility
  {
    private static SelectElement Select(IWebElement element)
    {
      return new SelectElement(element);
    }
    public static void SelectByText(By locator, string text)
    {
      Select(driver.FindElement(locator)).SelectByText(text);
    }
    public static void SelectByValue(By locator, string value)
    {
      Select(driver.FindElement(locator)).SelectByValue(value);
    }
  }
}
