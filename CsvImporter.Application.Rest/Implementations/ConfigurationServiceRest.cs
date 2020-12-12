using CsvImporter.DataAccess.RestClient;
using System.Threading.Tasks;

namespace CsvImporter.Application.Rest.Implementations
{
	public class ConfigurationServiceRest : IConfigurationServiceRest
	{
		private readonly IConfigurationRestClient _configurationRestClient;

		public ConfigurationServiceRest(IConfigurationRestClient configurationRestClient)
		{
			_configurationRestClient = configurationRestClient;
		}

		public async Task<int> CreateStockProductTableAsync()
		{
			return await _configurationRestClient.CreateStockProductTableAsync();
		}
	}
}
