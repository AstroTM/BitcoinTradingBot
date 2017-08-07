using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingLib.Tests
{
	/// <summary>
	/// Tests class HistoricalTrade
	/// </summary>
	[TestClass()]
	public class HistoricalTradeTests
	{
		/// <summary>
		/// Tests HistoricalTrade functions with positive value (bid)
		/// </summary>
		[TestMethod()]
		public void HistoricalTradeTest1()
		{
			HistoricalTrade HT = new HistoricalTrade(1000, 1000000, 147.147, 999); // Creates new HistoricalTrade object with positive value

			Assert.AreEqual(HT.Amount, 147.147); // Check the value is inserted as a positive
			Assert.IsTrue(HT.IsBid); // Check IsBid is true
		}

		/// <summary>
		/// Tests HistoricalTrade functions with negative value (ask)
		/// </summary>
		[TestMethod()]
		public void HistoricalTradeTest2()
		{
			HistoricalTrade HT = new HistoricalTrade(1000, 1000000, -147.147, 999); // Creates new HistoricalTrade object with negative value

			Assert.AreEqual(HT.Amount, 147.147); // Check the value is inserted as a positive
			Assert.IsTrue(!HT.IsBid); // Check IsBid is false
		}
	}
}