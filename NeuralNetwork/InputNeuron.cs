namespace TradingBot
{
	internal class InputNeuron : Neuron
	{
		public double OutValue;

		public InputNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}