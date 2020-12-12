using System;

namespace CsvImporter.Domain
{
	public class StockProduct
	{
		public int PointOfSale { get; set; }
		public string ProductNumber { get; set; }
		public DateTime DateInventory { get; set; }
		public int Stock { get; set; }
		public string Description { get; set; }
	}
}
