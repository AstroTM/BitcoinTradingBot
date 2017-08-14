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

	    public override void Propogate()
	    {
	        OutValue = InValue[0];
	    }

        public InputNeuron(int layer, int height, int inputSynapses) : base(layer, height, inputSynapses)
		{
			this.Layer = layer;
			this.Height = height;
		    inValue = new double[inputSynapses];
		}
	}
}