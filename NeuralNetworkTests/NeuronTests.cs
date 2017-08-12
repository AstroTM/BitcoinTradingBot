using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Tests
{
	[TestClass()]
	public class NeuronTests
	{
		[TestMethod()]
		public void SigmoidTest1()
		{
			Assert.AreEqual(HiddenNeuron.Sigmoid(0.458), 0.61253961344091512);
		}

		[TestMethod()]
		public void SoftmaxTest1()
		{
			double[] input = { 1.0, 2.0, 3.0, 4.0, 1.0, 2.0, 3.0 };
			double[] expectedOutput = { 0.024, 0.064, 0.175, 0.475, 0.024, 0.064, 0.175 };

			double[] output = HiddenNeuron.Softmax(input);

			for (int i = 0; i < output.Length; i++)
			{
				output[i] = Math.Round(output[i], 3);

				Assert.AreEqual(output[i], expectedOutput[i]);
			}
		}
	}
}