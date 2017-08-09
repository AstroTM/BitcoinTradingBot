using System;
using System.Collections.Generic;
using IronPython;
using IronPython.Hosting;
using IronPython.Modules;
using Microsoft.Scripting.Hosting;
using Python.Runtime;
using TradingLib;

namespace TradingBot
{
	internal class PythonInterface
	{
		public PythonInterface()
		{
		}

		// Yet to be returning a double, having a double[,] 
		public void TrainNetwork(List<DatabaseRow> inputAsDatabaseRows)
		{
			double[,] input = new double[4, inputAsDatabaseRows.Count/4];
			for (int i = 0; i < inputAsDatabaseRows.Count/4; i++)
			{
				input[0, i] = inputAsDatabaseRows[i].date;
				input[1, i] = inputAsDatabaseRows[i].price;
				input[2, i] = inputAsDatabaseRows[i].volAsk;
				input[3, i] = inputAsDatabaseRows[i].volBid;
			}

			using (Py.GIL())
			{
				dynamic NeuralNetworkInterface = Py.Import("NeuralNetwork");

				Console.WriteLine(NeuralNetworkInterface.CSharpInterface.TrainNetwork(input));
			}
		}
	}
}