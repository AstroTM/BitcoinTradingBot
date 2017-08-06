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
	public class ApiReaderTests
	{
		ApiReader APC = new ApiReader();

		[TestMethod()]
		public void getPriceTest()
		{
			TickerResult TR = APC.GetTickerResult(new CurrencyPair(0, 1));
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

		[TestMethod()]
		[ExpectedException(typeof(UnknownCurrencyPairException), "Currency pair (1, 1) unknown.")]
		public void throwsCurrencyPairException()
		{
			CurrencyPair cp = new CurrencyPair(1, 1);
			cp.GetBitfinexCurrencyPair();
		}

		[TestMethod()]
		public void downloadStringTest()
		{
			Assert.AreEqual(ApiReader.DownloadString("https://example.com/"), 
				"<!doctype html>\n<html>\n<head>\n    <title>Example Domain</title>\n\n    <meta charset=\"utf-8\" />\n    <meta http-equiv=\"Content-type\" content=\"text/html; charset=utf-8\" />\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\n    <style type=\"text/css\">\n    body {\n        background-color: #f0f0f2;\n        margin: 0;\n        padding: 0;\n        font-family: \"Open Sans\", \"Helvetica Neue\", Helvetica, Arial, sans-serif;\n        \n    }\n    div {\n        width: 600px;\n        margin: 5em auto;\n        padding: 50px;\n        background-color: #fff;\n        border-radius: 1em;\n    }\n    a:link, a:visited {\n        color: #38488f;\n        text-decoration: none;\n    }\n    @media (max-width: 700px) {\n        body {\n            background-color: #fff;\n        }\n        div {\n            width: auto;\n            margin: 0 auto;\n            border-radius: 0;\n            padding: 1em;\n        }\n    }\n    </style>    \n</head>\n\n<body>\n<div>\n    <h1>Example Domain</h1>\n    <p>This domain is established to be used for illustrative examples in documents. You may use this\n    domain in examples without prior coordination or asking for permission.</p>\n    <p><a href=\"http://www.iana.org/domains/example\">More information...</a></p>\n</div>\n</body>\n</html>\n");
		}
	}
}