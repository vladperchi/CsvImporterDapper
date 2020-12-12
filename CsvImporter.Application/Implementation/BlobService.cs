using CsvImporter.Application.Interfaces;
using CsvImporter.Domain;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.RetryPolicies;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter.Application.Implementation
{
	public class BlobService : IBlobService
	{
		public async Task<string> GetFileFromBlobStorage(BlobRequest blobRequest)
		{
			if (blobRequest.StorageUri == null || string.IsNullOrEmpty(blobRequest.StorageUri))
			{
				throw new ArgumentException("Se debe enviar el StorageUri");
			}
			if (blobRequest.FileName == null || string.IsNullOrEmpty(blobRequest.FileName))
			{
				throw new ArgumentException("Se debe enviar el FileName");
			}
			if (blobRequest.Folder == null || string.IsNullOrEmpty(blobRequest.Folder))
			{
				throw new ArgumentException("Se debe enviar el Folder");
			}

			var fileFullPath = string.Empty;
			Uri uri = new Uri(blobRequest.StorageUri);
			CloudBlobClient blobclient = new CloudBlobClient(uri);
			CloudBlobContainer blobcontainer = blobclient.GetContainerReference(blobRequest.Folder);
			CloudBlockBlob blob = blobcontainer.GetBlockBlobReference(blobRequest.FileName);
			blob.FetchAttributes();
			var blobSize = blob.Properties.Length;
			long blockSize = (10 * 1024 * 1024);
			blockSize = Math.Min(blobSize, blockSize);

			if (!Directory.Exists("Files"))
			{
				Directory.CreateDirectory("Files");
			}
			var filePath = $"Files\\{blobRequest.FileName}";
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			using (FileStream fs = new FileStream(filePath, FileMode.Create))
			{
				fs.SetLength(blobSize);
				fileFullPath = fs.Name;
			}

			var blobRequestOptions = new BlobRequestOptions
			{
				RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(5), 3),
				MaximumExecutionTime = TimeSpan.FromMinutes(60),
				ServerTimeout = TimeSpan.FromMinutes(60)
			};
			long currentPointer = 0;
			long bytesRemaining = blobSize;
			do
			{
				try
				{
					var bytesToFetch = Math.Min(blockSize, bytesRemaining);
					using (MemoryStream ms = new MemoryStream())
					{
						blob.DownloadRangeToStream(ms, currentPointer, bytesToFetch, null, blobRequestOptions);
						ms.Position = 0;
						var contents = ms.ToArray();

						using (var fs = new FileStream(filePath, FileMode.Open))
						{
							fs.Position = currentPointer;
							fs.Write(contents, 0, contents.Length);
						}

						currentPointer += contents.Length;
						bytesRemaining -= contents.Length;
					}
				}
				catch (Exception ex)
				{
					throw new Exception($"File has error in {currentPointer} a las {DateTime.Now}", ex);
				}
			}
			while (bytesRemaining > 0);

			return fileFullPath;
		}
	}
}
