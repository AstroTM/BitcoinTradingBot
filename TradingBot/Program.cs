using System;
using System.Collections.Generic;
using System.Linq;
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

				var time = graph.Placeholder(TFDataType.Int32, null, "time");
				var price = graph.Placeholder(TFDataType.Double, null, "price");
				var volBid = graph.Placeholder(TFDataType.Double, null, "volBid");
				var volAsk = graph.Placeholder(TFDataType.Double, null, "volAsk");
			}
		}
	}
}
