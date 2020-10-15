using System;

namespace GoodweDataManagement.Models
{
	public class GoodweData
	{
		//public int Temperature { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		//public string OutputCurrent { get; set; }
		public int OutputVoltage { get; set; }

		/// <summary>
		/// outputpower (W) 'out_pac'
		/// </summary>
		public int OutputPower { get; set; }

		/// <summary>
		/// Total of day (kWh) 'eday'
		/// </summary>
		public double ETotal { get; set; }

		public string RawData { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
