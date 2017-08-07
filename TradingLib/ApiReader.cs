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
			string rawData = DownloadString("https://api.bitfinex.com/v2/ticker/" + currency.GetBitfinexCurrencyPair()); // Downloads raw data from API

			double[] array = JsonConvert.DeserializeObject<double[]>(rawData); // Converts raw data to double[]

			TickerResult result = TickerArrayToTickerResult(array); // Converts array to TickerResult

			return result;
		}

		/// <summary>
		/// Gets ticker info for currency pair from the API.
		/// </summary>
		/// <param name="currency">Currency pair to get data about</param>
		/// <returns>TradeHistoryResult containing last 120(?) trades</returns>
		public TradeHistoryResult GetTradeHistoryResult(CurrencyPair currency)
		{
			string rawData = DownloadString("https://api.bitfinex.com/v2/trades/" + 
				currency.GetBitfinexCurrencyPair() +
				"/hist"); // Downloads raw data from API

			double[,] array = JsonConvert.DeserializeObject<double[,]>(rawData); // Converts raw data to double[,]

			TradeHistoryResult result = TradeHistoryArrayToTradeHistoryResult(array); // Converts array to TradeHistoryResult

			return result;
		}

		/// <summary>
		/// Converts double[,] containing trade history to TradeHistoryResult object
		/// </summary>
		/// <param name="input">Input as a double[,]</param>
		/// <returns>TradeHistoryResult of input</returns>
		TradeHistoryResult TradeHistoryArrayToTradeHistoryResult(double[,] input)
		{
			TradeHistoryResult THR = new TradeHistoryResult(); // Creates blank TradeHistoryResult

			for (int i = 0; i < input.Length / 4; i++) // For each row in the input array
			{
				THR.trades.Add(new HistoricalTrade(
					Convert.ToUInt32(input[i, 0]), 
					Convert.ToUInt32(input[i, 1] / 1000), 
					input[i, 2], 
					input[i, 2])); // Append row's trade data to TradeHistoryResult
			}

			return THR;
		}
		/// <summary>
		/// Converts double[] containing ticker info to TickerResult object
		/// </summary>
		/// <param name="input">Input as a double[]</param>
		/// <returns>TickerResult of input</returns>
		TickerResult TickerArrayToTickerResult(double[] input)
		{
			TickerResult result = new TickerResult(); // Create a blank TickerResult

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
			WebClient client = new WebClient(); // Create WebClient

			// This line often takes a while as it involves connecting to the internet
			string reply = client.DownloadString(address); // Download data from address as string.

			return reply;
		}
	}
}