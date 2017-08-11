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
	public class NeuralNetworkTests
	{
		[TestMethod()]
		public void TrainNetworkTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void SoftmaxTest1()
		{
			double[] input = {1.0, 2.0, 3.0, 4.0, 1.0, 2.0, 3.0};
			double[] output = {0.024, 0.064, 0.175, 0.475, 0.024, 0.064, 0.175};

			Assert.AreEqual(NeuralNetwork.Softmax(input), output);
		}
	}
}