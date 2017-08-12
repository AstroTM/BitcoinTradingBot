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

		public InputNeuron(int layer, int height, int inputNeurons) : base(layer, height, inputNeurons)
		{
			this.Layer = layer;
			this.Height = height;
		    inValue = new double[inputNeurons];
		}
	}
}