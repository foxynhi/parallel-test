using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ParalelTest.Utilities
{
  public class WaitUtility : Utility
  {
    public static WebDriverWait Wait(int seconds = 10)
    {
      return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
    }
    public static IWebElement WaitForElementToBeVisible(By locator, int seconds = 10)
    {
       return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(locator));
    }
    public static IWebElement WaitForElementToBeClickable(By locator, int seconds = 10)
    {
      return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(locator));
    }
    public static IAlert WaitForAlert(int seconds = 3)
    {
      return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.AlertIsPresent());
    }
  }
}
