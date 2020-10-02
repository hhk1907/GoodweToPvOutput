using BlazorApp1.Services;
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

		private Timer timer;

		protected int currentCount = 0;
		//protected string data = string.Empty;
		public string Data { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await Task.Run(GoodweService.TokenRequest);
			ConfigureTimer();
		}

		private void ConfigureTimer()
		{
			timer = new Timer(30000);
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Data += GoodweService.GetData().Result;
			InvokeAsync(StateHasChanged);
		}

		private async void GetData()
		{
			//data = await GoodweService.GetData();
		}

		protected void IncrementCount()
		{
			//GetData();
		}
	}
}
