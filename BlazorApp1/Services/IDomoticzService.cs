using GoodweDataManagement.Models.Domoticz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public interface IDomoticzService
	{
		public Task<DomoticzData> GetDomoticzData();

		public Task<DomoticzData> GetDeviceByIdx(int idx);
	}
}
