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
			double[,] input = new double[inputAsDatabaseRows.Count, 4];
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				input[i, 0] = inputAsDatabaseRows[i].date;
				input[i, 1] = inputAsDatabaseRows[i].price;
				input[i, 2] = inputAsDatabaseRows[i].volAsk;
				input[i, 3] = inputAsDatabaseRows[i].volBid;
			}

			int thirdLength = inputAsDatabaseRows.Count / 3; // Splits at 2/3 of the data

			double[,] xTrain = new double[thirdLength * 2, 4];
			double[,] xTest = new double[thirdLength, 4];

			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				if (i < thirdLength * 2)
				{
					xTrain[i, 0] = input[i, 0];
					xTrain[i, 1] = input[i, 1];
					xTrain[i, 2] = input[i, 2];
					xTrain[i, 3] = input[i, 3];
				}
				else
				{
					xTest[i - thirdLength * 2, 0] = input[i, 0];
					xTest[i - thirdLength * 2, 1] = input[i, 1];
					xTest[i - thirdLength * 2, 2] = input[i, 2];
					xTest[i - thirdLength * 2, 3] = input[i, 3];
				}
			}
		}
	}
}