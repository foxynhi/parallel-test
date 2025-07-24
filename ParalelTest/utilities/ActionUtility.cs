using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using ParalelTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelTest.Utilities
{
  public class ActionsUtility : Utility
  {
    private static Actions Act()
    {
      return new Actions(driver);
    }
    public static void MoveToElement(By element)
    {
      Act().MoveToElement(driver.FindElement(element)).Perform();
    }

    internal static void ScrollTo(By element)
    {
      Act().ScrollToElement(driver.FindElement(element)).Perform();
    }
  }
}
