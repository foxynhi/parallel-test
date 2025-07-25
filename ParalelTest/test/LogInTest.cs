using NPOI.SS.Formula.Functions;
using NUnit.Framework;
using ParalelTest.Utilities;

namespace ParalelTest.Test
{
  [TestFixture]
  [Parallelizable(ParallelScope.All)]
  public class LogInTest : BaseTest
  {
    [Test, TestCaseSource(typeof(ExcelUtility), nameof(ExcelUtility.LogInTestData))]
    //[TestCase("Nguyen102500@gmail.com", "PuQ8a6eg!ptG", true)]
    //[TestCase("wrong@user.com", "wrongpass", false)]
    //[TestCase("Nguyen102500@gmail.com", "wrongpass", false)]
    //[TestCase("joedoe1@gmail.com", "12341234", true)]
    public void LogInUser(string email, string password, bool result)
    {
      LogIn(email, password, result);
    }
    //[Test]
    public void CheckData()
    {
      var data = ExcelUtility.LogInTestData();
      foreach (var item in data)
      {
        var testCase = item as NUnit.Framework.Interfaces.ITestCaseData;
        if (testCase != null)
        {
          Console.WriteLine($"Test Case: {testCase.TestName}");
          Console.WriteLine($"Email: {testCase.Arguments[0]}");
          Console.WriteLine($"Password: {testCase.Arguments[1].ToString}");
          Console.WriteLine($"Expected Result: {testCase.Arguments[2]}");
        }
      }
    }
  }
}