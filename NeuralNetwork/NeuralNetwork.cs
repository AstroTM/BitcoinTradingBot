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

		public List<Synapse> Synapses;
		public List<Neuron> Neurons;

		public NeuralNetwork()
		{
			Synapses = new List<Synapse>();
			Neurons = new List<Neuron>();

			for (int i = 1; i < InputLayerSize; i++)
				Neurons.Add(new InputNeuron(1, i));
			for (int i = 1; i < HiddenLayerSize; i++)
				Neurons.Add(new HiddenNeuron(1, i));
			for (int i = 1; i < OutputLayerSize; i++)
				Neurons.Add(new OutputNeuron(1, i));

			// Create first layer synapses
			for (int i = 1; i <= InputLayerSize; i++)
				for (int j = 1; j <= HiddenLayerSize; j++)
					Synapses.Add(new Synapse(1, i, j));
			// Create second layer synapses
			for (int i = 1; i <= HiddenLayerSize; i++)
				for (int j = 1; j <= OutputLayerSize; j++)
					Synapses.Add(new Synapse(2, i, j));
		}

		public void TrainNetwork(List<DatabaseRow> inputAsDatabaseRows)
		{
			InputData X = new InputData(inputAsDatabaseRows);
		}

		void Forward() // Will be double
		{
			foreach (Neuron n in Neurons)
			{
				if (n.Layer == 1) // If it's in the input layer
				{
					foreach (Synapse s in Synapses)
					{
						if (s.Layer == 1 && s.InNeuron == n.Height) // If it's Neuron N
						{
							s.InValue = n.OutValue;
						}
					}
				}
			}
		}
	}
}