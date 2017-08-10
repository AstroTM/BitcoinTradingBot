using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TradingLib;

namespace PriceUpdater
{
	static class Program
	{
		private static Timer _timer;

		public static CurrencyPair currency;
		public static ApiReader APR;
		public static DatabaseConnector DBC;
		public static int LastInsert;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			APR = new ApiReader(); // Initialises ApiReader
			DBC = new DatabaseConnector(); // Initialises DatabaseConnector

			currency = new CurrencyPair(2, 1); // Creates new currency pair of ETH/BTC

			_timer = new Timer(10 * 1000); // Create timer to run every 10 seconds
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed); // Sets method to be run when the timer is elapsed
			_timer.Start(); // Starts the timer
		}

		/// <summary>
		/// Run every 10 seconds, gets data from the API and puts it into the database.
		/// </summary>
		public static void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			LastInsert = ApiReader.GetUnixTime();

			TickerResult ticker = APR.GetTickerResult(currency); // Gets the ticker for the specified currency
			TradeHistoryResult history = APR.GetTradeHistoryResult(currency); // Gets the trade history for the specified currency

			double volAsk = 0;
			double volBid = 0;

			foreach (HistoricalTrade trade in history.trades)
			{
				if (trade.IsBid)
					volBid += trade.Amount;
				else
					volAsk += trade.Amount;
			}

			DatabaseRow row = new DatabaseRow(LastInsert, ticker.lastPrice, volBid, volAsk);
			DBC.InsertIntoDatabase(row);
		}
	}
}
