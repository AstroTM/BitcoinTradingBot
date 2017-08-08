using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TradingLib;
using TensorFlow;

namespace TradingBot
{
	class Program
	{
		private static ApiReader APR;
		private static DatabaseConnector DBC;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			APR = new ApiReader(); // Initialises ApiReader
			DBC = new DatabaseConnector(); // Initialises DatabaseConnector

			using (var graph = new TFGraph())
			{
				List<DatabaseRow> inputData = DBC.SelectAllFromDatabase();

				var one = graph.Const(1);

				var time = graph.Placeholder(TFDataType.Int32, TFShape.Scalar, "time"); // Input value for time of database row
				var price = graph.Placeholder(TFDataType.Double, TFShape.Scalar, "price"); // Input value for ETHBTC price
				var volBid = graph.Placeholder(TFDataType.Double, TFShape.Scalar, "volBid"); // Input value for volume of ETH bought
				var volAsk = graph.Placeholder(TFDataType.Double, TFShape.Scalar, "volAsk"); // Input value for volume of ETH sold

				// These will be the proportion of ETH to BTC: 1 means all ETH and no BTC, 0 means all BTC and no ETH
				var ethToBTCHoldings = graph.Variable(new TFOutput(), "ethToBtc"); // Ammount of ETH have as a fraction of 1
			}
		}
	}
}
