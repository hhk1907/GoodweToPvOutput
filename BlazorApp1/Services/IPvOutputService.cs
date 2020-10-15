using GoodweDataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public interface IPvOutputService
	{
		void AddStatus(GoodweData goodweData);
		void AddStatus(PvOutputData pvOutputData);

		string GetStatus();
	}
}
