namespace TradingBot
{
	internal class HiddenNeuron : Neuron
	{
		public double value;

		public HiddenNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}