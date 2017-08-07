using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TradingLib;

namespace TradingPriceUpdater
{
	public partial class PriceUpdaterService : ServiceBase
	{
		private Timer _timer;
		private ApiReader apiRead;
		private DatabaseConnector databaseConnect;
		private CurrencyPair currency;

		public PriceUpdaterService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			apiRead = new ApiReader(); // Initialises ApiReader
			databaseConnect = new DatabaseConnector(); // Initialises DatabaseConnector

			currency = new CurrencyPair(2, 1); // Creates new currency pair of ETH/BTC

			_timer = new Timer(10 * 1000); // Create timer to run every 10 seconds
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed); // Sets method to be run when the timer is elapsed
			_timer.Start(); // Starts the timer
		}

		protected override void OnStop()
		{
			databaseConnect.CloseConnection(); // Closes the connection
		}

		/// <summary>
		/// Run every 10 seconds, gets data from the API and puts it into the database.
		/// </summary>
		public void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			TickerResult ticker = apiRead.GetTickerResult(currency); // Gets the ticker for the specified currency
		}
	}
}
