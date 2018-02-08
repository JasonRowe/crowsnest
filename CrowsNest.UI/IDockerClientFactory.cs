using Docker.DotNet;

namespace CrowsNest.UI
{
	public interface IDockerClientFactory
	{
		IDockerClient Create();
	}
}