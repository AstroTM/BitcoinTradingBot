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
		public void SelectAllFromDatabaseTest()
		{
			List<DatabaseRow> Rows = DBC.SelectAllFromDatabase(); // Selects every row from the database

			// Checks the first one is as expected
			Assert.AreEqual(Rows[0].date, 1502316284);
			Assert.AreEqual(Rows[0].price, 0.090076);
			Assert.AreEqual(Rows[0].volBid, 10.6998);
			Assert.AreEqual(Rows[0].volAsk, 18.175939);
		}

		/// <summary>
		/// Check values can be correctly inserted, selected and removed from the database.
		/// </summary>
		[TestMethod()]
		public void InsertSelectRemoveTest()
		{
			DBC.InsertIntoDatabase(new DatabaseRow(999, 9999, 99999, 999999)); // Inserts unlikely values into the database
			List<DatabaseRow> values = DBC.SelectAllFromDatabase(); // Selects every row from the database

			bool hasBeenFound = false; // False until found

			foreach (DatabaseRow row in values) // Iterates through every row
			{
				if (row.date == 999 & row.price == 9999 & row.volBid == 99999 & row.volAsk == 999999) // If the row is the values previously inserted
				{
					hasBeenFound = true; // Test is passed
					string sql = "DELETE FROM prices WHERE date=999 AND price=9999 AND v_bid=99999 AND v_ask=999999;"; // Create sql to delete the row
					SQLiteCommand command = new SQLiteCommand(sql, DBC.connection); // Create the command
					command.ExecuteNonQuery(); // Execute the command
				}
			}
			
			Assert.IsTrue(hasBeenFound);
		}
	}
}