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

				var ETHUSD = graph.Const(APR.GetTickerResult(new CurrencyPair(0, 2)).lastPrice); // Price of ETH in USD at time of creating graph.
				var BTCUSD = graph.Const(APR.GetTickerResult(new CurrencyPair(0, 1)).lastPrice); // Price of BTC in USD at time of creating graph.

				var time = graph.Placeholder(TFDataType.Int32, TFShape.Scalar, "time"); // Input value for time of database row
				var price = graph.Placeholder(TFDataType.Double, TFShape.Scalar, "price"); // Input value for ETHBTC price
				var volBid = graph.Placeholder(TFDataType.Double, TFShape.Scalar, "volBid"); // Input value for volume of ETH bought
				var volAsk = graph.Placeholder(TFDataType.Double, TFShape.Scalar, "volAsk"); // Input value for volume of ETH sold

				var ETHBalance = graph.Placeholder(TFDataType.Int32, TFShape.Scalar, "ETHBalance"); // Initial Ethereum balance
				var BTCBalance = graph.Placeholder(TFDataType.Int32, TFShape.Scalar, "BTCBalance"); // Initial Bitcoin balance

				var bidValue = graph.Variable(new TFOutput(), "bid"); // Ammount of ETH to buy as a fraction of the 
				var askValue = graph.Variable(new TFOutput(), "ask"); // Ammount of to buy as a fraction of 1

				var remainingETH = graph.Mul(graph.Sub(one, bidValue), ETHBalance); // Ammount of ETH staying as ETH
				var boughtETH = graph.Div(graph.Mul(askValue, BTCBalance), price); // Ammount of BTC turned into ETH

				var finalETH = graph.Add(boughtETH, remainingETH); // Final Ethereum balance after transaction

				var remainingBTC = graph.Mul(graph.Sub(one, bidValue), BTCBalance); // Ammount of BTC staying as BTC
				var boughtBTC = graph.Div(graph.Mul(askValue, ETHBalance), price); // Ammount of ETH turned into BTC

				var finalBTC = graph.Add(boughtBTC, remainingBTC); // Final Bitcoin balance after transaction
			}
		}
	}
}
