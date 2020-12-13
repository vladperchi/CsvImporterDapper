using CsvImporter.Application.Interfaces;
using CsvImporter.Domain;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Range = CsvImporter.Domain.Range;

namespace CsvImporter.Application.Implementation
{
	public class BlobService : IBlobService
	{
		static BlobService()
		{
			ServicePointManager.Expect100Continue = false;
			ServicePointManager.DefaultConnectionLimit = 100;
			ServicePointManager.MaxServicePointIdleTime = 1000;

		}

		public async Task<string> GetFileFromBlobStorage(BlobRequest blobRequest)
		{
			await Task.Yield();
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
					using MemoryStream ms = new MemoryStream();
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
				catch (Exception ex)
				{
					throw new Exception($"File has error in {currentPointer} a las {DateTime.Now}", ex);
				}
			}
			while (bytesRemaining > 0);

			return fileFullPath;
		}

		public async Task<DownloadResult> GetFileFromBlobStorageParallel(BlobRequest blobRequest)
		{
			await Task.Yield();
			if (string.IsNullOrWhiteSpace(blobRequest.StorageUri) || string.IsNullOrEmpty(blobRequest.StorageUri))
			{
				throw new ArgumentException("Se debe enviar el StorageUri");
			}
			if (string.IsNullOrWhiteSpace(blobRequest.FileName) || string.IsNullOrEmpty(blobRequest.FileName))
			{
				throw new ArgumentException("Se debe enviar el FileName");
			}
			if (string.IsNullOrWhiteSpace(blobRequest.Folder) || string.IsNullOrEmpty(blobRequest.Folder))
			{
				throw new ArgumentException("Se debe enviar el Folder");
			}
			var fileUrl = $"{blobRequest.StorageUri}/{blobRequest.Folder}/{blobRequest.FileName}";
			Uri uri = new Uri(fileUrl);
			string destinationFilePath = Path.Combine("Files", blobRequest.FileName);
			DownloadResult result = new DownloadResult() { FilePath = destinationFilePath };
			var numberOfParallelDownloads = 10;

			#region Get file size  
			WebRequest webRequest = HttpWebRequest.Create(fileUrl);
			webRequest.Method = "HEAD";
			long responseLength;
			using (WebResponse webResponse = webRequest.GetResponse())
			{
				responseLength = long.Parse(webResponse.Headers.Get("Content-Length"));
				result.Size = responseLength;
			}
			#endregion

			if (!Directory.Exists("Files"))
			{
				Directory.CreateDirectory("Files");
			}
			if (File.Exists(destinationFilePath))
			{
				File.Delete(destinationFilePath);
			}

			using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Append))
			{
				List<TempFilesResponse> tempFilesDictionary = new List<TempFilesResponse>();

				#region Calculate ranges  
				List<Range> readRanges = new List<Range>();
				for (int chunk = 0; chunk < numberOfParallelDownloads - 1; chunk++)
				{
					var range = new Range()
					{
						Start = chunk * (responseLength / numberOfParallelDownloads),
						End = ((chunk + 1) * (responseLength / numberOfParallelDownloads)) - 1
					};
					readRanges.Add(range);
				}


				readRanges.Add(new Range()
				{
					Start = readRanges.Any() ? readRanges.Last().End + 1 : 0,
					End = responseLength - 1
				});

				#endregion

				DateTime startTime = DateTime.Now;

				#region Parallel download  

				int index = 0;
				Parallel.ForEach(readRanges, new ParallelOptions() { MaxDegreeOfParallelism = numberOfParallelDownloads }, readRange =>
				{
					HttpWebRequest httpWebRequest = HttpWebRequest.Create(fileUrl) as HttpWebRequest;
					httpWebRequest.Method = "GET";
					httpWebRequest.AddRange(readRange.Start, readRange.End);
					using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
					{
						String tempFilePath = Path.GetTempFileName();
						using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
						{
							httpWebResponse.GetResponseStream().CopyTo(fileStream);
							tempFilesDictionary.Add(new TempFilesResponse()
							{
								Index = (int)index,
								FileName = tempFilePath,
								EndRange = readRange.End,
								StartRange = readRange.Start
							});
						}
					}
					index++;

				});

				result.ParallelDownloads = index;

				#endregion

				result.TimeTaken = DateTime.Now.Subtract(startTime);

				#region Merge to single file  
				foreach (var tempFile in tempFilesDictionary.OrderBy(b => b.StartRange))
				{
					byte[] tempFileBytes = File.ReadAllBytes(tempFile.FileName);
					destinationStream.Write(tempFileBytes, 0, tempFileBytes.Length);
					File.Delete(tempFile.FileName);
				}
				#endregion
				result.FilePath = destinationStream.Name;

				return result;
			}
		}
	}
}
