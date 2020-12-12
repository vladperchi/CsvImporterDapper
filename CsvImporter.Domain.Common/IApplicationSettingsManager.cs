using System.Threading.Tasks;

namespace CsvImporter.Domain.Common
{
	public interface IApplicationSettingsManager
	{
		Task<string> GetConnectionStringValuebyKey(string key);
	}
}
