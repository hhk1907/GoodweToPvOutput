using GoodweDataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public class PvOutputService : IPvOutputService
	{
		public HttpClient HttpClient { get; }

		public PvOutputService(HttpClient httpClient)
		{
			HttpClient = httpClient;
		}



		public async void AddStatus(GoodweData goodweData)
		{
			PvOutputData pvOutputData = new PvOutputData()
			{
				EnergyGeneration = (int)(goodweData.ETotal * 1000),
				PowerGeneration = goodweData.OutputPower,
				Date = goodweData.TimeStamp.ToString("yyyyMMdd"),
				Time = goodweData.TimeStamp.ToString("HH:mm"),
			};


			var parameters = new Dictionary<string, string> {
				{ "d", pvOutputData.Date },
				{ "t", pvOutputData.Time },
				{ "v1", pvOutputData.EnergyGeneration.ToString() },
				{ "v2", pvOutputData.PowerGeneration.ToString() }
			};

			var encodedContent = new FormUrlEncodedContent(parameters);

			//if (!HttpClient.DefaultRequestHeaders.Contains("X-Pvoutput-Apikey"))
			//{
			//	HttpClient.DefaultRequestHeaders.Add("X-Pvoutput-Apikey", "7399e14968f03ce6329da1c324737632d6320185");
			//	HttpClient.DefaultRequestHeaders.Add("X-Pvoutput-SystemId", "79017");
			//}

			var response = await HttpClient.PostAsync("addstatus.jsp", encodedContent);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				var resultString = await response.Content.ReadAsStringAsync();
			}


		}

		public string GetStatus()
		{
			throw new NotImplementedException();
		}
	}
}
