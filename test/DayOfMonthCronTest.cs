using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
  [TestClass]
  public class DayOfMonthCronTest
  {
    [TestMethod]
    public void Every_Day()
    {
      // Arrange
      var cron = new Cron("0 0 * * *");

      // ActAssert
      for (int i = 1; i <= 31; i++)
      {
        var expectedDay = 0;

        if (i < 31)
        {
          expectedDay = i + 1;

          Helper.ActAssert(cron,
            new DateTime(2000, 1, i, 0, 0, 0),
            new DateTime(2000, 1, expectedDay, 0, 0, 0));
        }
        else
        {
          Helper.ActAssert(cron,
            new DateTime(2000, 1, i, 0, 0, 0),
            new DateTime(2000, 2, 1, 0, 0, 0));
        }
      }
    }

    [TestMethod]
    public void Every_1_30_Days()
    {
      // Arrange
      var cron = new Cron("0 0 1,30 * *");

      // ActAssert
      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 12, 1, 0),
        new DateTime(2000, 1, 30, 0, 0, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 30, 23, 1, 0),
        new DateTime(2000, 2, 1, 0, 0, 0));
    }

    [TestMethod]
    public void Every_2_Day_with_LeapYear()
    {
      // Arrange
      var cron = new Cron("0 0 2 * *");

      // ActAssert
      Helper.ActAssert(cron,
        new DateTime(2000, 2, 2, 23, 30, 1),
        new DateTime(2000, 3, 2, 0, 00, 0));
    }
  }
}
