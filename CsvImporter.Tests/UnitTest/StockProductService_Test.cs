using CsvImporter.Application;
using CsvImporter.Application.Implementation;
using CsvImporter.Tests.UnitTest.RepositoriesMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsvImporter.Tests.UnitTest
{
	public class StockProductService_Test
	{
		public StockProductService_Test()
		{
			this.InstanceToTest = new StockProductService(StockRepositoryMock.GetStockMock().Object);
		}

		internal IStockProductService InstanceToTest { get; }

		[Fact]
		public async Task SaveMassStock()
		{
			string path = "File\\Stock.csv";
			var result = await InstanceToTest.SaveMassStockAsync(path);
			Assert.True(result == UnitConsts.TOTAL_ROW);
		} 

		[Fact]
		public async Task DeleteStock()
		{
			var result = await InstanceToTest.DeleteMassAsync();
			Assert.True(result == UnitConsts.TOTAL_ROW);
		}

		[Fact]
		public async Task CountStock()
		{
			var result = await InstanceToTest.CountRowsInStock();
			Assert.True(result == UnitConsts.TOTAL_ROW);
		}


	}
}
