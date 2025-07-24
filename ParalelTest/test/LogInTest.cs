using NUnit.Framework;

namespace ParalelTest.Test
{
  [TestFixture]
  [Parallelizable(ParallelScope.All)]
  public class LogInTest : BaseTest
  {
    [Test]
    public void LogInUser()
    {
      LogIn();
    }
  }
}