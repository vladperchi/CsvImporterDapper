using CsvImporter.Domain.Common.Definitions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CsvImporter.Application.Implementation
{
	public class ApplicationSettingsManager : IApplicationSettingsManager
	{
		private readonly IConfiguration _configuration;
		public ApplicationSettingsManager(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> GetConnectionStringValuebyKey(string key)
		{
			await Task.Yield();
			return _configuration.GetConnectionString(key);
		}
	}
}
