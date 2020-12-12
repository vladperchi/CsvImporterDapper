using System.Threading.Tasks;

namespace CsvImporter.Application.Rest
{

	public interface IConfigurationServiceRest
	{
		Task<int> CreateStockProductTableAsync();
	}
}
