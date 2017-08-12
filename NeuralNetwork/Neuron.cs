using System;

namespace TradingBot
{
	public abstract class Neuron
	{
		public int Layer;
		public int Height;

		public virtual double[] InValue { get; set; }
		public double OutValue;

		public Neuron(int layer, int height, int inputNeurons)
		{
			this.Layer = layer;
		    this.Height = height;
        }
	}
}