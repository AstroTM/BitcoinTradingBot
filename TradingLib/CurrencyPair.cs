using System;

namespace TradingLib
{
	/// <summary>
	/// A currency pair to be given to an exchange. Eg. USD and Bitcoin
	/// </summary>
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

		/// <summary>
		/// Gets Bitfinex-style currency pair as a string
		/// </summary>
		/// <returns>Bitfinex-style currency pair</returns>
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

			// If it doesn't understand the currency pair given.
			throw new UnknownCurrencyPairException(
				"Currency pair (" + Currency1 + ", " + Currency2 + ") on Bitfinex unknown.");
		}
	}

	/// <summary>
	/// Currency pair is not known about on a certain exchange.
	/// </summary>
	public class UnknownCurrencyPairException : Exception
	{
		public UnknownCurrencyPairException() : base() { }
		public UnknownCurrencyPairException(string message) : base(message) { }
		public UnknownCurrencyPairException(string message, System.Exception inner) : base(message, inner) { }
	}
}