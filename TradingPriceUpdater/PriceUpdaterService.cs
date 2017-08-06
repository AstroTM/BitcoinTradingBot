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

namespace TradingPriceUpdater
{
	public partial class PriceUpdaterService : ServiceBase
	{
		private Timer _timer;

		public PriceUpdaterService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			_timer = new Timer(10 * 1000); // every 10 seconds
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
			_timer.Start(); 
		}

		protected override void OnStop()
		{
		}

		public void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
		}
	}
}
