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
        if (extent.Value != null)
          return;
        string path = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true"
          ? Path.Combine(Environment.CurrentDirectory, "TestResults")
          : Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "TestResults"));

         Console.WriteLine("path " + path);
        lock (lockObj)
        {
          if (!Directory.Exists(path))
          {
            Directory.CreateDirectory(path);
          }
        }
        string fileName = Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true"
          ? Path.Combine(path, $"{TestContext.CurrentContext.Test.MethodName}-CI_Report.html")
          : Path.Combine(path, $"{TestContext.CurrentContext.Test.MethodName}_{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.html");
         Console.WriteLine($"Report file path: {fileName}");

        var reporter = new ExtentSparkReporter(fileName);
        var report = new ExtentReports();
        report.AttachReporter(reporter);

        spark.Value = reporter;
        extent.Value = report;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to initialize report: {ex.Message}");
        extent = null;
      }
    }
    public static void CreateTest(string testName)
    {
      if (extent.Value == null) {
        InitReport();
      }
      if (extent.Value != null)
      {
        test.Value = extent.Value.CreateTest(testName);
      }
    }
    public static void FlushReport()
    {
      try
      {
        if (extent.Value != null)
        {
          extent.Value.Flush();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to flush report: {ex.Message}");
      }
    }
    public static void Cleanup()
    {
      try
      {
        extent.Value?.Flush();
        extent.Value = null;
        spark.Value = null;
        test.Value = null;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to cleanup report resources: {ex.Message}");
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
