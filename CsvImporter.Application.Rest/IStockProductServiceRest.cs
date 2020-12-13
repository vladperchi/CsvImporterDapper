using CsvImporter.Domain;
using System.Threading.Tasks;

namespace CsvImporter.Application.Rest
{
	public interface IStockProductServiceRest
	{
		Task<string> GetFileBlobAsync(BlobRequest blobRequest);
		Task<DownloadResult> GetFileBlobParallelAsync(BlobRequest blobRequest);
		Task<int> SaveStockDataAsync(string path);
		Task<int> CountStockDataAsync();
		Task<int> DeleteStockDataAsync();
	}
}
