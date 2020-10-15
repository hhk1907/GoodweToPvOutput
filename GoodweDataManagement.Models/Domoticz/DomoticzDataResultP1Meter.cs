using System;
using System.Collections.Generic;
using System.Text;

namespace GoodweDataManagement.Models.Domoticz
{
	public class DomoticzDataResultP1Meter : DomoticzDataResult
	{
		/// <summary>
		/// Current usage
		/// </summary>
		public string Usage { get; set; }

		/// <summary>
		/// Current delivery
		/// </summary>
		public string UsageDeliv { get; set; }

		public string Counter { get; set; }
		public string CounterDeliv { get; set; }

		/// <summary>
		/// Total delivered today
		/// </summary>
		public string CounterDelivToday { get; set; }
	}
}
