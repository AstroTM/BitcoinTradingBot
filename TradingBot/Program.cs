using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TradingLib;
using Microsoft.Scripting.Hosting;

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

			NeuralNet ANN = new NeuralNet();

			ANN.TrainNetwork(DBC.SelectAllFromDatabase());
		}
	}
}
