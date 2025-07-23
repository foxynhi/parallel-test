using ParalelTest.Base;
using OpenQA.Selenium;
using System;

namespace ParalelTest.Utilities
{
  public class Utility
  {
    public static IWebDriver driver => DriverManager.GetDriver();
  }
}
