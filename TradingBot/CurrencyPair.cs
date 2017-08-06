using System;

namespace TradingBot
{
	public class CurrencyPair
	{
		public int currency1;
		public int currency2;

		/**
		 * 0: USD
		 * 1: BTC
		 * 2: ETH
		**/

		public CurrencyPair(int currency1, int currency2)
		{
			this.currency1 = currency1;
			this.currency2 = currency2;
		}

		public string GetBitfinexCurrencyPair()
		{
			switch (currency1)
			{
				case 0:
					switch (currency2)
					{
						case 1:
							return "tBTCUSD";
							break;
						case 2:
							return "tETHUSD";
							break;
					}
					break;
				case 2:
					switch (currency2)
					{
						case 1:
							return "tETHBTC";
							break;
					}
					break;
			}
			return "NotDefined";
		}
	}
}