using System;
using System.Collections.Generic;
using System.Xml.XPath;
using TradingLib;

namespace TradingBot
{
	public class NeuralNetwork
	{
		public int InputLayerSize = 4;
		public int HiddenLayerSize = 16;
		public int OutputLayerSize = 1;
		public List<Synapse> synapses;

		public NeuralNetwork()
		{
			synapses = new List<Synapse>();

			// Create first layer synapses
			for (int i = 1; i <= InputLayerSize; i++)
			{
				for (int j = 1; j <= HiddenLayerSize; j++)
				{
					synapses.Add(new Synapse(1, i, j));
				}
			}

			// Create second layer synapses
			for (int i = 1; i <= HiddenLayerSize; i++)
			{
				for (int j = 1; j <= OutputLayerSize; j++)
				{
					synapses.Add(new Synapse(2, i, j));
				}
			}
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