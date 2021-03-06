﻿namespace TradingLib
{
	/// <summary>
	/// Holds the data from a ticker call on the API
	/// </summary>
	public class TickerResult
	{
		public double bid;
		public double bidSize;
		public double ask;
		public double askSize;
		public double dailyChange;
		public double dailyChangePerc;
		public double lastPrice;
		public double volume;
		public double high;
		public double low;
	}
}