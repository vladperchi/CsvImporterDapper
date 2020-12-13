using CsvImporter.Domain;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.RestClient
{
	public interface IStockProductRestClient
	{
		Task<string> GetFileBlobAsync(BlobRequest blobRequest);
		Task<DownloadResult> GetFileBlobParallelAsync(BlobRequest blobRequest);
		Task<int> SaveStockDataAsync(string path);
		Task<int> CountStockDataAsync();
		Task<int> DeleteStockDataAsync();

	}
}
