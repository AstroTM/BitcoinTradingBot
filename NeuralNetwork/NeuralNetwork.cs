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
				Neurons.Add(new InputNeuron(1, i, 1));
			for (int i = 1; i < HiddenLayerSize; i++)
				Neurons.Add(new HiddenNeuron(1, i, InputLayerSize));
			for (int i = 1; i < OutputLayerSize; i++)
				Neurons.Add(new OutputNeuron(1, i, HiddenLayerSize));

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

			for (int i = 0; i < X.xTrain.Length / 4; i++)
			{
				foreach (Neuron n in Neurons)
				{
					if (n.Layer == 1)
					{
						if (n.Height == 1)
							n.InValue = new double[] { X.xTrain[i, 0] };
						if (n.Height == 2)
							n.InValue = new double[] { X.xTrain[i, 1] };
						if (n.Height == 3)
							n.InValue = new double[] { X.xTrain[i, 2] };
						if (n.Height == 4)
							n.InValue = new double[] { X.xTrain[i, 3] };
					}
				}

				Forward();
			}
		}

		void Forward() // Will be double
		{
		    #region z2Propogate
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
            #endregion

		    #region a2Propogate
		    foreach (Synapse s in Synapses)
		    {
		        if (s.Layer == 2)
		        {
		            foreach (Neuron n in Neurons)
		            {
		                if (n.Layer == 2) // If it's in the hidden layer
		                {
		                    n.InValue[s.InNeuron] = s.OutValue;
		                }
		            }
		        }
		    }
		    #endregion
        }
    }
}