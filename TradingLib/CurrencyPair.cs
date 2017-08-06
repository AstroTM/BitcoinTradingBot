using System;

namespace TradingLib
{
	public class CurrencyPair
	{
		public int Currency1;
		public int Currency2;

		/**
		 * 0: USD
		 * 1: BTC
		 * 2: ETH
		**/

		public CurrencyPair(int currency1, int currency2)
		{
			this.Currency1 = currency1;
			this.Currency2 = currency2;
		}

		public string GetBitfinexCurrencyPair()
		{
			switch (Currency1)
			{
				case 0:
					switch (Currency2)
					{
						case 1:
							return "tBTCUSD";
						case 2:
							return "tETHUSD";
					}
					break;
				case 2:
					switch (Currency2)
					{
						case 1:
							return "tETHBTC";
					}
					break;
			}

			throw new UnknownCurrencyPairException(
				"Currency pair (" + Currency1 + ", " + Currency2 + ") unknown.");
		}
	}

	public class UnknownCurrencyPairException : Exception
	{
		public UnknownCurrencyPairException() : base() { }
		public UnknownCurrencyPairException(string message) : base(message) { }
		public UnknownCurrencyPairException(string message, System.Exception inner) : base(message, inner) { }
	}
}