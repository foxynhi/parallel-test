using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System.Reflection;

namespace ParalelTest.Utilities
{
  public class Report
  {
    private static ThreadLocal<ExtentReports> extent = new ThreadLocal<ExtentReports>();
    private static ThreadLocal<ExtentSparkReporter> spark = new ThreadLocal<ExtentSparkReporter>();
    private static readonly object lockObj = new object();
    private static ThreadLocal<ExtentTest> test = new ThreadLocal<ExtentTest>();

    public static void InitReport()
    {
      try {
        string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "TestResults\\"));
        Console.WriteLine("path ", path);
        if (!Directory.Exists(path))
        {
          Directory.CreateDirectory(path);
        }
        string fileName = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true"
          ? Path.Combine(path, TestContext.CurrentContext.Test.MethodName + "-CI_Report.html")
          : path + TestContext.CurrentContext.Test.MethodName + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".html";
        Console.WriteLine($"Report file path: {fileName}");
        var report = new ExtentReports();
        var reporter = new ExtentSparkReporter(fileName);
        report.AttachReporter(reporter);
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
        test.Value = extent.Value?.CreateTest(testName);
      }
    }
    public static void FlushReport()
    {
      try
      {
        extent.Value?.Flush();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to flush report: {ex.Message}");
      }
    }
    public static void LogInfo(string message)
    {
      test.Value?.Info(message);
    }
    public static void LogFail(string message)
    {
      test.Value?.Fail(message);
    }
    public static void LogPass(string message)
    {
      test.Value?.Pass(message);
    }
    public static void LogScreenShot(string message, string img)
    {
      test.Value?.Info(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(img).Build());
    }
  }
}
