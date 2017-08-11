using System;

namespace TradingBot
{
	public class Synapse
	{
		public int layer;
		public int inNeuron;
		public int outNeuron;

		public double weight;

		public Synapse(int layer, int inNeuron, int outNeuron)
		{
			this.layer = layer;
			this.inNeuron = inNeuron;
			this.outNeuron = outNeuron;

			Random r = new Random(layer * inNeuron * outNeuron); // Makes it a bit more random...
			this.weight = r.NextDouble(); // Random number between 0.0 and 1.0
		}
	}
}