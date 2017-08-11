using System.Collections.Generic;
using TradingLib;

namespace TradingBot
{
	public class InputData
	{
		public double[,] xTrain;
		public double[,] xTest;

		public int maxDate = 2147483647;
		public double maxPrice;
		public double maxVolAsk;
		public double maxVolBid;

		public InputData(List<DatabaseRow> inputAsDatabaseRows)
		{
			// Makes sure number of inputs is a multiple of 3, so that it splits into arrays neatly.
			if (inputAsDatabaseRows.Count % 3 == 1) inputAsDatabaseRows.RemoveAt(0);
			if (inputAsDatabaseRows.Count % 3 == 2)
			{
				inputAsDatabaseRows.RemoveAt(0);
				inputAsDatabaseRows.RemoveAt(1);
			}

			int thirdLength = inputAsDatabaseRows.Count / 3; // Splits at 2/3 of the data

			foreach (DatabaseRow row in inputAsDatabaseRows)
			{
				if (row.price > maxPrice) maxPrice = row.price;
				if (row.volAsk > maxVolAsk) maxVolAsk = row.volAsk;
				if (row.volBid > maxVolBid) maxVolBid = row.volBid;
			}

			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				inputAsDatabaseRows[i].date = inputAsDatabaseRows[i].date / maxDate;
				inputAsDatabaseRows[i].price = inputAsDatabaseRows[i].price / maxPrice;
				inputAsDatabaseRows[i].volAsk = inputAsDatabaseRows[i].volAsk / maxVolAsk;
				inputAsDatabaseRows[i].volBid = inputAsDatabaseRows[i].volBid / maxVolBid;
			}

			double[,] input = new double[inputAsDatabaseRows.Count, 4];
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				input[i, 0] = inputAsDatabaseRows[i].date;
				input[i, 1] = inputAsDatabaseRows[i].price;
				input[i, 2] = inputAsDatabaseRows[i].volAsk;
				input[i, 3] = inputAsDatabaseRows[i].volBid;
			}

			xTrain = new double[thirdLength * 2, 4];
			xTest = new double[thirdLength, 4];

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