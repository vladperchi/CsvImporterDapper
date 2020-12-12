using System.Threading.Tasks;

namespace CsvImporter.DataAccess
{
	public interface IDataBaseStructureRepository
	{
		Task<int> CreateStockProductTableAsync();
	}
}
