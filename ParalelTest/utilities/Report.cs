using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System.Reflection;

namespace ParalelTest.Utilities
{
  public class Report
  {
    private static ExtentReports extent;
    private static ExtentHtmlReporter htmlReporter;
    private static ExtentTest test;

    public static void InitReport()
    {
      try {
        string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "TestResults\\"));
        if (!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
        }
        string fileName = path + TestContext.CurrentContext.Test.MethodName + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".html";
        Console.WriteLine($"Report file path: {fileName}");
        extent = new ExtentReports();
        htmlReporter = new ExtentHtmlReporter(fileName);
        extent.AttachReporter(htmlReporter);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to initialize report: {ex.Message}");
        extent = null;
      }
    }
    public static void CreateTest(string testName)
    {
      if (extent == null) {
        InitReport();
      }
      if (extent != null)
      {
        test = extent.CreateTest(testName);
      }
    }
    public static void FlushReport()
    {
      try
      {
        extent?.Flush();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to flush report: {ex.Message}");
      }
    }
    public static void LogInfo(string message)
    {
      test.Info(message);
    }
    public static void LogFail(string message)
    {
      test.Fail(message);
    }
    public static void LogPass(string message)
    {
      test.Pass(message);
    }
    public static void LogScreenShot(string message, string img)
    {
      test.Info(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(img).Build());
    }
  }
}
