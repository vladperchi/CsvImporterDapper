using CsvImporter.DataAccess.Base;
using CsvImporter.DataAccess.Definitions;
using CsvImporter.DataAccess.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.DataAccess
{
	public static class IOCConfiguration
	{
		public static void RegisterDataAccessDependencies(this IServiceCollection container)
		{
			container.RegisterDataAcessBaseDependencies();
			container.AddScoped<IStockRepository, StockRepository>();
			container.AddScoped<IDataBaseStructureRepository, DataBaseStructureRepository>();
		}
	}
}
