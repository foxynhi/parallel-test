using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NUnit.Framework;

namespace ParalelTest.Utilities
{
  public class ExcelUtility : Utility
  {
    public static string GetCellAsString(ICell cell)
    {
      if (cell == null)
        return string.Empty;

      return cell.CellType switch
      {
        CellType.String => cell.StringCellValue,
        CellType.Numeric => cell.NumericCellValue.ToString(),
        CellType.Boolean => cell.BooleanCellValue.ToString(),
        CellType.Formula => cell.ToString(), // or use EvaluateFormula
        CellType.Blank => string.Empty,
        _ => cell.ToString()
      };
    }
    public static IEnumerable<TestCaseData> LogInTestData()
    {
      string filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "..", "LogInData.xlsx");
      Console.WriteLine($"Loading test data from: {filePath}");

      using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      {
        var workbook = new XSSFWorkbook(fs);
        var sheet = workbook.GetSheetAt(0);

        for (int row = 1; row <= sheet.LastRowNum; row++)
        {
          var email = GetCellAsString(sheet.GetRow(row).GetCell(0));
          var password = GetCellAsString(sheet.GetRow(row).GetCell(1));
          var isSuccess = sheet.GetRow(row).GetCell(2)?.BooleanCellValue ?? false;

          yield return new TestCaseData(email, password, isSuccess)
              .SetName($"LoginTest_{email}_{isSuccess}");
        }
      }
    }
  }
}
