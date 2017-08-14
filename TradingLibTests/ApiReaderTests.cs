using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingLib.Tests
{
	/// <summary>
	/// Tests class ApiReader
	/// </summary>
	[TestClass()]
	public class ApiReaderTests
	{
		ApiReader APR = new ApiReader(); // Initialise ApiReader for tests

		/// <summary>
		/// Checks GetPrice() returns a TickerResult with values
		/// </summary>
		[TestMethod()]
		public void TradingLib_ApiReader_GetPriceTest()
		{
			TickerResult TR = APR.GetTickerResult(new CurrencyPair(0, 1)); // Get result for currency pair USD/BTC

			// Check each variable in TickerResult has a value
			Assert.AreNotEqual(TR.ask, null);
			Assert.AreNotEqual(TR.askSize, null);
			Assert.AreNotEqual(TR.bid, null);
			Assert.AreNotEqual(TR.bidSize, null);
			Assert.AreNotEqual(TR.dailyChange, null);
			Assert.AreNotEqual(TR.dailyChangePerc, null);
			Assert.AreNotEqual(TR.lastPrice, null);
			Assert.AreNotEqual(TR.volume, null);
			Assert.AreNotEqual(TR.high, null);
			Assert.AreNotEqual(TR.low, null);
		}

		/// <summary>
		/// Checks GetHistory() returns a TradeHistoryResult with individual values equal to the sumBid and sumAsk values.
		/// </summary>
		[TestMethod()]
		public void TradingLib_ApiReader_GetHistoryTest()
		{
			TradeHistoryResult THR = APR.GetTradeHistoryResult(new CurrencyPair(0, 1)); // Get result for currency pair USD/BTC

			double sumBid = 0;
			double sumAsk = 0;

			foreach (HistoricalTrade trade in THR.trades) // Iterate through each trade
			{
				if (trade.IsBid) // If it's a bid
				{
					sumBid += trade.Amount; // Add the amount to sumBid
				}
				else // Else it's an ask
				{
					sumAsk += trade.Amount; // Add the amount to sumAsk
				}
			}

			Assert.AreEqual(THR.sumAsk(), sumAsk); // Check calculated ask is the same as sumAsk
			Assert.AreEqual(THR.sumBid(), sumBid); // Check calculated bid is the same as sumBid
		}

		/// <summary>
		/// Checks DownloadString functions correctly by downloading https://example.com/
		/// </summary>
		[TestMethod()]
		public void TradingLib_ApiReader_DownloadStringTest()
		{
			Assert.AreEqual(ApiReader.DownloadString("https://example.com/"),
				"<!doctype html>\n<html>\n<head>\n    <title>Example Domain</title>\n\n    <meta charset=\"utf-8\" />\n    <meta http-equiv=\"Content-type\" content=\"text/html; charset=utf-8\" />\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\n    <style type=\"text/css\">\n    body {\n        background-color: #f0f0f2;\n        margin: 0;\n        padding: 0;\n        font-family: \"Open Sans\", \"Helvetica Neue\", Helvetica, Arial, sans-serif;\n        \n    }\n    div {\n        width: 600px;\n        margin: 5em auto;\n        padding: 50px;\n        background-color: #fff;\n        border-radius: 1em;\n    }\n    a:link, a:visited {\n        color: #38488f;\n        text-decoration: none;\n    }\n    @media (max-width: 700px) {\n        body {\n            background-color: #fff;\n        }\n        div {\n            width: auto;\n            margin: 0 auto;\n            border-radius: 0;\n            padding: 1em;\n        }\n    }\n    </style>    \n</head>\n\n<body>\n<div>\n    <h1>Example Domain</h1>\n    <p>This domain is established to be used for illustrative examples in documents. You may use this\n    domain in examples without prior coordination or asking for permission.</p>\n    <p><a href=\"http://www.iana.org/domains/example\">More information...</a></p>\n</div>\n</body>\n</html>\n");
		}
	}
}