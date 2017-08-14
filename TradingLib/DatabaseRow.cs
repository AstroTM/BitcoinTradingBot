namespace TradingLib
{
	/// <summary>
	/// Holds the data of a single row of the database.
	/// </summary>
	public class DatabaseRow
	{
	    public double[] data;

		public DatabaseRow(
		    double date,
		    double price,
		    double bid,
		    double ask,
            double percChange,
	        double high,
	        double low)
		{
            data = new double[7];
		    data[0] = date;
		    data[1] = price;
		    data[2] = bid;
		    data[3] = ask;
		    data[4] = percChange;
		    data[5] = high;
		    data[6] = low;
		}
	}
}