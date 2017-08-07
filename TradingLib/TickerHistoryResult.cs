using System.Collections.Generic;
using System.Reflection;

namespace TradingLib
{
	/// <summary>
	/// Holds a list of historical trades from the API
	/// </summary>
	public class TradeHistoryResult
	{
		public List<HistoricalTrade> trades = new List<HistoricalTrade>();

		/// <summary>
		/// Gets the sum of all asks in this instance
		/// </summary>
		/// <returns>Sum of all asks</returns>
		public double sumAsk()
		{
			double output = 0;

			foreach (HistoricalTrade trade in trades)
			{
				if (!trade.IsBid)
					output += trade.Amount;
			}

			return output;
		}

		/// <summary>
		/// Gets the sum of all bids in this instance
		/// </summary>
		/// <returns>Sum of all bids</returns>
		public double sumBid()
		{
			double output = 0;

			foreach (HistoricalTrade trade in trades)
			{
				if (trade.IsBid)
					output += trade.Amount;
			}

			return output;
		}
	}
}