using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
  public static class Helper
  {
    public static void ActAssert(Cron cron, DateTime now, DateTime expected)
    {
      // Act
      var nextOccurence = cron.GetNextOccurrence(now);

      // Assert
      Assert.AreEqual(expected, nextOccurence);
    }
  }
}
