using OpenQA.Selenium;

namespace ParalelTest.Utilities
{
  public class ScreenshotUtility : Utility
  {
    public static string TakeScreenshotAsBase64()
    {
      var img = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
      return img;
    }

    public static string TakeScreenshotToFile(string methodName)
    {
      string timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

      string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "ReportResults", "Screenshots\\");
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      string fileName = Path.Combine(path, $"{methodName}-{timestamp}.png");
      Console.WriteLine($"Screenshot file path: {fileName}");
      Directory.CreateDirectory(Path.GetDirectoryName(fileName));

      var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
      screenshot.SaveAsFile(fileName);

      Report.LogInfo($"Screenshot is saved at: {Path.GetFullPath(fileName)}");

      return fileName;
    }
  }
}
