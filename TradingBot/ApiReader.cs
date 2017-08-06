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

			double[] array = JsonConvert.DeserializeObject<double[]>(rawData);

			TickerResult result = TickerArrayToTickerResult(array);

			return result;
		}

		TickerResult TickerArrayToTickerResult(double[] input)
		{
			TickerResult result = new TickerResult();

			result.bid = input[0];
			result.bidSize = input[1];
			result.ask = input[2];
			result.askSize = input[3];
			result.dailyChange = input[4];
			result.dailyChangePerc = input[5];
			result.lastPrice = input[6];
			result.volume = input[7];
			result.high = input[8];
			result.low = input[9];

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