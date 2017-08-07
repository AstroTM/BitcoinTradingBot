using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingLib.Tests
{
	[TestClass()]
	public class HistoricalTradeTests
	{
		[TestMethod()]
		public void HistoricalTradeTest1()
		{
			HistoricalTrade HT = new HistoricalTrade(1000, 1000000, 147.147, 999);

			Assert.AreEqual(HT.Amount, 147.147);
			Assert.IsTrue(HT.IsBid);
		}
		[TestMethod()]

		public void HistoricalTradeTest2()
		{
			HistoricalTrade HT = new HistoricalTrade(1000, 1000000, -147.147, 999);

			Assert.AreEqual(HT.Amount, 147.147);
			Assert.IsTrue(!HT.IsBid);
		}
	}
}