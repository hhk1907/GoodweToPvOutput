using System;
using System.Collections.Generic;
using System.Text;

namespace GoodweDataManagement.Models
{
	public class PvOutputData
	{
		/// <summary>
		/// date in yyyymmdd format
		/// </summary>
		public string Date { get; set; }

		/// <summary>
		/// Time in hh:mm format
		/// </summary>
		public string Time { get; set; }

		/// <summary>
		/// This is the total Watt Hours generated so far for the current day, so this should start at 0 and steadily increase or stay the same during the day (WattHour)
		/// </summary>
		public int EnergyGeneration { get; set; }

		/// <summary>
		/// The current watts being generated. This will of course vary up/down from 0 to . Not really significant except for pretty graphs of your output. (Watt)
		/// </summary>
		public int PowerGeneration { get; set; }

	}
}
