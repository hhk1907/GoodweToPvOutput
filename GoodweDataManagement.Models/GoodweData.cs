using System;

namespace GoodweDataManagement.Models
{
	public class GoodweData
	{
		public int Temperature { get; set; }
		public string OutputCurrent { get; set; }
		public string OutputVoltage { get; set; }
		public string OutputPower { get; set; }
		public int ETotal { get; set; }

		public string RawData { get; set; }
	}
}
