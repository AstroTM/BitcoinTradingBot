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

			for (int i = 1; i <= InputLayerSize; i++)
				Neurons.Add(new InputNeuron(1, i, 1));
			for (int i = 1; i <= HiddenLayerSize; i++)
				Neurons.Add(new HiddenNeuron(2, i, InputLayerSize));
			for (int i = 1; i <= OutputLayerSize; i++)
				Neurons.Add(new OutputNeuron(3, i, HiddenLayerSize));

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

				double output = Forward();

			    Console.WriteLine(
                    "Input variables: {0:F20}, {1:F20}, {2:F20}, {3:F20}. Output: {4:F20}",
			        X.xTrain[i, 0], X.xTrain[i, 1], X.xTrain[i, 2], X.xTrain[i, 3], output);
			}
		}

		double Forward() // Will be double
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
		        if (s.Layer == 1)
		        {
		            foreach (Neuron n in Neurons)
		            {
		                if (n.Layer == 2) // If it's in the hidden layer
		                {
		                    n.InValue[s.InNeuron - 1] = s.OutValue;
		                }
		            }
		        }
		    }

		    foreach (Neuron n in Neurons)
		    {
		        if (n.Layer == 2)
		            n.Propogate();
		    }
            #endregion

		    #region z3Propogate
		    foreach (Neuron n in Neurons)
		    {
		        if (n.Layer == 2) // If it's in the hidden layer
		        {
		            foreach (Synapse s in Synapses)
		            {
		                if (s.Layer == 2 && s.InNeuron == n.Height) // If it's Neuron N
		                {
		                    s.InValue = n.OutValue;
		                }
		            }
		        }
		    }
            #endregion

		    #region yHatPropogate
		    foreach (Synapse s in Synapses)
		    {
		        if (s.Layer == 2)
		        {
		            foreach (Neuron n in Neurons)
		            {
		                if (n.Layer == 3) // If it's in the hidden layer
		                {
		                    n.InValue[s.InNeuron - 1] = s.OutValue;
		                }
		            }
		        }
		    }
            #endregion

		    foreach (Neuron n in Neurons)
		    {
		        if (n.Layer == 3)
		        {
		            n.Propogate();
		            return n.OutValue;
		        }
		    }

		    return 0.00000000001;
		}
    }
}