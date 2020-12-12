using CsvImporter.DataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Tests.UnitTest.RepositoriesMock
{
	public static class StockRepositoryMock
	{
		public static Mock<IStockRepository> GetStockMock()
		{
			var stockRepository = new Mock<IStockRepository>();
			stockRepository.Setup(c => c.SaveMassStockAsync(It.IsAny<string>()))
				.Returns<string>
				((filePath) =>
				{
					int totalInserted = 0;
					if (!string.IsNullOrEmpty(filePath))
					{
						totalInserted = UnitConsts.TOTAL_ROW;

					}
					return Task.FromResult(UnitConsts.TOTAL_ROW);
				});

			stockRepository.Setup(c => c.DeleteMassAsync())
				.Returns(Task.FromResult(UnitConsts.TOTAL_ROW));

			stockRepository.Setup(c => c.CountRowsInStock())
				.Returns(Task.FromResult(UnitConsts.TOTAL_ROW));

			return stockRepository;
		}
	}
}
