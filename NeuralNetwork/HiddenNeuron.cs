﻿using System;

namespace TradingBot
{
	public class HiddenNeuron : Neuron
	{
		public double value;

		public override double[] InValue
		{
			get { return inValue; }
			set
			{
				inValue = value;
				double sumIn = 0;
				foreach (double val in inValue)
					sumIn += val;
				OutValue = Sigmoid(sumIn);
			}
		}
		private double[] inValue;

		public HiddenNeuron(int layer, int height, int inputNeurons) : base(layer, height, inputNeurons)
		{
			this.Layer = layer;
			this.Height = height;
		    inValue = new double[inputNeurons];
        }

		public static double Sigmoid(double inValue)
		{
			return 1 / (1 + Math.Exp(-inValue));
		}
	}
}