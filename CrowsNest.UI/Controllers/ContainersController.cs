using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrowsNest.UI.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class ContainersController : Controller
	{
		private readonly IDockerClientFactory dockerClientFactory;

		public ContainersController(IDockerClientFactory dockerClientFactory)
		{
			this.dockerClientFactory = dockerClientFactory;
		}

		[HttpGet("[action]")]
		public async Task<bool> Stop(string id){
			var result = false;

			using (var client = this.dockerClientFactory.Create())
			{
				result = await client.Containers.StopContainerAsync(id, new ContainerStopParameters());
			}

			return result;
		}

		[HttpGet("[action]")]
		public async Task<bool> Start(string id){
			var result = false;

			using (var client = this.dockerClientFactory.Create())
			{
				result = await client.Containers.StartContainerAsync(id, new ContainerStartParameters());
			}

			return result;
		}

		[HttpGet("[action]")]
		public async Task<IList<ContainerListViewModel>> List(int limit, bool showAll)
		{
			var containerModels = new List<ContainerListViewModel>();

			using (var client = this.dockerClientFactory.Create())
			{
				var dockerContainers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = showAll, Limit = limit });

				foreach (var container in dockerContainers)
				{
					var ipAddresses = FindNetworkIPAddress(container.NetworkSettings.Networks);

					var model = new ContainerListViewModel
					{
						ID = container.ID,
						Image = container.Image,
						Names = container.Names,
						Ports = container.Ports,
						State = container.State,
						IPAddresses = ipAddresses,
					};

					containerModels.Add(model);
				}
			}

			return containerModels;
		}

		private List<string> FindNetworkIPAddress(IDictionary<string, EndpointSettings> networks)
		{
			var ipAddresses = new List<string>();

			foreach(var networkName in networks.Keys){

				var ip = networks[networkName].IPAddress;

				if(!string.IsNullOrWhiteSpace(ip)){
					ipAddresses.Add(ip);
				}
			}

			return ipAddresses;
		}
	}
}