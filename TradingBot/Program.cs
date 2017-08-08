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

				var bidValue = graph.Variable(new TFOutput(), "bid"); // Ammount of ETH to buy as a fraction of 1
				var askValue = graph.Variable(new TFOutput(), "ask"); // Ammount of to buy as a fraction of 1
			}
		}
	}
}
