using System.Collections.Generic;
using System.Reflection;

namespace TradingLib
{
	public class TickerHistoryResult
	{
		public List<HistoricalTrade> trades = new List<HistoricalTrade>();

		public double sumAsk()
		{
			throw new System.NotImplementedException();
		}

		public double sumBid()
		{
			throw new System.NotImplementedException();
		}
	}
}