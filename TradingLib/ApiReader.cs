using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TradingLib
{
	/// <summary>
	/// Allows readonly operations to be carried out on the Bitfinex API.
	/// </summary>
	public class ApiReader
	{
		public ApiReader()
		{
			// Empty for now
		}

		/// <summary>
		/// Gets ticker info for currency pair from the API.
		/// </summary>
		/// <param name="currency">Currency pair to get data about</param>
		/// <returns>TickerResult containing ticker data for specified currency on Bitfinex</returns>
		public TickerResult GetTickerResult(CurrencyPair currency)
		{
			string rawData = DownloadString("https://api.bitfinex.com/v2/ticker/" + currency.GetBitfinexCurrencyPair());

			double[] array = JsonConvert.DeserializeObject<double[]>(rawData);

			TickerResult result = TickerArrayToTickerResult(array);

			return result;
		}

		/// <summary>
		/// Gets ticker info for currency pair from the API.
		/// </summary>
		/// <param name="currency">Currency pair to get data about</param>
		/// <returns>TradeHistoryResult containing last 120(?) trades</returns>
		public TradeHistoryResult GetTradeHistoryResult(CurrencyPair currency)
		{
			string rawData = DownloadString("https://api.bitfinex.com/v2/trades/" + currency.GetBitfinexCurrencyPair() +
			                                "/hist");

			double[,] array = JsonConvert.DeserializeObject<double[,]>(rawData);

			TradeHistoryResult result = TickerHistoryArrayToTradeHistoryResult(array);

			return result;
		}

		TradeHistoryResult TickerHistoryArrayToTradeHistoryResult(double[,] input)
		{
			TradeHistoryResult THR = new TradeHistoryResult();

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


		/// <summary>
		/// Downloads a web page as a string
		/// </summary>
		/// <param name="address">Address to get data from</param>
		/// <returns>Web page as string</returns>
		public static string DownloadString(string address)
		{
			WebClient client = new WebClient();
			string reply = client.DownloadString(address);

			return reply;
		}
	}
}