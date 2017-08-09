using System.Collections.Generic;
using TradingLib;

namespace TradingBot
{
	internal class NeuralNetwork
	{
		public NeuralNetwork()
		{
		}

		public void TrainNetwork(List<DatabaseRow> inputAsDatabaseRows)
		{
			InputData X = new InputData(inputAsDatabaseRows);


		}
	}
}