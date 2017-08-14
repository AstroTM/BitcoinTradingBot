using System;
using System.Collections.Generic;
using System.Xml.XPath;
using TradingLib;

namespace TradingBot
{
	public class NeuralNetwork
	{
		public int InputLayerSize = 7;
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

			for (int i = 0; i < X.xTrain.Length / 8; i++)
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
					    if (n.Height == 5)
					        n.InValue = new double[] { X.xTrain[i, 4] };
					    if (n.Height == 6)
					        n.InValue = new double[] { X.xTrain[i, 5] };
					    if (n.Height == 7)
					        n.InValue = new double[] { X.xTrain[i, 6] };
                    }
				}

				double output = Forward();

			    Console.WriteLine(
                    "Input variables: {0:F20}, {1:F20}, {2:F20}, {3:F20}. Output: {4:F20}",
			        X.xTrain[i, 0], X.xTrain[i, 1], X.xTrain[i, 2], X.xTrain[i, 3], output);
			}

            Console.WriteLine(Cost(X));
		}

        // Calculates how accurate network currently is
	    double Cost(InputData X)
	    {
	        double cost = 0;

	        for (int i = 0; i < X.xTrain.Length / 8; i++)
	        {
	            double output = Forward(); // Propogate network

	            double e = Math.Abs(output - X.xTrain[i, 7]); // Difference between yHat and y

                cost += (e * e) / 2; // 1/2 * e^2
            }

	        for (int i = 0; i < X.xTest.Length / 8; i++)
	        {
	            double output = Forward(); // Propogate network

                double e = Math.Abs(output - X.xTest[i, 7]); // Difference between yHat and y

                cost += (e * e) / 2; // 1/2 * e^2
	        }

	        return cost;
	    }

        // Gradient of a sigmoid
	    double SigmoidPrime(double z)
	    {
	        double exp = Math.Exp(-z);
	        double tmp = Math.Pow((1 + exp), 2);
	        return exp / tmp;
	    }

	    public static T[] GetRow<T>(T[,] matrix, int row)
	    {
	        var columns = matrix.GetLength(1);
	        var array = new T[columns];
	        for (int i = 0; i < columns; ++i)
	            array[i] = matrix[row, i];
	        return array;
	    }

        double Forward()
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

		    throw new Exception("Output neuron cannot be found.");
		}

	    double Forward(double[] input)
	    {
	        double[] saveData = new double[7];

            // Set neurons to input data
	        foreach (Neuron n in Neurons)
	        {
	            if (n.Layer == 1)
	            {
	                if (n.Height == 1)
	                    n.InValue = new double[] { input[0] };
	                if (n.Height == 2)
	                    n.InValue = new double[] { input[1] };
	                if (n.Height == 3)
	                    n.InValue = new double[] { input[2] };
	                if (n.Height == 4)
	                    n.InValue = new double[] { input[3] };
	                if (n.Height == 5)
	                    n.InValue = new double[] { input[4] };
	                if (n.Height == 6)
	                    n.InValue = new double[] { input[5] };
	                if (n.Height == 7)
	                    n.InValue = new double[] { input[6] };
	            }
	        }

	        Forward();
	        double output = 0;
	        foreach (Neuron n in Neurons)
	        {
	            if (n.Layer == 3)
	            {
	                n.Propogate();
	                output = n.OutValue;
	            }
	        }

	        return output;
	    }
    }
}