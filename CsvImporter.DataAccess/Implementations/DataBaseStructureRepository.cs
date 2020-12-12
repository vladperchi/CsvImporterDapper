using CsvImporter.DataAccess.Base.Definitions;
using CsvImporter.Domain;
using Dapper;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.Implementations
{
	public class DataBaseStructureRepository : IDataBaseStructureRepository
	{
		public DataBaseStructureRepository(IDapperBase<StockProduct> dapperBase)
		{
			_dapperBase = dapperBase;
		}

		IDapperBase<StockProduct> _dapperBase { get; }

		public async Task<int> CreateStockProductTableAsync()
		{
			var dataBaseCreated = default(int);
			using (var connection = await _dapperBase.GetConnection())
			{
				var dataBaseName = connection.Database;
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					dataBaseCreated = connection.Execute(SqlStatements.Create_StockProductTable, null, transaction);
					transaction.Commit();
				}

			}
			return dataBaseCreated;
		}
	}
}
