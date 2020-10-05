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
		public GoodweData GoodweData { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await Task.Run(GoodweService.TokenRequest);
			ConfigureTimer();
		}

		private void ConfigureTimer()
		{
			timer = new Timer(10000);
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (!getdatabool)
			{
				getdatabool = true;
				GoodweData = GoodweService.GetData().Result;
				
				PvOutputService.AddStatus(GoodweData);

				
			}

		}

		protected void GetData()
		{
			GoodweData = GoodweService.GetData().Result;

			PvOutputService.AddStatus(GoodweData);
		}


	}
}
