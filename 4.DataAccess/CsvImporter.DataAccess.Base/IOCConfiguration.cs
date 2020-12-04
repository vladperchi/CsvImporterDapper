using CsvImporter.DataAccess.Base.Definitions;
using CsvImporter.DataAccess.Base.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.DataAccess.Base
{
	public static class IOCConfiguration
	{
		public static void RegisterDataAcessBaseDependencies(this IServiceCollection container)
		{
			container.AddScoped(typeof(IDapperBase<>), typeof(DapperBase<>));
		}
	}
}
