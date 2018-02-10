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
		public async Task<IList<ContainerListViewModel>> List(int limit)
		{
			var containerModels = new List<ContainerListViewModel>();

			using (var client = this.dockerClientFactory.Create())
			{
				var dockerContainers = await client.Containers.ListContainersAsync(new ContainersListParameters() { Limit = limit });

				foreach (var container in dockerContainers)
				{
					var ipAddresses = new List<string>();

					foreach(var networkName in container.NetworkSettings.Networks.Keys){

						var ip = container.NetworkSettings.Networks[networkName].IPAddress;

						if(!string.IsNullOrWhiteSpace(ip)){
							ipAddresses.Add(ip);
						}
					}

					var model = new ContainerListViewModel
					{
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
	}
}