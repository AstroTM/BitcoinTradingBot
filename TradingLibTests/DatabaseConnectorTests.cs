using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingLib;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingLib.Tests
{
	/// <summary>
	/// Tests class DatabaseConnector
	/// </summary>
	[TestClass()]
	public class DatabaseConnectorTests
	{
		DatabaseConnector DBC = new DatabaseConnector(); // Creates new DatabaseConnector for testing on

		/// <summary>
		/// Check selecting the first row from the database yields the correct result
		/// </summary>
		[TestMethod()]
		public void TradingLib_DatabaseConnector_FirstDatabaseSelect()
		{
			List<DatabaseRow> Rows = DBC.SelectAllFromDatabase(); // Selects every row from the database

            // Checks the first one is as expected
		    Assert.AreEqual(Rows[0].data[0], 1502645719.2494643);
		    Assert.AreEqual(Rows[0].data[1], 0.073035);
		    Assert.AreEqual(Rows[0].data[2], 42.059763350000004);
		    Assert.AreEqual(Rows[0].data[3], 22.40682312);
		    Assert.AreEqual(Rows[0].data[4], 0.073016);
		    Assert.AreEqual(Rows[0].data[5], 9.95207781);
		    Assert.AreEqual(Rows[0].data[6], 0.073254);
		    Assert.AreEqual(Rows[0].data[7], 1);
		    Assert.AreEqual(Rows[0].data[8], 1);
		    Assert.AreEqual(Rows[0].data[9], 1);
		    Assert.AreEqual(Rows[0].data[10], 1);
        }

		/// <summary>
		/// Check values can be correctly inserted, selected and removed from the database.
		/// </summary>
		[TestMethod()]
		public void TradingLib_DatabaseConnector_InsertSelectRemoveTest()
		{
		    DatabaseRow expectedDatabaseRow = new DatabaseRow(
		        1, 2, 3, 4, 5, 6, 7);

            DBC.InsertIntoDatabase(expectedDatabaseRow); // Inserts unlikely values into the database
			List<DatabaseRow> values = DBC.SelectAllFromDatabase(); // Selects every row from the database

			bool hasBeenFound = false; // False until found

			foreach (DatabaseRow row in values) // Iterates through every row
			{
				if (row == expectedDatabaseRow) // If the row is the values previously inserted
				{
					hasBeenFound = true; // Test is passed
				    string sql = "DELETE FROM prices WHERE " +
				                 "date=1 AND " +
				                 "price=2 AND " +
				                 "bid=3 AND " +
				                 "bid_size=4 AND " +
				                 "bid_v=5 AND " +
				                 "ask=6 AND " +
				                 "ask_size=7 AND " +
				                 "ask_v=8 AND " +
				                 "change_perc=9 AND " +
				                 "high=10 AND " +
				                 "low=11"; // Create sql to delete the row
					SQLiteCommand command = new SQLiteCommand(sql, DBC.connection); // Create the command
					command.ExecuteNonQuery(); // Execute the command
				}
			}
			
			Assert.IsTrue(hasBeenFound);
		}
	}
}