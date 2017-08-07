using System.Collections.Generic;
using System.Reflection;

namespace TradingLib
{
	public class TradeHistoryResult
	{
		public List<HistoricalTrade> trades = new List<HistoricalTrade>();

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