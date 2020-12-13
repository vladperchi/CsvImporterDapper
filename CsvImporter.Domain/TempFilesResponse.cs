using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Domain
{
	public class TempFilesResponse
	{
		public int Index { get; set; }
		public string FileName { get; set; }
		public long StartRange { get; set; }
		public long EndRange { get; set; }
	}
}
