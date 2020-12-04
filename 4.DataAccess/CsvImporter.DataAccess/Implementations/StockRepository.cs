using CsvImporter.DataAccess.Base.Definitions;
using CsvImporter.DataAccess.Definitions;
using CsvImporter.Domain.Entities;
using Dapper;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.Implementations
{
	public class StockRepository : IStockRepository
	{
		public StockRepository(IDapperBase<StockProduct> dapperBase)
		{
			_dapperBase = dapperBase;
		}

		IDapperBase<StockProduct> _dapperBase { get; }
		public async Task<int> SaveMassStockAsync(string filePath)
		{
			var rowsQuantity = default(int);
			using (var connection = await _dapperBase.GetConnection())
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					rowsQuantity = connection.Execute(SqlStatements.SaveBulkData, new
					{
						path = filePath
					}, transaction, 0);
					transaction.Commit();
				}

			}
			return rowsQuantity;
		}

		public async Task<int> DeleteMassAsync()
		{
			var affectedRows = 0;
			affectedRows = await _dapperBase.DeleteAsync(SqlStatements.DeleteMassStock, null, null, 0);
			return affectedRows;
		}

		public async Task<int> CountRowsInStock()
		{
			var totalCount = 0;
			totalCount = await _dapperBase.ExecuteEscalarAsync(SqlStatements.CountStock);
			return totalCount;
		}
	}
}
