using System;

namespace TradingBot
{
	public class Synapse
	{
		public int Layer;
		public int InNeuron;
		public int OutNeuron;

		public double Weight;

		private double inValue;
		public double InValue
		{
			get { return inValue; }
			set
			{
				inValue = value;
				OutValue = Weight * value;
			}
		}
		public double OutValue { get; private set; }

		public Synapse(int layer, int inNeuron, int outNeuron)
		{
			this.Layer = layer;
			this.InNeuron = inNeuron;
			this.OutNeuron = outNeuron;

			Random r = new Random(layer * inNeuron * outNeuron); // Makes it a bit more random...
			this.Weight = r.NextDouble(); // Random number between 0.0 and 1.0
		}
	}
}