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
		public void NeuralNetwork_Neuron_InputNeuronIOTest()
		{
			InputNeuron IN = new InputNeuron(1, 1, 1);
			IN.InValue = new double[] { 0.010101 };
			Assert.AreEqual(IN.OutValue, 0.010101);
		}

		[TestMethod()]
		public void NeuralNetwork_Neuron_HiddenNeuronIOTest()
		{
			HiddenNeuron HN = new HiddenNeuron(1, 1, 3);
			HN.InValue = new double[] { 0.10101, 0.20202, 0.30303 };
			Assert.AreEqual(HN.OutValue, 0.64704151179442626);
		}

		[TestMethod()]
		public void NeuralNetwork_Neuron_OutputNeuronIOTest()
		{
			OutputNeuron ON = new OutputNeuron(1, 1, 3);
			ON.InValue = new double[] { 0.20202, 0.30303, 0.40404 };
			Assert.AreEqual(ON.OutValue, 0.71281391251731641);
		}

		[TestMethod()]
		public void NeuralNetwork_Neuron_SigmoidTest1()
		{
			Assert.AreEqual(HiddenNeuron.Sigmoid(0.458), 0.61253961344091512);
		}

		[TestMethod()]
		public void NeuralNetwork_Neuron_SoftmaxTest1()
		{
			double[] input = { 1.0, 2.0, 3.0, 4.0, 1.0, 2.0, 3.0 };
			double[] expectedOutput = { 0.024, 0.064, 0.175, 0.475, 0.024, 0.064, 0.175 };

			double[] output = OutputNeuron.Softmax(input);

			for (int i = 0; i < output.Length; i++)
			{
				output[i] = Math.Round(output[i], 3);

				Assert.AreEqual(output[i], expectedOutput[i]);
			}
		}
	}
}