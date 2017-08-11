using System;

namespace TradingBot
{
	public class HiddenNeuron : Neuron
	{
		public double value;

		public HiddenNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}

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