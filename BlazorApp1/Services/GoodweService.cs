using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO.Pipelines;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;

namespace BlazorApp1.Services
{
	public class GoodweService : IGoodweService
	{
		private readonly HttpClient httpClient;
		private string token = @"{""uid"": """",""timestamp"": 0,""token"": """",""client"": ""web"",""version"": """",""language"": ""zh - CN""}";
		private bool tokenAvailable = false;
		//private Login login = new Login() { account = "h.kose@live.nl", pwd = "Fenerbahce!1", is_local = true, agreement_agreement = 1 };

		public GoodweService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<string> GetData()
		{
			if (!tokenAvailable)
			{
				await TokenRequest();
			}

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Common/GetDefaultInfo");
			var result = await httpClient.SendAsync(request);
			if (result.IsSuccessStatusCode)
			{
				var resultString = await result.Content.ReadAsStringAsync();

				return resultString;
			}
			return "";

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
	//public class Login
	//{
	//	public string account { get; set; }
	//	public string pwd { get; set; }
	//	public bool is_local { get; set; }
	//	public int agreement_agreement { get; set; }
	//}
}
