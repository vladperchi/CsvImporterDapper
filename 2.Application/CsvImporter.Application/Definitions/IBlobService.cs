using CsvImporter.Domain.Settings;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter.Application.Definitions
{
	public interface IBlobService
	{
		Task<Stream> GetFileFromBlobStorage(BlobRequest blobRequest);
	}
}
