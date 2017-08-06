using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TradingBot
{
	public class ApiReader
	{
		public ApiReader()
		{
		}

		public TickerResult GetTickerResult(CurrencyPair currency)
		{
			string rawData = DownloadString("https://api.bitfinex.com/v2/ticker/" + currency.GetBitfinexCurrencyPair());

			TickerResult result = JsonConvert.DeserializeObject<TickerResult>(rawData);

			return result;
		}
		
		public static string DownloadString(string address)
		{
			WebClient client = new WebClient();
			string reply = client.DownloadString(address);

			return reply;
		}
	}
}