using GoodweDataManagement.Models.Domoticz;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public class DomoticzService : IDomoticzService
	{
		private readonly HttpClient httpClient;

		public DomoticzService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<DomoticzData> GetDomoticzData()
		{
			var result = await httpClient.GetJsonAsync<DomoticzData>("/json.htm?type=devices&used=true&displayhidden=1");
			return result;
		}

		public async Task<DomoticzData> GetDeviceByIdx(int idx)
		{
			var result1 = await httpClient.GetAsync($"/json.htm?type=devices&rid={idx}");
			var resultString = await result1.Content.ReadAsStringAsync();
			DomoticzData domoticzData = JsonConvert.DeserializeObject<DomoticzData>(resultString);
			return domoticzData;
		}
	}
}
