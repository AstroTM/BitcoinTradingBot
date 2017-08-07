using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TradingLib
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

		public TickerHistoryResult GetTickerHistoryResult(CurrencyPair currency)
		{
			string rawData = DownloadString("https://api.bitfinex.com/v2/trades/" + currency.GetBitfinexCurrencyPair() +
			                                "/hist");

			double[,] array = JsonConvert.DeserializeObject<double[,]>(rawData);

			TickerHistoryResult result = TickerHistoryArrayToTickerHistoryResult(array);

			return result;
		}

		TickerHistoryResult TickerHistoryArrayToTickerHistoryResult(double[,] input)
		{
			TickerHistoryResult THR = new TickerHistoryResult();

			for (int i = 0; i < input.Length/4; i++)
			{
				THR.trades.Add(new HistoricalTrade(Convert.ToUInt32(input[i, 0]), Convert.ToUInt32(input[i, 1] / 1000), input[i, 2], input[i, 2]));
			}

			return THR;
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