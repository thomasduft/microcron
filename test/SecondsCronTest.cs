using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
  [TestClass]
  public class SecondsCronTest
  {
    [TestMethod]
    public void Every_Second()
    {
      // Arrange
      var cron = new Cron("* * * * * *");

      // ActAssert
      for (int i = 0; i <= 59; i++)
      {
        var expected = 0;

        if (i < 59)
        {
          expected = i + 1;

          Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 0, 0, i),
            new DateTime(2000, 1, 1, 0, 0, expected));
        }
        else
        {
          Helper.ActAssert(cron,
            new DateTime(2000, 1, 1, 0, 0, i),
            new DateTime(2000, 1, 1, 0, 1, expected));
        }
      }
    }

    [TestMethod]
    public void Every_10_Seconds()
    {
      // Arrange
      var cron = new Cron("0,10,20,30,40,50 * * * * *");

      // ActAssert
      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 0),
        new DateTime(2000, 1, 1, 0, 0, 10));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 11),
        new DateTime(2000, 1, 1, 0, 0, 20));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 29),
        new DateTime(2000, 1, 1, 0, 0, 30));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 34),
        new DateTime(2000, 1, 1, 0, 0, 40));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 46),
        new DateTime(2000, 1, 1, 0, 0, 50));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 51),
        new DateTime(2000, 1, 1, 0, 1, 0));
    }

    [TestMethod]
    public void Every_30_Seconds()
    {
      // Arrange
      var cron = new Cron("0,30 * * * * *");

      // ActAssert
      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 0),
        new DateTime(2000, 1, 1, 0, 0, 30));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 11, 45),
        new DateTime(2000, 1, 1, 0, 12, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 29, 0),
        new DateTime(2000, 1, 1, 0, 29, 30));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 0, 1),
        new DateTime(2000, 1, 1, 0, 0, 30));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 29, 1),
        new DateTime(2000, 1, 1, 0, 29, 30));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 0, 59, 59),
        new DateTime(2000, 1, 1, 1, 0, 0));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 23, 59, 0),
        new DateTime(2000, 1, 1, 23, 59, 30));

      Helper.ActAssert(cron,
        new DateTime(2000, 1, 1, 23, 59, 30),
        new DateTime(2000, 1, 2, 0, 0, 0));
    }
  }
}
