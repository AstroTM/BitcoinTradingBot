using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TradingLib;
using TensorFlow;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace TradingBot
{
	class Program
	{
		private static ApiReader APR;
		private static DatabaseConnector DBC;
		private static PythonInterface PI;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			APR = new ApiReader(); // Initialises ApiReader
			DBC = new DatabaseConnector(); // Initialises DatabaseConnector

			PI = new PythonInterface();

			PI.TrainNetwork();

			using (var graph = new TFGraph())
			{
				List<DatabaseRow> inputData = DBC.SelectAllFromDatabase();

				var one = graph.Const(1);

				// X: array with 4 values: time, price, volBid, volAsk.
				var X = graph.Placeholder(TFDataType.Int32, new TFShape(4), "X"); // Input value for a single database row

				// These will be the proportion of ETH to BTC: 1 means all ETH and no BTC, 0 means all BTC and no ETH
				var ethToBTCHoldings = graph.Variable(new TFOutput(), "ethToBtc"); // Ammount of ETH have as a fraction of 1
			}
		}
	}
}
