using System;
using System.Collections.Generic;
using System.Linq;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrowsNest.UI.Controllers
{
	[Route("api/[controller]")]
	public class SampleDataController : Controller
	{
		public SampleDataController(IDockerClientFactory dockerClientFactory)
		{
			this.dockerClientFactory = dockerClientFactory;
		}

		private static string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly IDockerClientFactory dockerClientFactory;

		[HttpGet("[action]")]
		public IEnumerable<WeatherForecast> WeatherForecasts()
		{
			using (var client = dockerClientFactory.Create())
			{
				IList<ContainerListResponse> containers = client.Containers.ListContainersAsync(
				new ContainersListParameters()
				{
					Limit = 10,
				}).Result;
			}

			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			});
		}

		public class WeatherForecast
		{
			public string DateFormatted { get; set; }
			public int TemperatureC { get; set; }
			public string Summary { get; set; }

			public int TemperatureF
			{
				get
				{
					return 32 + (int)(TemperatureC / 0.5556);
				}
			}
		}
	}
}
