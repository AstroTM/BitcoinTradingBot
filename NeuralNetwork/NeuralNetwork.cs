using System;
using System.Collections.Generic;
using System.Xml.XPath;
using TradingLib;

namespace TradingBot
{
	public class NeuralNetwork
	{
		public NeuralNetwork()
		{
		}

		public void TrainNetwork(List<DatabaseRow> inputAsDatabaseRows)
		{
			InputData X = new InputData(inputAsDatabaseRows);


		}

		public static dynamic Softmax(double[] z)
		{
			double[] zExp = new double[z.Length];

			for (int i = 0; i < zExp.Length; i++)
			{
				zExp[i] = Math.Exp(z[i]);
			}

			double sumZExp = 0;

			for (int i = 0; i < zExp.Length; i++)
			{
				sumZExp += zExp[i];
			}

			double[] softmax = new double[z.Length];

			for (int i = 0; i < zExp.Length; i++)
			{
				softmax[i] = zExp[i] / sumZExp;
			}

			return softmax;
		}
	}
}