using System;
using System.Linq;

namespace TradingBot
{
	public class OutputNeuron : Neuron
	{
	    public override double[] InValue
	    {
	        get
	        {
	            return inValue;
	        }
            set
			{
				inValue = value;
			}
		}
		private double[] inValue;

		public OutputNeuron(int layer, int height, int inputNeurons) : base(layer, height, inputNeurons)
		{
			this.Layer = layer;
			this.Height = height;
            inValue = new double[inputNeurons];
        }

	    public override void Propogate()
	    {
	        double sumIn = 0;
	        foreach (double val in InValue)
	            sumIn += val;
	        OutValue = Sigmoid(sumIn);
        }

		public static double Sigmoid(double inValue)
		{
			return 1 / (1 + Math.Exp(-inValue));
		}

		// Currently not used: If anyone knows how to use this for the last layer activation, please help.
		public static double[] Softmax(double[] z)
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