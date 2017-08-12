using System;

namespace TradingBot
{
	public class HiddenNeuron : Neuron
	{
		public double value;

		public double[] InValue
		{
			get { return InValue; }
			set
			{
				InValue = value;
				double sumIn = 0;
				foreach (double val in InValue)
					sumIn += val;
				OutValue = Sigmoid(sumIn);
			}
		}

		public HiddenNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}

		public static double Sigmoid(double inValue)
		{
			return 1 / (1 + Math.Exp(-inValue));
		}
	}
}