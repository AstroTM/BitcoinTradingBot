using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceUpdater;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingLib;
using System.Data;

namespace PriceUpdater.Tests
{
	[TestClass()]
	public class PriceUpdaterTests
	{
		/// <summary>
		/// Checks TimerElapsed() functions correctly
		/// </summary>
		[TestMethod()]
		public void PriceUpdater_TimerElapsedTest()
		{
			int timeOfInsert = ApiReader.GetUnixTime();

			Program.APR = new ApiReader(); // Initialises ApiReader
			Program.DBC = new DatabaseConnector(); // Initialises DatabaseConnector

			Program.currency = new CurrencyPair(2, 1); // Creates new currency pair of ETH/BTC

			Program.TimerElapsed(null, null); // Runs TimerElapsed

			List<DatabaseRow> values = Program.DBC.SelectAllFromDatabase(); // Selects every row from the database

			bool hasBeenFound = false; // False until found

			foreach (DatabaseRow row in values) // Iterates through every row
			{
				if (Convert.ToInt32(row.date) == Program.LastInsert) // If the row is the values previously inserted
				{
					hasBeenFound = true; // Test is passed
					string sql = "DELETE FROM prices WHERE date=" + Program.LastInsert + ";"; // Create sql to delete the row
					SQLiteCommand command = new SQLiteCommand(sql, Program.DBC.connection); // Create the command
					command.ExecuteNonQuery(); // Execute the command
				}
			}

			Assert.AreEqual(timeOfInsert, Program.LastInsert);
			Assert.IsTrue(hasBeenFound);
		}
	}
}