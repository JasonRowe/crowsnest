using System.Collections.Generic;
using Docker.DotNet.Models;

namespace CrowsNest.UI
{
	public class ContainerListViewModel
	{
		public string ID { get; set; }
		
		public IList<string> Names { get; set; }

		public string Image { get; set; }

		public string State { get; set; }

		public IList<Port> Ports { get; set; }

		public IList<string> IPAddresses { get; set; }
	}
}