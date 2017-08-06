using System;
using System.Data.SQLite;

namespace TradingLib
{
	public class DatabaseConnector
	{
		SQLiteConnection connection = new SQLiteConnection(
			@"Data Source=C:\Users\matth\OneDrive\Documents\Visual Studio 2017\Projects\TradingBot\priceData.db;");

		public DatabaseConnector()
		{
			connection.Open();
		}

		public void InsertIntoDatabase(int date, double price, double volBid, double volAsk)
		{
			string sql = "INSERT INTO prices VALUES (" + 
				Convert.ToString(date) + ", " + 
				Convert.ToString(price) + ", " + 
				Convert.ToString(volBid) + ", " + 
				Convert.ToString(volAsk) + ");";

			SQLiteCommand command = new SQLiteCommand(sql, connection);
			command.ExecuteNonQuery();
		}
	}
}