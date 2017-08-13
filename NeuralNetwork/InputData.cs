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
			    for (int i = 0; i < row.data.Length; i++)
			    {
			        if (row.data[i] > max[1]) max[1] = row.data[i];
                }
            }

			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
			    {
			        inputAsDatabaseRows[i].data[j] = inputAsDatabaseRows[i].data[j] / max[0];
                }
            }

			double[,] input = new double[inputAsDatabaseRows.Count, 4];
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
			    {
			        inputAsDatabaseRows[i].data[j] = inputAsDatabaseRows[i].data[j];
                }
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