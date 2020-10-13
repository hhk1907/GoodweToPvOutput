using BlazorApp1.Services;
using GoodweDataManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorApp1.Pages
{
	public class GoodweBase : ComponentBase
	{
		[Inject]
		public IGoodweService GoodweService { get; set; }
		[Inject]
		public IPvOutputService PvOutputService { get; set; }

		private Timer timer;

		private bool getdatabool = false;

		protected int currentCount = 0;
		//protected string data = string.Empty;
		public string Data { get; set; }
		public List<GoodweData> GoodweData { get; set; }

		protected override async Task OnInitializedAsync()
		{
			//await Task.Run(GoodweService.TokenRequest);
			//ConfigureTimer();
			//this.GoodweData = new List<GoodweData>();
		}

		private void ConfigureTimer()
		{
			timer = new Timer(300000);
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			//if (!getdatabool)
			//{
			//getdatabool = true;
			var latestData = GoodweService.GetData().Result;

			GoodweData.Add(latestData);

			PvOutputService.AddStatus(latestData);

			if (GoodweData.Count > 100)
				GoodweData.RemoveAt(0);
			//}
			InvokeAsync(StateHasChanged);
		}

		protected void GetData()
		{
			var latestData = GoodweService.GetData().Result;
			GoodweData.Add(latestData);
		}
	}
}
