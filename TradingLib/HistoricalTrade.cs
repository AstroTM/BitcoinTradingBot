namespace TradingLib
{
	public class HistoricalTrade
	{
		public uint Id;
		public uint Mts;
		public double Amount;
		public double Price;
		public bool IsBid;

		public HistoricalTrade(uint id, uint mts, double amount, double price)
		{
			this.Id = id;
			this.Mts = mts;
			this.Price = price;
			if (amount < 0)
			{
				IsBid = false;
				this.Amount = -amount;
			}
			else
			{
				IsBid = true;
				this.Amount = amount;
			}
		}
	}
}