using CsvImporter.DataAccess;
using System.Threading.Tasks;

namespace CsvImporter.Application.Implementation
{
	public class StockProductService : IStockProductService
	{
		public StockProductService(IStockRepository stockRepository)
		{
			_stockRepository = stockRepository;
		}

		private IStockRepository _stockRepository { get; }

		public async Task<int> SaveMassStockAsync(string pathFile)
		{
			return await _stockRepository.SaveMassStockAsync(pathFile);
		}

		public async Task<int> CountRowsInStock()
		{
			return await _stockRepository.CountRowsInStock();
		}
		public async Task<int> DeleteMassAsync()
		{
			return await _stockRepository.DeleteMassAsync();
		}

	}
}
