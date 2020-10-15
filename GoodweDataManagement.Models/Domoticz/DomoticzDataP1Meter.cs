using System;
using System.Collections.Generic;
using System.Text;

namespace GoodweDataManagement.Models.Domoticz
{
	public class DomoticzDataP1Meter : DomoticzData
	{
		public new List<DomoticzDataResultP1Meter> Result { get; set; }
	}
}
