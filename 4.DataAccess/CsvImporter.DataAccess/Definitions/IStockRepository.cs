using System.Threading.Tasks;

namespace CsvImporter.DataAccess
{
	public interface IStockRepository
	{
		Task<int> SaveMassStockAsync(string filePath);
		Task<int> DeleteMassAsync();
		Task<int> CountRowsInStock();
	}
}
