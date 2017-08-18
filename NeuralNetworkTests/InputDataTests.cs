using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingLib;

namespace TradingBot.Tests
{
	[TestClass()]
	public class InputDataTests
	{
	    [TestMethod()]
	    public void NeuralNetwork_InputData_InputDataTest()
	    {
	        List<DatabaseRow> input = new List<DatabaseRow>();
	        input.Add(new DatabaseRow(1, 0.1, 0.01, 0.001, 0.0001, 0.00001, 0.000001));
	        input.Add(new DatabaseRow(2, 0.2, 0.02, 0.002, 0.0002, 0.00002, 0.000002));
	        input.Add(new DatabaseRow(3, 0.3, 0.03, 0.003, 0.0003, 0.00003, 0.000003));
	        input.Add(new DatabaseRow(4, 0.4, 0.04, 0.004, 0.0004, 0.00004, 0.000004));
	        input.Add(new DatabaseRow(5, 0.5, 0.05, 0.005, 0.0005, 0.00005, 0.000005));
	        input.Add(new DatabaseRow(6, 0.6, 0.06, 0.006, 0.0006, 0.00006, 0.000006));
	        input.Add(new DatabaseRow(7, 0.7, 0.07, 0.007, 0.0007, 0.00007, 0.000007));

	        InputData data = new InputData(input);

	        Assert.AreEqual(data.xTrain[0, 0], 2.0 / InputData.max[0]);
	        Assert.AreEqual(data.xTrain[0, 1], 0.2 / InputData.max[1]);
	        Assert.AreEqual(data.xTrain[0, 2], 0.02 / InputData.max[2]);
	        Assert.AreEqual(data.xTrain[0, 3], 0.002 / InputData.max[3]);

	        Assert.AreEqual(data.xTrain[1, 0], 3.0 / InputData.max[0]);
	        Assert.AreEqual(data.xTrain[1, 1], 0.3 / InputData.max[1]);
	        Assert.AreEqual(data.xTrain[1, 2], 0.03 / InputData.max[2], 0.000001);
	        Assert.AreEqual(data.xTrain[1, 3], 0.003 / InputData.max[3], 0.000001);

	        Assert.AreEqual(data.xTrain[2, 0], 4.0 / InputData.max[0]);
	        Assert.AreEqual(data.xTrain[2, 1], 0.4 / InputData.max[1]);
	        Assert.AreEqual(data.xTrain[2, 2], 0.04 / InputData.max[2]);
	        Assert.AreEqual(data.xTrain[2, 3], 0.004 / InputData.max[3]);

	        Assert.AreEqual(data.xTrain[3, 0], 5.0 / InputData.max[0]);
	        Assert.AreEqual(data.xTrain[3, 1], 0.5 / InputData.max[1]);
	        Assert.AreEqual(data.xTrain[3, 2], 0.05 / InputData.max[2]);
	        Assert.AreEqual(data.xTrain[3, 3], 0.005 / InputData.max[3]);

	        Assert.AreEqual(data.xTest[0, 0], 6.0 / InputData.max[0]);
	        Assert.AreEqual(data.xTest[0, 1], 0.6 / InputData.max[1]);
	        Assert.AreEqual(data.xTest[0, 2], 0.06 / InputData.max[2], 0.000001);
	        Assert.AreEqual(data.xTest[0, 3], 0.006 / InputData.max[3], 0.000001);

	        Assert.AreEqual(data.xTest[1, 0], 7.0 / InputData.max[0]);
	        Assert.AreEqual(data.xTest[1, 1], 0.7 / InputData.max[1]);
	        Assert.AreEqual(data.xTest[1, 2], 0.07 / InputData.max[2]);
	        Assert.AreEqual(data.xTest[1, 3], 0.007 / InputData.max[3]);
	    }

	    [TestMethod()]
	    public void NeuralNetwork_InputData_ScoringTest()
	    {
	        List<DatabaseRow> input = new List<DatabaseRow>();
	        input.Add(new DatabaseRow(1, 1, 1, 1, 1, 1, 1));
	        input.Add(new DatabaseRow(1, 1.1, 1, 1, 1, 1, 1));
	        input.Add(new DatabaseRow(1, 1, 1, 1, 1, 1, 1));

	        InputData data = new InputData(input);

	        Assert.AreEqual(1, data.xTrain[0, 7]);
	        Assert.AreEqual(0, data.xTrain[1, 7]);
	        Assert.AreEqual(0, data.xTest[0, 7]);
        }
    }
}