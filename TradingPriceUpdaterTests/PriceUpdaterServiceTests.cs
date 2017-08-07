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
			TimeSpan timeDifference = DateTime.UtcNow -
			                                 new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			int timeOfInsert = System.Convert.ToInt32(timeDifference.TotalSeconds);

			PUS.TimerElapsed(null, null); // Runs TimerElapsed

			List<DatabaseRow> values = PUS.DBC.SelectAllFromDatabase(); // Selects every row from the database

			bool hasBeenFound = false; // False until found

			foreach (DatabaseRow row in values) // Iterates through every row
			{
				if (row.date == PUS.LastInsert) // If the row is the values previously inserted
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