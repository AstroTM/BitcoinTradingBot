using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingPriceUpdater;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingLib;

namespace TradingPriceUpdater.Tests
{
	/// <summary>
	/// Tests class PriceUpdaterService
	/// </summary>
	[TestClass()]
	public class PriceUpdaterServiceTests
	{
		PriceUpdaterService PUS = new PriceUpdaterService();

		/// <summary>
		/// Checks TimerElapsed() functions correctly
		/// </summary>
		[TestMethod()]
		public void TimerElapsedTest()
		{
			int timeOfInsert = ApiReader.GetUnixTime();

			PUS.APR = new ApiReader(); // Initialises ApiReader
			PUS.DBC = new DatabaseConnector(); // Initialises DatabaseConnector

			PUS.currency = new CurrencyPair(2, 1); // Creates new currency pair of ETH/BTC

			PUS.TimerElapsed(null, null); // Runs TimerElapsed

			List<DatabaseRow> values = PUS.DBC.SelectAllFromDatabase(); // Selects every row from the database

			bool hasBeenFound = false; // False until found

			foreach (DatabaseRow row in values) // Iterates through every row
			{
				if (Convert.ToInt32(row.date) == PUS.LastInsert) // If the row is the values previously inserted
				{
					hasBeenFound = true; // Test is passed
					string sql = "DELETE FROM prices WHERE date=" + PUS.LastInsert + ";"; // Create sql to delete the row
					SQLiteCommand command = new SQLiteCommand(sql, PUS.DBC.connection); // Create the command
					command.ExecuteNonQuery(); // Execute the command
				}
			}

			Assert.AreEqual(timeOfInsert, PUS.LastInsert);
			Assert.IsTrue(hasBeenFound);
		}
	}
}