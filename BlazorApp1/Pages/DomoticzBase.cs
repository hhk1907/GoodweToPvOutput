using BlazorApp1.Services;
using GoodweDataManagement.Models.Domoticz;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
	public class DomoticzBase : ComponentBase
	{
		[Inject]
		public IDomoticzService DomoticzService { get; set; }

		public DomoticzData domoticzData;

		protected override Task OnInitializedAsync()
		{
			return base.OnInitializedAsync();
		}

		public async void GetDomoticzData()
		{
			domoticzData = await DomoticzService.GetDomoticzData();// GetDeviceByIdx(26);
			await InvokeAsync(StateHasChanged);
		}
	}
}
