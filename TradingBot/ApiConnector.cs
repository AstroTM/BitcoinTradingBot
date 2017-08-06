using System;
using System.Net;

namespace TradingBot
{
	public class ApiConnector
	{
		public ApiConnector()
		{
		}

		public int getPrice(CurrencyPair currency)
		{
			string rawData = downloadString()

			return price;
		}

		public static string downloadString(string address)
		{
			WebClient client = new WebClient();
			string reply = client.DownloadString(address);

			return reply;
		}
	}
}