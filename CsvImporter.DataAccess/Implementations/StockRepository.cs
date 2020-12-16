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
			DapperBase = dapperBase;
		}

		IDapperBase<StockProduct> DapperBase { get; }
		public async Task<int> SaveMassStockAsync(string filePath)
		{
			try
			{
				var rowsQuantity = default(int);
				using (var connection = await DapperBase.GetConnection())
				{
					connection.Open();
					using var transaction = connection.BeginTransaction();
					rowsQuantity = connection.Execute(SqlStatements.SaveBulkData, new
					{
						path = filePath
					}, transaction, 0);
					transaction.Commit();

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
			try
			{
				int affectedRows = await DapperBase.DeleteAsync(SqlStatements.DeleteMassStock, null, null, 0);
				return affectedRows;
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
		}

		public async Task<int> CountRowsInStock()
		{
			int totalCount;
			try
			{
				totalCount = await DapperBase.ExecuteEscalarAsync(SqlStatements.CountStock);
			}
			catch (System.Exception ex)
			{

				throw ex;
			}

			return totalCount;
		}
	}
}
