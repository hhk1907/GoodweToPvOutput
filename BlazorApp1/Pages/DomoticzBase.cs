using BlazorApp1.Services;
using GoodweDataManagement.Models;
using GoodweDataManagement.Models.Domoticz;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorApp1.Pages
{
	public class DomoticzBase : ComponentBase
	{
		[Inject]
		public IDomoticzService DomoticzService { get; set; }
		[Inject]
		public IPvOutputService PvOutputService { get; set; }

		int energyGenerationIdx = 33;
		int p1MeterIdx = 24;

		public DomoticzData domoticzData;

		private Timer timer;
		public List<PvOutputData> PvOutputDataList { get; set; }

		protected override Task OnInitializedAsync()
		{
			this.timer = new Timer(300000);
			timer.Elapsed += Timer_Elapsed;
			timer.Enabled = true;
			this.PvOutputDataList = new List<PvOutputData>();
			return base.OnInitializedAsync();
		}

		private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			var energyGenerationData = await DomoticzService.GetDeviceByIdx(energyGenerationIdx);
			var p1MeterData = await DomoticzService.GetDeviceByIdx(p1MeterIdx);

			SendDataToPvOutput(energyGenerationData, p1MeterData);
		}

		private void SendDataToPvOutput(DomoticzData energyGenerationData, DomoticzData p1MeterData)
		{
			PvOutputData pvOutputData = new PvOutputData();
			pvOutputData.Date = DateTime.Parse(energyGenerationData.Result[0].LastUpdate).ToString("yyyyMMdd");
			pvOutputData.Time = DateTime.Parse(energyGenerationData.Result[0].LastUpdate).ToString("HH:mm");
			pvOutputData.EnergyGeneration = int.Parse(Regex.Replace(energyGenerationData.Result[0].CounterToday, "[^0-9]", ""));
			pvOutputData.PowerGeneration = int.Parse(Regex.Replace(energyGenerationData.Result[0].Usage, "[^0-9]", ""));


			//Energy
			var counterToday = p1MeterData.Result[0].CounterToday;
			var deliveryToday = p1MeterData.Result[0].CounterDelivToday;

			// Power
			var usage = int.Parse(Regex.Replace(p1MeterData.Result[0].Usage, "[^0-9]", ""));
			var delivery = int.Parse(Regex.Replace(p1MeterData.Result[0].UsageDeliv, "[^0-9]", ""));
			var powerConsumption = usage - delivery;

			pvOutputData.PowerConsumption = powerConsumption;

			PvOutputDataList.Add(pvOutputData);
			if (PvOutputDataList.Count > 50)
				PvOutputDataList.RemoveAt(0);

			PvOutputService.AddStatus(pvOutputData);

			InvokeAsync(StateHasChanged);
		}

		public async void GetDomoticzData()
		{
			domoticzData = await DomoticzService.GetDomoticzData();// GetDeviceByIdx(26);
			await InvokeAsync(StateHasChanged);
		}
	}
}
