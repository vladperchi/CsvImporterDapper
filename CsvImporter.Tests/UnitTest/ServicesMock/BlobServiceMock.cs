using CsvImporter.Application.Interfaces;
using CsvImporter.Domain;
using Moq;

namespace CsvImporter.Tests.UnitTest.ServicesMock
{
	public static class BlobServiceMock
	{
		public static Mock<IBlobService> GetBlobMock()
		{
			var blobService = new Mock<IBlobService>();
			blobService.Setup(c => c.GetFileFromBlobStorage(It.IsAny<BlobRequest>()))
				.Returns<BlobRequest>
				((fileFullPath) =>
				{
					///TODO///
					return null;
				});

			return blobService;
		}
	}
}
