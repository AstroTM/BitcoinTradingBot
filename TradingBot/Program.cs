using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TradingLib;
using Microsoft.Scripting.Hosting;

namespace TradingBot
{
    class Program
    {
        private static ApiReader APR;
        private static DatabaseConnector DBC;
        private static NeuralNetwork ANN;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            APR = new ApiReader(); // Initialises ApiReader
            DBC = new DatabaseConnector(); // Initialises DatabaseConnector

            ANN = new NeuralNetwork();

            ANN.ImportWeights(ReadWeights());

            List<DatabaseRow> rows = DBC.SelectAllFromDatabase();

            //ANN.TrainNetwork(rows, 1000);

            List<double> weights = new List<double>();
            for (int i = 0; i < ANN.Synapses.Count; i++)
            {
                weights.Add(ANN.Synapses[i].Weight);
            }

            WriteWeights(weights);

            double output = TestNetwork(rows, 1);

            Console.ReadLine();
        }

        static double TestNetwork(List<DatabaseRow> rows, double inputBTC)
        {
            double BTCBalance = inputBTC;
            double ETHBalance = 0;

            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < InputData.max.Length; j++)
                {
                    rows[i].data[j] = rows[i].data[j] / InputData.max[j];
                }
            }

            foreach (var row in rows)
            {
                double netOutput = ANN.Forward(row.data);

                double fullBalanceInBTC = BTCBalance + ETHBalance * row.data[1];

                double ETHBalanceInBTC = netOutput * fullBalanceInBTC;

                BTCBalance = fullBalanceInBTC - ETHBalanceInBTC;
                
                ETHBalance = ETHBalanceInBTC / row.data[1];
            }

            return 0;
        }

        static void WriteWeights(List<double> weights)
        {
            string WeightSaveString = @"C:\Users\matth\OneDrive\Documents\Visual Studio 2017\Projects\TradingBot\weights.txt";

            if (File.Exists(WeightSaveString))
            {
                // Delete file
                File.Delete(WeightSaveString);
            }

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(WeightSaveString))
            {
                foreach (double weight in weights)
                {
                    file.WriteLine(weight);
                }
            }
        }

        static List<double> ReadWeights()
        {
            string WeightSaveString = @"C:\Users\matth\OneDrive\Documents\Visual Studio 2017\Projects\TradingBot\weights.txt";
            List<double> output = new List<double>();

            foreach(string line in File.ReadLines(WeightSaveString))
            {
                output.Add(Convert.ToDouble(line));
            }

            return output;
        }
    }
}
