using System.Threading.Tasks;

namespace CsvImporter.Application.Definitions
{
	public interface IStockProductService
	{
		Task<int> SaveMassStockAsync(string pathFile);
		Task<int> CountRowsInStock();
		Task<int> DeleteMassAsync();
	}
}
