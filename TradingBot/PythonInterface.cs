using System;
using System.Collections.Generic;
using IronPython;
using IronPython.Hosting;
using IronPython.Modules;
using Microsoft.Scripting.Hosting;
using Python.Runtime;

namespace TradingBot
{
	internal class PythonInterface
	{
		public PythonInterface()
		{
			using (Py.GIL())
			{
				dynamic NeuralNetworkInterface = Py.Import("NeuralNetwork");
				Console.WriteLine(NeuralNetworkInterface.CSharpInterface.returnHello("Matt"));
			}
		}

		// Yet to be returning a double, having a double[,] 
		public void TrainNetwork()
		{
			//Console.WriteLine(PythonFile.MethodCall("returnHello", "Matt"));
		}
	}
}