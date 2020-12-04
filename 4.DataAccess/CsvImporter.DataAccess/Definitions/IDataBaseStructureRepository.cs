using System.Threading.Tasks;

namespace CsvImporter.DataAccess.Definitions
{
	public interface IDataBaseStructureRepository
	{
		Task<int> CreateStockProductTableAsync();
	}
}
