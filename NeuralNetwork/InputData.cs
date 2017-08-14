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
            max = new double[inputAsDatabaseRows[0].data.Length];
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
			        if (row.data[i] > max[i]) max[i] = row.data[i];
                }
            }

			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
			    {
			        inputAsDatabaseRows[i].data[j] = inputAsDatabaseRows[i].data[j] / max[j];
                }
            }

			double[,] input = new double[inputAsDatabaseRows.Count, inputAsDatabaseRows[0].data.Length];
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
			    {
			        input[i, j] = inputAsDatabaseRows[i].data[j];
			    }
            }

			xTrain = new double[thirdLength * 2, inputAsDatabaseRows[0].data.Length];
			xTest = new double[thirdLength, inputAsDatabaseRows[0].data.Length];

			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				if (i < thirdLength * 2)
				{
				    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
				    {
				        xTrain[i, j] = input[i, j];
				    }
				}
				else
				{
				    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
				    {
				        xTest[i - thirdLength * 2, j] = input[i, j];
                    }
				}
			}
		}
	}
}