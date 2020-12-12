using CsvImporter.DataAccess.RestClient;
using CsvImporter.Domain;
using System.Threading.Tasks;

namespace CsvImporter.Application.Rest.Implementations
{
	public class StockProductServiceRest : IStockProductServiceRest
	{
		private readonly IStockProductRestClient _stockProductRestClient;

		public StockProductServiceRest(IStockProductRestClient stockProductRestClient)
		{
			_stockProductRestClient = stockProductRestClient;
		}

		public async Task<string> GetFileBlobAsync(BlobRequest blobRequest)
		{
			return await _stockProductRestClient.GetFileBlobAsync(blobRequest);
		}
		public async Task<int> SaveStockDataAsync(string path)
		{
			return await _stockProductRestClient.SaveStockDataAsync(path);
		}

		public async Task<int> CountStockDataAsync()
		{
			return await _stockProductRestClient.CountStockDataAsync();
		}

		public async Task<int> DeleteStockDataAsync()
		{
			return await _stockProductRestClient.DeleteStockDataAsync();
		}

	}
}
