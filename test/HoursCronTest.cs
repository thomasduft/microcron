using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
  [TestClass]
  public class HoursCronTest
  {
    [TestMethod]
    public void Every_Hour()
    {
      // Arrange
      string expr = "0 * * * *";

      var cron = new Cron(expr);

      // ActAssert
      for (int i = 0; i <= 23; i++)
      {
        var expectedHour = 0;

        if (i < 23)
        {
          expectedHour = i + 1;

          Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, i, 0, 0),
            new DateTime(2000, 1, 1, expectedHour, 0, 0));
        }
        else
        {
          Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, i, 0, 0),
            new DateTime(2000, 1, 2, expectedHour, 0, 0));
        }
      }
    }

    [TestMethod]
    public void Every_6_Hours()
    {
      // Arrange
      string expr = "0 0,6,12,18 * * *";

      var cron = new Cron(expr);

      // ActAssert
      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 0, 0, 0),
            new DateTime(2000, 1, 1, 6, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 6, 11, 0),
            new DateTime(2000, 1, 1, 12, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 12, 29, 0),
            new DateTime(2000, 1, 1, 18, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 18, 34, 0),
            new DateTime(2000, 1, 2, 0, 0, 0));
    }

    [TestMethod]
    public void AllwaysOn_6_8_14_16_Hours()
    {
      // Arrange
      string expr = "0 6,8,14,16 * * *";

      var cron = new Cron(expr);

      // ActAssert
      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 0, 3, 0),
            new DateTime(2000, 1, 1, 6, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 6, 3, 0),
            new DateTime(2000, 1, 1, 8, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 8, 3, 0),
            new DateTime(2000, 1, 1, 14, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 14, 3, 0),
            new DateTime(2000, 1, 1, 16, 0, 0));

      Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 16, 0, 0),
            new DateTime(2000, 1, 2, 6, 0, 0));
    }

    [TestMethod]
    public void Every_12_Hours_with_LeapYear()
    {
      // Arrange
      string expr = "0 0,12 * * *";

      var cron = new Cron(expr);

      // ActAssert
      Helper.ActAssert(cron,
            new DateTime(2000, 2, 29, 23, 30, 1),
            new DateTime(2000, 3, 1, 0, 00, 0));
    }
  }
}
