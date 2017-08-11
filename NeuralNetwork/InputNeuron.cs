namespace TradingBot
{
	internal class InputNeuron : Neuron
	{
		public double value;

		public InputNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}