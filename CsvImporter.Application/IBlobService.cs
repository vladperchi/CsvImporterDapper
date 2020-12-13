using CsvImporter.Domain;
using System.Threading.Tasks;

namespace CsvImporter.Application.Interfaces
{
	public interface IBlobService
	{
		Task<string> GetFileFromBlobStorage(BlobRequest blobRequest);
		Task<DownloadResult> GetFileFromBlobStorageParallel(BlobRequest blobRequest);
	}
}
