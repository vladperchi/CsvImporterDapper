namespace CsvImporter.Domain
{
	public class BlobRequest
	{
		public string StorageUri { get; set; }
		public string Folder { get; set; }
		public string FileName { get; set; }
	}
}
