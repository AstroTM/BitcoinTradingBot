using System;

namespace TradingBot
{
	public class Neuron
	{
		public int Layer;
		public int Height;

		public double InValue;
		public double OutValue;

		public Neuron(int layer, int height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}