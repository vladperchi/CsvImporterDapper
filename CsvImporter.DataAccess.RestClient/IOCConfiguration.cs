using CsvImporter.DataAccess.RestClient.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.DataAccess.RestClient
{
	public static class IOCConfiguration
	{
		public static void RegisterDataAccessRestClientDependencies(this IServiceCollection container)
		{
			container.AddScoped<IConfigurationRestClient, ConfigurationRestClient>();
			container.AddScoped<IStockProductRestClient, StockProductRestClient>();
		}
	}
}
