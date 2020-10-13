using System;
using System.Collections.Generic;
using System.Text;

namespace GoodweDataManagement.Models.Domoticz
{
	public class DomoticzData
	{
		public Int64 ActTime { get; set; }
		public string AstrTwilightEnd { get; set; }
		public string AstrTwilightStart { get; set; }
		public string CivTwilightEnd { get; set; }
		public string CivTwilightStart { get; set; }
		public string DayLength { get; set; }
		public string NautTwilightEnd { get; set; }
		public string NautTwilightStart { get; set; }
		public string ServerTime { get; set; }
		public string SunAtSouth { get; set; }
		public string Sunrise { get; set; }
		public string Sunset { get; set; }
		public string App_version { get; set; }
		public List<DomoticzDataResult> Result { get; set; }
		public string Status { get; set; }
		public string Title { get; set; }
	}
}
