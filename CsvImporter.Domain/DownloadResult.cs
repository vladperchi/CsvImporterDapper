using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Domain
{
	public class DownloadResult
	{
		public long Size { get; set; }
		public String FilePath { get; set; }
		public TimeSpan TimeTaken { get; set; }
		public int ParallelDownloads { get; set; }
	}
}
