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
	/// Tests class CurrencyPair
	/// </summary>
	[TestClass()]
	public class CurrencyPairTests
	{
		/// <summary>
		/// Checks GetBitfinexCurrencyPair() functions as expected
		/// </summary>
		[TestMethod()]
		public void TradingLib_CurrencyPair_GetBitfinexCurrencyPairTest()
		{
			CurrencyPair cp = new CurrencyPair(0, 1); // Creates the currency pair USD/BTC
			Assert.AreEqual(cp.GetBitfinexCurrencyPair(), "tBTCUSD"); // Gets the Bitfinex currency pair and checks it returns 'tBTCUSD'
		}

		/// <summary>
		/// Checks GetBitfinexCurrencyPair() throws an UnknownCurrencyPairException when given a currency pair it doesn't understand.
		/// </summary>
		[TestMethod()]
		[ExpectedException(typeof(UnknownCurrencyPairException), "Currency pair (1, 1) unknown.")]
		public void TradingLib_CurrencyPair_ThrowsCurrencyPairException()
		{
			CurrencyPair cp = new CurrencyPair(1, 1); // Creates the unknown currency pair USD/USD
			cp.GetBitfinexCurrencyPair(); // Gets the Bitfinex currency pair and checks it throws an exception
		}
	}
}