using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp1.Data;
using BlazorApp1.Services;

namespace BlazorApp1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddSingleton<WeatherForecastService>();
			services.AddHttpClient<IGoodweService, GoodweService>(client =>
			{
				client.BaseAddress = new Uri("http://semsportal.com:82/api/v2/");
				client.DefaultRequestHeaders.Add("Accept", "application/json");
			});
			services.AddHttpClient<IPvOutputService, PvOutputService>(client =>
			{
				client.BaseAddress = new Uri("https://pvoutput.org/service/r2/");
				client.DefaultRequestHeaders.Add("X-Pvoutput-Apikey", Configuration.GetSection("PvOutput").GetSection("ApiKey").Value);
				client.DefaultRequestHeaders.Add("X-Pvoutput-SystemId", Configuration.GetSection("PvOutput").GetSection("SystemId").Value);
			});
			services.AddHttpClient<IDomoticzService, DomoticzService>(client => 
			{
				client.BaseAddress = new Uri("http://192.168.1.54:8080");
				client.DefaultRequestHeaders.Add("Accept", "application/json");
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
