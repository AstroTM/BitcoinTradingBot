namespace TradingBot
{
	public class InputNeuron : Neuron
	{
		private double[] inValue;
		public override double[] InValue
		{
			get
			{
				return inValue;
			}
			set
			{
				inValue = value;
				OutValue = value[0];
			}
		}

		public InputNeuron(int layer, int height) : base(layer, height)
		{
			this.Layer = layer;
			this.Height = height;
		}
	}
}