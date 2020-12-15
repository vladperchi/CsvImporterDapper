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
			this.instanceToTest = new StockProductService(StockRepositoryMock.GetMock().Object);
		}

		internal IStockProductService instanceToTest { get; }

		[Fact]
		public async Task SaveMassStock()
		{
			string path = "File\\Stock.csv";
			var result = await instanceToTest.SaveMassStockAsync(path);
			Assert.True(result == UnitConsts.TOTAL_INSERTED);
		} 

		[Fact]
		public async Task DeleteStock()
		{
			var result = await instanceToTest.DeleteMassAsync();
			Assert.True(result == UnitConsts.TOTAL_DELETED);
		}

		[Fact]
		public async Task CountStock()
		{
			var result = await instanceToTest.CountRowsInStock();
			Assert.True(result == UnitConsts.TOTAL_DELETED);
		}


	}
}
