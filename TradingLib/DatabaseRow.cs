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
            data = new double[11];
		    data[0] = date;
		    data[1] = price;
		    data[2] = bid;
		    data[5] = ask;
		    data[8] = percChange;
		    data[9] = high;
		    data[10] = low;
		}
	}
}