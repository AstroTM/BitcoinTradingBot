using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TradingLib
{
	/// <summary>
	/// Connects to the SqLite database and allows operations to be carried out on it.
	/// </summary>
	public class DatabaseConnector
	{
		/// <summary>
		/// SQLite connection that all operations are carried out on.
		/// </summary>
		public SQLiteConnection connection = new SQLiteConnection(
			@"Data Source=C:\Users\matth\OneDrive\Documents\Visual Studio 2017\Projects\TradingBot\priceData.db;version=3;new=False;datetimeformat=CurrentCulture");

		public DatabaseConnector()
		{
			connection.Open(); // Opens connection to database
		}

		/// <summary>
		/// Inserts a row into price database
		/// </summary>
		/// <param name="input">Row to be inserted</param>
		public void InsertIntoDatabase(DatabaseRow input)
		{
			string sql = "INSERT INTO prices VALUES (" + // Creates sql string to be executed on the database
				Convert.ToString(input.date) + ", " + 
				Convert.ToString(input.price) + ", " + 
				Convert.ToString(input.volBid) + ", " + 
				Convert.ToString(input.volAsk) + ");";

			SQLiteCommand command = new SQLiteCommand(sql, connection); // Creates the command
			command.ExecuteNonQuery(); // Executes the command
		}

		/// <summary>
		/// Selects every row from the price database
		/// </summary>
		/// <returns>List of every row in the database</returns>
		public List<DatabaseRow> SelectAllFromDatabase()
		{
			List<DatabaseRow> output = new List<DatabaseRow>(); // Creates empty list to hold outputs

			string sql = "SELECT * FROM prices;"; // Creates sql string to be executed on the database

			SQLiteCommand command = new SQLiteCommand(sql, connection); // Creates the command

			SQLiteDataReader reader = command.ExecuteReader(); // Executes the command as a reader

			while (reader.Read()) // For every row in the result
			{
				output.Add(new DatabaseRow(
				    reader.GetDouble(0),
				    reader.GetDouble(1),
				    reader.GetDouble(2),
				    reader.GetDouble(3),
				    reader.GetDouble(4),
				    reader.GetDouble(5),
				    reader.GetDouble(6),
				    reader.GetDouble(7),
				    reader.GetDouble(8),
				    reader.GetDouble(9),
				    reader.GetDouble(10),
				    reader.GetDouble(11))); // Add a row to the output list
			}

			return output;
		}

		/// <summary>
		/// Closes the connection to the database
		/// </summary>
		public void CloseConnection()
		{
			connection.Close(); // Close the connection
		}
	}
}