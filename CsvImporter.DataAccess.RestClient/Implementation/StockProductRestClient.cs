using CsvImporter.Domain;
using CsvImporter.Utilities.Infrastructure.RestClient;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.RestClient.Implementation
{
	public class StockProductRestClient : BaseRestClient, IStockProductRestClient
	{
		public StockProductRestClient() : base("http://localhost:61187")
		{
		}

		public async Task<string> GetFileBlobAsync(BlobRequest blobRequest)
		{
			return await Post<string, BlobRequest>(blobRequest,
				"api/StockProduct/GetFileBlob/", null);
		}

		public async Task<DownloadResult> GetFileBlobParallelAsync(BlobRequest blobRequest)
		{
			return await Post<DownloadResult, BlobRequest>(blobRequest,
				"api/StockProduct/GetFileBlobParallel/", null);
		}

		public async Task<int> SaveStockDataAsync(string path)
		{
			return await Post<int, string>(path,
				"api/StockProduct/SaveStockData/", null);
		}

		public async Task<int> CountStockDataAsync()
		{
			return await Get<int>(
				"api/StockProduct/CountStockData/", null);
		}

		public async Task<int> DeleteStockDataAsync()
		{
			return await Post<int>(
				"api/StockProduct/DeleteStockData/", null);
		}
	}
}
