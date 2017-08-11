using System.Collections.Generic;
using TradingLib;

namespace TradingBot
{
	public class NeuralNetwork
	{
		public NeuralNetwork()
		{
		}

		public void TrainNetwork(List<DatabaseRow> inputAsDatabaseRows)
		{
			InputData X = new InputData(inputAsDatabaseRows);


		}

		public dynamic Softmax(dynamic z)
		{
			var zExp = z;

			foreach (double value in z)
			{
				
			}
		}
	}
}