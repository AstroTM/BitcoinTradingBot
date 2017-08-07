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
	[TestClass()]
	public class DatabaseConnectorTests
	{
		DatabaseConnector DBC = new DatabaseConnector();

		[TestMethod()]
		public void SelectAllFromDatabaseTest()
		{
			List<DatabaseRow> Rows = DBC.SelectAllFromDatabase();
			Assert.AreEqual(Rows[0].date, 1501940449.3283525);
			Assert.AreEqual(Rows[0].price, 0.07585);
			Assert.AreEqual(Rows[0].volBid, 202.4462506);
			Assert.AreEqual(Rows[0].volAsk, 118.01180373000003);
		}

		[TestMethod()]
		public void InsertSelectRemoveTest()
		{
			DBC.InsertIntoDatabase(new DatabaseRow(999, 9999, 99999, 999999));
			List<DatabaseRow> values = DBC.SelectAllFromDatabase();

			bool hasBeenFound = false;
			foreach (DatabaseRow row in values)
			{
				if (row.date == 999 & row.price == 9999 & row.volBid == 99999 & row.volAsk == 999999)
				{
					hasBeenFound = true;
					string sql = "DELETE FROM prices WHERE date=999 AND price=9999 AND v_bid=99999 AND v_ask=999999;";
					SQLiteCommand command = new SQLiteCommand(sql, DBC.connection);
					command.ExecuteNonQuery();
				}
			}

			if (!hasBeenFound)
			{
				Assert.Fail();
			}
		}
	}
}