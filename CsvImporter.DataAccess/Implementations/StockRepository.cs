using CsvImporter.DataAccess.Base.Definitions;
using CsvImporter.Domain;
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
			try
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
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		public async Task<int> DeleteMassAsync()
		{
			var affectedRows = 0;
			try
			{

				affectedRows = await _dapperBase.DeleteAsync(SqlStatements.DeleteMassStock, null, null, 0);
				return affectedRows;
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		public async Task<int> CountRowsInStock()
		{
			var totalCount = 0;
			try
			{
				totalCount = await _dapperBase.ExecuteEscalarAsync(SqlStatements.CountStock);
			}
			catch (System.Exception ex)
			{

				throw ex;
			}

			return totalCount;
		}
	}
}
