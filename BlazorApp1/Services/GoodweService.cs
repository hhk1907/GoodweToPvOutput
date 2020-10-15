using GoodweDataManagement.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public class GoodweService : IGoodweService
	{
		private readonly HttpClient httpClient;
		private string token = @"{""uid"": """",""timestamp"": 0,""token"": """",""client"": ""web"",""version"": """",""language"": ""zh - CN""}";
		private bool tokenAvailable = false;
		private readonly IConfiguration Configuration;

		public GoodweService(HttpClient httpClient, IConfiguration configuration)
		{
			this.httpClient = httpClient;
			this.Configuration = configuration;
		}

		public async Task<GoodweData> GetData()
		{
			if (!tokenAvailable)
			{
				await TokenRequest();
			}

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "PowerStation/GetMonitorDetailByPowerstationId");

			var powerstationId = Configuration.GetSection("GoodWe").GetSection("powerStationId").Value;
			string content = @"{""powerStationId"": """ + powerstationId + "\"}";


			request.Content = new StringContent(content,
													Encoding.UTF8,
													"application/json");

			var result = await httpClient.SendAsync(request);
			if (result.IsSuccessStatusCode)
			{
				var resultString = await result.Content.ReadAsStringAsync();

				JObject jObject = JObject.Parse(resultString);

				GoodweData data = new GoodweData();
				data.ETotal = jObject.SelectToken("data.inverter[0].invert_full.eday").ToObject<double>();
				data.OutputPower = jObject.SelectToken("data.inverter[0].invert_full.pac").ToObject<int>();
				data.OutputVoltage = jObject.SelectToken("data.inverter[0].invert_full.vac1").ToObject<int>();
				data.TimeStamp = jObject.SelectToken("data.info.time").ToObject<DateTime>();
				//data.Temperature = 0;
				//data.OutputCurrent = 0;


				return data;
			}
			return null;

		}

		public async Task<bool> TokenRequest()
		{
			try
			{
				string userSettings = ReadUserSettingsFromFile();
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Common/CrossLogin");

				httpClient.DefaultRequestHeaders.Add("token", @"{""uid"": """",""timestamp"": 0,""token"": """",""client"": ""web"",""version"": """",""language"": ""zh - CN""}");


				request.Content = new StringContent(userSettings,
													Encoding.UTF8,
													"application/json");//CONTENT-TYPE header
																		//request.Headers.Add()

				var result = await httpClient.SendAsync(request);
				if (result.IsSuccessStatusCode)
				{
					var resultString = await result.Content.ReadAsStringAsync();

					JObject jObject = JObject.Parse(resultString);

					token = jObject["data"].ToString(Newtonsoft.Json.Formatting.None);

					httpClient.DefaultRequestHeaders.Remove("token");
					httpClient.DefaultRequestHeaders.Add("token", token);

					tokenAvailable = true;
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private string ReadUserSettingsFromFile()
		{
			using (StreamReader r = new StreamReader("userSettings.json"))
			{
				string json = r.ReadToEnd();
				return json;
			}
		}
	}
}
