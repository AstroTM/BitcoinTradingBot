using System.Collections.Generic;
using System.Reflection;

namespace TradingLib
{
	/// <summary>
	/// Holds a list of historical trades from the API
	/// </summary>
	public class TradeHistoryResult
	{   
		/// <summary>
		/// List holding all the HistoricalTrade objects
		/// </summary>
		public List<HistoricalTrade> trades = new List<HistoricalTrade>();

		/// <summary>
		/// Gets the sum of all asks in this instance
		/// </summary>
		/// <returns>Sum of all asks</returns>
		public double sumAsk()
		{
			double output = 0; // Initialise the output variable

			foreach (HistoricalTrade trade in trades) // Loop round each HistoricalTrade in the list
			{
				if (!trade.IsBid) // If it's an ask
					output += trade.Amount; // Append the trade value to the output
			}

			return output;
		}

		/// <summary>
		/// Gets the sum of all bids in this instance
		/// </summary>
		/// <returns>Sum of all bids</returns>
		public double sumBid()
		{
			double output = 0; // Initialise the output variable

			foreach (HistoricalTrade trade in trades) // Loop round each HistoricalTrade in the list
			{
				if (trade.IsBid) // If it's a bid
					output += trade.Amount; // Append the trade value to the output
			}

			return output;
		}
	}
}