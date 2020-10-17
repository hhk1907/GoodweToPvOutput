using GoodweDataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public interface IPvOutputService
	{
		void AddStatus(GoodweData goodweData);
		Task<HttpResponseMessage> AddStatus(PvOutputData pvOutputData);

		string GetStatus();
	}
}
