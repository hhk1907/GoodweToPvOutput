﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
	public interface IGoodweService
	{
		Task<bool> TokenRequest();
		Task<string> GetData();
	}
}