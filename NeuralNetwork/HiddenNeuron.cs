using System;

namespace TradingBot
{
	public class HiddenNeuron : Neuron
	{
		public double value;

		public override double[] InValue
		{
			get { return inValue; }
			set
			{
				inValue = value;
				double sumIn = 0;
				foreach (double val in inValue)
					sumIn += val;
				OutValue = Sigmoid(sumIn);
			}
		}
		private double[] inValue;

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