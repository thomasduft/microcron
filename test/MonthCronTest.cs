using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
  [TestClass]
  public class MonthCronTest
  {
    [TestMethod]
    public void Every_Month()
    {
      // Arrange
      string expr = "0 0 1 * *";

      var cron = new Cron(expr);

      // ActAssert
      for (int i = 1; i <= 12; i++)
      {
        var expected = 0;

        if (i < 12)
        {
          expected = i + 1;

          Helper.ActAssert(cron,
            new DateTime(2000, i, 1, 0, 0, 0),
            new DateTime(2000, expected, 1, 0, 0, 0));
        }
        else
        {
          Helper.ActAssert(cron,
            new DateTime(2000, i, 1, 0, 0, 0),
            new DateTime(2001, 1, 1, 0, 0, 0));
        }
      }
    }

    [TestMethod]
    public void Only_In_April()
    {
      // Arrange
      string expr = "0 0 * APR *";

      var cron = new Cron(expr);

      // ActAssert
      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 0),
        new DateTime(2000, 4, 1, 0, 0, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 4, 1, 0, 0, 0),
        new DateTime(2000, 4, 2, 0, 0, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 4, 2, 0, 0, 0),
        new DateTime(2000, 4, 3, 0, 0, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 4, 29, 0, 0, 0),
        new DateTime(2000, 4, 30, 0, 0, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 4, 30, 0, 0, 0),
        new DateTime(2001, 4, 1, 0, 0, 0));
    }
  }
}
