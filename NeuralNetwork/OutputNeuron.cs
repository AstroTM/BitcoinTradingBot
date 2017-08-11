namespace TradingBot
{
	internal class OutputNeuron : Neuron
	{
		public double value;

		public OutputNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}