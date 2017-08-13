using System.Collections.Generic;
using TradingLib;

namespace TradingBot
{
	public class InputData
	{
		public double[,] xTrain;
		public double[,] xTest;

	    public double[] max;

		public InputData(List<DatabaseRow> inputAsDatabaseRows)
		{
            max = new double[11];
		    max[0] = 2147483647;

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
			    if (row.price >      max[1]) max[1] = row.price;
			    if (row.bid >        max[2]) max[2] = row.bid;
			    if (row.sizeBid >    max[3]) max[3] = row.sizeBid;
			    if (row.volBid >     max[4]) max[4] = row.volBid;
			    if (row.ask >        max[5]) max[5] = row.ask;
			    if (row.sizeAsk >    max[6]) max[6] = row.sizeAsk;
			    if (row.volAsk >     max[7]) max[7] = row.volAsk;
			    if (row.percChange > max[8]) max[8] = row.percChange;
			    if (row.high >       max[9]) max[9] = row.high;
			    if (row.low >        max[10]) max[10] = row.low;
            }

			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    inputAsDatabaseRows[i].date = inputAsDatabaseRows[i].date / max[0];
			    inputAsDatabaseRows[i].price = inputAsDatabaseRows[i].price / max[1];
			    inputAsDatabaseRows[i].bid = inputAsDatabaseRows[i].bid / max[2];
			    inputAsDatabaseRows[i].sizeBid = inputAsDatabaseRows[i].sizeBid / max[3];
			    inputAsDatabaseRows[i].volBid = inputAsDatabaseRows[i].volBid / max[4];
			    inputAsDatabaseRows[i].ask = inputAsDatabaseRows[i].ask / max[5];
			    inputAsDatabaseRows[i].sizeAsk = inputAsDatabaseRows[i].sizeAsk / max[6];
			    inputAsDatabaseRows[i].volAsk = inputAsDatabaseRows[i].volAsk / max[7];
			    inputAsDatabaseRows[i].percChange = inputAsDatabaseRows[i].percChange / max[8];
			    inputAsDatabaseRows[i].high = inputAsDatabaseRows[i].high / max[9];
			    inputAsDatabaseRows[i].low = inputAsDatabaseRows[i].low / max[10];
            }

			double[,] input = new double[inputAsDatabaseRows.Count, 4];
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				input[i, 0] = inputAsDatabaseRows[i].date;
				input[i, 1] = inputAsDatabaseRows[i].price;
				input[i, 2] = inputAsDatabaseRows[i].volAsk;
				input[i, 3] = inputAsDatabaseRows[i].volBid;
			    inputAsDatabaseRows[i].date = inputAsDatabaseRows[i].date;
			    inputAsDatabaseRows[i].price = inputAsDatabaseRows[i].price;
			    inputAsDatabaseRows[i].bid = inputAsDatabaseRows[i].bid;
			    inputAsDatabaseRows[i].sizeBid = inputAsDatabaseRows[i].sizeBid;
			    inputAsDatabaseRows[i].volBid = inputAsDatabaseRows[i].volBid;
			    inputAsDatabaseRows[i].ask = inputAsDatabaseRows[i].ask;
			    inputAsDatabaseRows[i].sizeAsk = inputAsDatabaseRows[i].sizeAsk;
			    inputAsDatabaseRows[i].volAsk = inputAsDatabaseRows[i].volAsk;
			    inputAsDatabaseRows[i].percChange = inputAsDatabaseRows[i].percChange;
			    inputAsDatabaseRows[i].high = inputAsDatabaseRows[i].high;
			    inputAsDatabaseRows[i].low = inputAsDatabaseRows[i].low;
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