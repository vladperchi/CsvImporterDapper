using System.Threading.Tasks;


namespace CsvImporter.Application
{
	public interface IDataBaseStructureService
	{
		Task<int> CreateStockProductTableAsync();
	}
}
