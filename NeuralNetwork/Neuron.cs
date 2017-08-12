using System;

namespace TradingBot
{
	public abstract class Neuron
	{
		public int Layer;
		public int Height;

		public virtual double[] InValue { get; set; }
		public double OutValue;

		public Neuron(int layer, int height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}