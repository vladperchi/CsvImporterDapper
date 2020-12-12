using CsvImporter.Application.Rest.Implementations;
using CsvImporter.DataAccess.RestClient;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.Application.Rest
{
	public static class IOCConfiguration
	{
		public static void RegisterApplicationRestClientDependencies(this IServiceCollection container)
		{
			container.RegisterDataAccessRestClientDependencies();
			container.AddScoped<IConfigurationServiceRest, ConfigurationServiceRest>();
			container.AddScoped<IStockProductServiceRest, StockProductServiceRest>();
		}
	}
}
