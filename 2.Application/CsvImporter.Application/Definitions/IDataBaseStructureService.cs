using System.Threading.Tasks;


namespace CsvImporter.Application.Definitions
{
	public interface IDataBaseStructureService
	{
		Task<int> CreateStockProductTableAsync();
	}
}
