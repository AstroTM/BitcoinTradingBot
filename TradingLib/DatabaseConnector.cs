using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TradingLib
{
	public class DatabaseConnector
	{
		SQLiteConnection connection = new SQLiteConnection(
			@"Data Source=C:\Users\matth\OneDrive\Documents\Visual Studio 2017\Projects\TradingBot\priceData.db;version=3;new=False;datetimeformat=CurrentCulture");

		public DatabaseConnector()
		{
			connection.Open();
		}

		public void InsertIntoDatabase(DatabaseRow input)
		{
			string sql = "INSERT INTO prices VALUES (" + 
				Convert.ToString(input.date) + ", " + 
				Convert.ToString(input.price) + ", " + 
				Convert.ToString(input.volBid) + ", " + 
				Convert.ToString(input.volAsk) + ");";

			SQLiteCommand command = new SQLiteCommand(sql, connection);
			command.ExecuteNonQuery();
		}

		public List<DatabaseRow> SelectAllFromDatabase()
		{
			List<DatabaseRow> output = new List<DatabaseRow>();

			string sql = "SELECT * FROM prices;";

			SQLiteCommand command = new SQLiteCommand(sql, connection);

			SQLiteDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				double date = reader.GetDouble(0);
				double price = reader.GetDouble(1);
				double volBid = reader.GetDouble(2);
				double volAsk = reader.GetDouble(3);
				output.Add(new DatabaseRow(date, price, volBid, volAsk));
			}

			return output;
		}
	}
}