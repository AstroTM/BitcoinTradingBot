namespace TradingLib
{
	/// <summary>
	/// Holds the data of a single row of the database.
	/// </summary>
	public class DatabaseRow
    {
        public double date;
        public double price;
        public double bid;
        public double sizeBid;
        public double volBid;
        public double ask;
        public double sizeAsk;
        public double volAsk;
        public double percChange;
        public double high;
        public double low;

		public DatabaseRow(
		    double date,
		    double price,
		    double bid,
		    double sizeBid,
            double volBid,
		    double ask,
		    double sizeAsk,
		    double volAsk,
            double percChange,
	        double high,
	        double low)
		{
		    this.date = date;
		    this.price = price;
		    this.bid = bid;
		    this.sizeBid = sizeBid;
		    this.volBid = volBid;
		    this.ask = ask;
		    this.sizeAsk = sizeAsk;
		    this.volAsk = volAsk;
		    this.percChange = percChange;
		    this.high = high;
		    this.low = low;
		}
	}
}