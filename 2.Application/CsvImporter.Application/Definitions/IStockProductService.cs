using System.Threading.Tasks;

namespace CsvImporter.Application
{
	public interface IStockProductService
	{
		Task<int> SaveMassStockAsync(string pathFile);
		Task<int> CountRowsInStock();
		Task<int> DeleteMassAsync();
	}
}
