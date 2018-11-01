using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
	[TestClass]
	public class DayOfWeekCronTest
	{
		[TestMethod]
		public void Every_Monday()
		{
			// Arrange
			string expr = "0 0 * * 1";

			var cron = new Cron(expr);

			// ActAssert
			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 1, 0, 0, 0),
			  new DateTime(2015, 1, 5, 0, 0, 0));

			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 5, 0, 0, 0),
			  new DateTime(2015, 1, 12, 0, 0, 0));
		}

		[TestMethod]
		public void Every_WorkDay()
		{
			// Arrange
			string expr = "0 0 * * 1,2,3,4,5";

			var cron = new Cron(expr);

			// ActAssert
			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 4, 0, 0, 0),
			  new DateTime(2015, 1, 5, 0, 0, 0));

			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 5, 0, 0, 0),
			  new DateTime(2015, 1, 6, 0, 0, 0));

			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 6, 0, 0, 0),
			  new DateTime(2015, 1, 7, 0, 0, 0));

			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 7, 0, 0, 0),
			  new DateTime(2015, 1, 8, 0, 0, 0));

			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 8, 0, 0, 0),
			  new DateTime(2015, 1, 9, 0, 0, 0));

			Helper.ActAssert(cron,
			  new DateTime(2015, 1, 9, 0, 0, 0),
			  new DateTime(2015, 1, 12, 0, 0, 0));
		}
	}
}
