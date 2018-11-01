using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tomware.Microcron.Core;

namespace tomware.Microcron.Tests
{
	[TestClass]
	public class MinutesCronTest
	{
		[TestMethod]
		public void Every_Minute()
		{
			// Arrange
			string expr = Expressions.DEFAULT_EXPRESSION;

			var cron = new Cron(expr);

			// ActAssert
			for (int i = 0; i <= 59; i++)
			{
				var expected = 0;
				
				if (i < 59)
				{
					expected = i + 1;

					Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, i, 0),
					  new DateTime(2000, 1, 1, 0, expected, 0));
				}
				else
				{
					Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, i, 0),
					  new DateTime(2000, 1, 1, 1, expected, 0));
				}				
			}
		}

		[TestMethod]
		public void Every_10_Minutes()
		{
			// Arrange
			string everyTenMinuteExpr = "0,10,20,30,40,50 * * * *";

			var cron = new Cron(everyTenMinuteExpr);
			
			// ActAssert
			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 0, 0),
					  new DateTime(2000, 1, 1, 0, 10, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 11, 0),
					  new DateTime(2000, 1, 1, 0, 20, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 29, 0),
					  new DateTime(2000, 1, 1, 0, 30, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 34, 0),
					  new DateTime(2000, 1, 1, 0, 40, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 46, 0),
					  new DateTime(2000, 1, 1, 0, 50, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 51, 0),
					  new DateTime(2000, 1, 1, 1, 00, 0));
		}

		[TestMethod]
		public void Every_30_Minutes()
		{
			// Arrange
			string everyTenMinuteExpr = "0,30 * * * *";

			var cron = new Cron(everyTenMinuteExpr);

			// ActAssert
			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 0, 0),
					  new DateTime(2000, 1, 1, 0, 30, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 11, 45),
					  new DateTime(2000, 1, 1, 0, 30, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 29, 0),
					  new DateTime(2000, 1, 1, 0, 30, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 0, 1),
					  new DateTime(2000, 1, 1, 0, 30, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 30, 1),
					  new DateTime(2000, 1, 1, 1, 00, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 59, 59),
					  new DateTime(2000, 1, 1, 1, 00, 0));

			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 23, 59, 0),
					  new DateTime(2000, 1, 2, 0, 00, 0));
		}

		[TestMethod]
		public void Every_30_Minutes_with_LeapYear()
		{
			// Arrange
			string everyTenMinuteExpr = "0,30 * * * *";

			var cron = new Cron(everyTenMinuteExpr);

			// ActAssert
			Helper.ActAssert(cron,
					  new DateTime(2000, 2, 29, 23, 30, 1),
					  new DateTime(2000, 3, 1, 0, 00, 0));
		}

		[TestMethod]
		public void AllwaysOn_2_22_42_Minutes()
		{
			// Arrange
			string allwaysOnExpr = "2,22,42 * * * *";

			var cron = new Cron(allwaysOnExpr);

			// ActAssert
			Helper.ActAssert(cron,
					  new DateTime(2000, 1, 1, 0, 3, 0),
					  new DateTime(2000, 1, 1, 0, 22, 0));
		}
	}
}
