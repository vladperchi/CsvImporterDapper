using System.Threading.Tasks;

namespace CsvImporter.DataAccess.Definitions
{
	public interface IStockRepository
	{
		Task<int> SaveMassStockAsync(string filePath);
		Task<int> DeleteMassAsync();
		Task<int> CountRowsInStock();
	}
}
