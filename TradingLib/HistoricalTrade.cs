namespace TradingLib
{
	/// <summary>
	/// Holds the data of a single historical trade from the API.
	/// </summary>
	public class HistoricalTrade
	{
		public uint Id;
		/// <summary> Timestamp for the trade in seconds from the Epoch. </summary>
		public uint Time;
		public double Amount;
		public double Price;
		/// <summary> True if the trade is a bid, false if the trade is an ask. </summary>
		public bool IsBid;

		public HistoricalTrade(uint id, uint time, double amount, double price)
		{
			this.Id = id;
			this.Time = time;
			this.Price = price;
			if (amount < 0) // If negative
			{
				IsBid = false; // It's an ask so set IsBid to false
				this.Amount = -amount; // amount is negative, so -amount to make it positive
			}
			else // Therefore positive
			{
				IsBid = true; // It's a bid so set IsBid to true
				this.Amount = amount; // Set this.Amount
			}
		}
	}
}