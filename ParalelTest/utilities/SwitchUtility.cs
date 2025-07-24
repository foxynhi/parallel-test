using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelTest.Utilities
{
  public class SwitchUtility : Utility
  {
    private static ITargetLocator SwitchTo => driver.SwitchTo();

    public static string GetAlertText()
    {
      return SwitchTo.Alert().Text;
    }
    public static void SwitchToAlert()
    {
      SwitchTo.Alert();
    }
    public static void Accept()
    {
      SwitchTo.Alert().Accept();
    }
    public static void Close()
    {
      SwitchTo.Alert().Dismiss();
    }
  }
}
