using System;
using Docker.DotNet;
using Microsoft.Extensions.Options;

namespace CrowsNest.UI
{
	internal class DockerClientFactory : IDockerClientFactory
	{
		private readonly IOptions<CrowsNestDockerConfiguration> options;

		public DockerClientFactory(IOptions<CrowsNestDockerConfiguration> options)
		{
			this.options = options;
		}

		public IDockerClient Create()
		{
			return new DockerClientConfiguration(new Uri(options.Value.Uri)).CreateClient();
		}
	}
}