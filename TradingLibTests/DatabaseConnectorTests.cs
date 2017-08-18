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
		    Assert.AreEqual(Rows[0].data[0], 1502654395.3391023);
            Assert.AreEqual(Rows[0].data[1], 0.073081);
		    Assert.AreEqual(Rows[0].data[2], 19.904977629999998);
		    Assert.AreEqual(Rows[0].data[3], 0.5108609);
		    Assert.AreEqual(Rows[0].data[4], -92.6919);
		    Assert.AreEqual(Rows[0].data[5], 0.072855);
		    Assert.AreEqual(Rows[0].data[6], 17.93705217);
        }

		/// <summary>
		/// Check values can be correctly inserted, selected and removed from the database.
		/// </summary>
		[TestMethod()]
		public void TradingLib_DatabaseConnector_InsertSelectRemoveTest()
		{
		    DatabaseRow expectedDatabaseRow = new DatabaseRow(1, 2, 3, 4, 5, 6, 7);

            DBC.InsertIntoDatabase(expectedDatabaseRow); // Inserts unlikely values into the database
			List<DatabaseRow> values = DBC.SelectAllFromDatabase(); // Selects every row from the database

			bool hasBeenFound = false; // False until found

		    string sql = "DELETE FROM prices WHERE " +
		                 "date=1 AND " +
		                 "price=2 AND " +
		                 "bid_v=3 AND " +
		                 "ask_v=4 AND " +
		                 "change_perc=5 AND " +
		                 "high=6 AND " +
		                 "low=7;"; // Create sql to delete the row

            foreach (DatabaseRow row in values) // Iterates through every row
			{
				if (row.data[0] == 1 &
				    row.data[1] == 2 &
				    row.data[2] == 3 &
				    row.data[3] == 4 &
				    row.data[4] == 5 &
				    row.data[5] == 6 &
				    row.data[6] == 7) // If the row is the values previously inserted
				{
					hasBeenFound = true; // Test is passed
					SQLiteCommand command = new SQLiteCommand(sql, DBC.connection); // Create the command
					command.ExecuteNonQuery(); // Execute the command
				}
			}
			
			Assert.IsTrue(hasBeenFound);
		}
	}
}