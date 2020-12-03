using System.Threading.Tasks;

namespace CsvImporter.Domain.Common.Definitions
{
	public interface IApplicationSettingsManager
	{
		Task<string> GetConnectionStringValuebyKey(string key);
	}
}
