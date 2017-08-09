using System.Collections.Generic;
using TradingLib;

namespace TradingBot
{
	internal class NeuralNet
	{
		public NeuralNet()
		{
		}

		public void TrainNetwork(List<DatabaseRow> inputAsDatabaseRows)
		{
			InputData X = new InputData(inputAsDatabaseRows);
		}
	}
}