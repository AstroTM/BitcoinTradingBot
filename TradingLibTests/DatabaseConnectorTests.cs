using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingLib;
using System;
using System.Collections.Generic;
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
	}
}