using CsvImporter.Utilities.Infrastructure.RestClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.RestClient.Implementation
{
	public class ConfigurationRestClient : BaseRestClient, IConfigurationRestClient
	{
		public ConfigurationRestClient() : base("http://localhost:61187")
		{
		}

		public async Task<int> CreateStockProductTableAsync()
		{
			return await Post<int>(
				"api/Configuration/CreateStockProductTable/", new Dictionary<string, string>
				{
				});
		}
	}
}
