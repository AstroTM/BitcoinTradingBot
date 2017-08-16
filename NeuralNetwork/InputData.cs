using System.Collections.Generic;
using System.Xml.Schema;
using TradingLib;

namespace TradingBot
{
	public class InputData
	{
		public double[,] xTrain;
		public double[,] xTest;

	    static public double[] max =
	    {
	        2147483647, 0.08, 2000, 2000, 100, 0.1, 250
	    };

		public InputData(List<DatabaseRow> inputAsDatabaseRows)
		{

            // Makes sure number of inputs is a multiple of 3, so that it splits into arrays neatly.
            if (inputAsDatabaseRows.Count % 3 == 1) inputAsDatabaseRows.RemoveAt(0);
			if (inputAsDatabaseRows.Count % 3 == 2)
			{
				inputAsDatabaseRows.RemoveAt(0);
				inputAsDatabaseRows.RemoveAt(1);
			}

		    // Splits at 2/3 of the data
            int thirdLength = inputAsDatabaseRows.Count / 3;

		    // If it's bigger than the max, update the max
            //foreach (DatabaseRow row in inputAsDatabaseRows)
			//{
			//    for (int i = 0; i < row.data.Length; i++)
			//    {
			//        if (row.data[i] > max[i]) max[i] = row.data[i];
            //    }
            //}

		    // Set each value to a fraction of the max
            for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
			    {
			       inputAsDatabaseRows[i].data[j] = inputAsDatabaseRows[i].data[j] / max[j];
                }
            }

            // Moves the array, kinda superfluous at the moment.
			double[,] input = new double[inputAsDatabaseRows.Count, inputAsDatabaseRows[0].data.Length + 1];
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
			    for (int j = 0; j < inputAsDatabaseRows[i].data.Length; j++)
			    {
			        input[i, j] = inputAsDatabaseRows[i].data[j];
			    }
            }

            // Add scoring variable
            // This will break if the price goes over 2x in 30 seconds - quite unlikely.
		    for (int i = 0; i < inputAsDatabaseRows.Count - 1; i++)
		    {
		        double rawscore = input[i + 1, 1] / input[i, 1];
		        rawscore = (rawscore / 2) * 64 - 31.5;
                input[i, inputAsDatabaseRows[0].data.Length] = rawscore;
		    }

            xTrain = new double[thirdLength * 2, inputAsDatabaseRows[0].data.Length + 1];
			xTest = new double[thirdLength, inputAsDatabaseRows[0].data.Length + 1];

            // Puts the values in the Train and Test data
			for (int i = 0; i < inputAsDatabaseRows.Count; i++)
			{
				if (i < thirdLength * 2)
				{
				    for (int j = 0; j < inputAsDatabaseRows[i].data.Length + 1; j++)
				    {
				        xTrain[i, j] = input[i, j];
				    }
				}
				else
				{
				    for (int j = 0; j < inputAsDatabaseRows[i].data.Length + 1; j++)
				    {
				        xTest[i - thirdLength * 2, j] = input[i, j];
                    }
				}
			}
		}
	}
}