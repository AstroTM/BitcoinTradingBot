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
			double[,] input = new double[4, inputAsDatabaseRows.Count / 4];
			for (int i = 0; i < inputAsDatabaseRows.Count / 4; i++)
			{
				input[0, i] = inputAsDatabaseRows[i].date;
				input[1, i] = inputAsDatabaseRows[i].price;
				input[2, i] = inputAsDatabaseRows[i].volAsk;
				input[3, i] = inputAsDatabaseRows[i].volBid;
			}
		}
}