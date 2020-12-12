using System.Threading.Tasks;

namespace CsvImporter.DataAccess.RestClient
{
	public interface IConfigurationRestClient
	{
		Task<int> CreateStockProductTableAsync();
	}
}
