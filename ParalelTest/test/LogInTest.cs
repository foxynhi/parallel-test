using NUnit.Framework;
using ParalelTest.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelTest.Test
{
  public class LogInTest : BaseTest
  {
    [Test]
    [Parallelizable(ParallelScope.Self)]
    public void LogInUser()
    {
      LogIn();
    }
  }
}