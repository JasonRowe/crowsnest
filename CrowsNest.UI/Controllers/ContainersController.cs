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
		public async Task<IList<ContainerListResponse>> List(int limit)
		{
			using (var client = dockerClientFactory.Create())
			{
				return await client.Containers.ListContainersAsync(new ContainersListParameters() { Limit = limit });
			}
		}
	}
}