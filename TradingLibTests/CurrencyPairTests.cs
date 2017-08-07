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
	public class CurrencyPairTests
	{
		[TestMethod()]
		public void GetBitfinexCurrencyPairTest()
		{
			CurrencyPair cp = new CurrencyPair(0, 1);
			Assert.AreEqual(cp.GetBitfinexCurrencyPair(), "tBTCUSD");
		}

		[TestMethod()]
		[ExpectedException(typeof(UnknownCurrencyPairException), "Currency pair (1, 1) unknown.")]
		public void ThrowsCurrencyPairException()
		{
			CurrencyPair cp = new CurrencyPair(1, 1);
			cp.GetBitfinexCurrencyPair();
		}
	}
}