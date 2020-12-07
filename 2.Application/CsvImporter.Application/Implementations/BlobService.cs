using CsvImporter.Application.Definitions;
using CsvImporter.Domain.Settings;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter.Application.Implementation
{
	public class BlobService : IBlobService
	{
		public async Task<Stream> GetFileFromBlobStorage(BlobRequest blobRequest)
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

			MemoryStream outPutStream = new MemoryStream();
			Uri uri = new Uri(blobRequest.StorageUri);
			CloudBlobClient blobclient = new CloudBlobClient(uri);
			CloudBlobContainer blobcontainer = blobclient.GetContainerReference(blobRequest.Folder);
			CloudBlockBlob blob = blobcontainer.GetBlockBlobReference(blobRequest.FileName);
			await blob.FetchAttributesAsync();
			var bufferLength = 2 * 1024 * 1024;
			long blobRemaininglength = blob.Properties.Length;
			Queue<KeyValuePair<long, long>> queues = new Queue<KeyValuePair<long, long>>();
			long offSet = 0;
			while (blobRemaininglength > 0)
			{
				long chunkLength = (long)Math.Min(bufferLength, blobRemaininglength);
				queues.Enqueue(new KeyValuePair<long, long>(offSet, chunkLength));
				offSet += (chunkLength);
				blobRemaininglength -= chunkLength;
			}
			try
			{
				await blob.DownloadToStreamAsync(outPutStream);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return outPutStream;
		}
	}
}
