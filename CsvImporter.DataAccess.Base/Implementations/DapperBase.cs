using CsvImporter.DataAccess.Base.Definitions;
using CsvImporter.Domain.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.Base.Implementations
{
	public class DapperBase<T> : IDapperBase<T> where T : class
	{
		private SqlConnection sqlConnection;
		private IApplicationSettingsManager applicationSettingsManager { get; }
		public DapperBase(IApplicationSettingsManager applicationSettingsManager)
		{
			this.applicationSettingsManager = applicationSettingsManager;
		}


		public async Task<int> DeleteAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				int affectedRows = await sqlConnection.ExecuteAsync(sql, parameters, transaction, commandTimeOut);
				return affectedRows;
			}
		}

		public async Task<int> EditAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				int affectedRows = await sqlConnection.ExecuteAsync(sql, parameters, transaction, commandTimeOut);
				return affectedRows;
			}
		}

		public async Task<int> ExecuteCommandAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				int affectedRows = await sqlConnection.ExecuteAsync(sql, parameters, transaction, commandTimeOut);
				return affectedRows;
			}
		}

		public async Task<int> ExecuteEscalarAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				int affectedRows = await sqlConnection.ExecuteScalarAsync<int>(sql, parameters, transaction, commandTimeOut);
				return affectedRows;
			}
		}


		public async Task<IEnumerable<T>> GetAllAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				IEnumerable<T> objects = await sqlConnection.QueryAsync<T>(sql, parameters, transaction, commandTimeOut);
				return objects;
			}
		}

		public async Task<T> GetByIdAsync(string sql, object parameters = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				T model = await sqlConnection.QuerySingleOrDefaultAsync<T>(sql, parameters);
				return model;
			}
		}

		public async Task<int> SaveAsync(string sql, object parameters = null)
		{
			using (sqlConnection = await GetSqlConnectionAsync())
			{
				int createdId = await sqlConnection.QuerySingleOrDefaultAsync<int>(sql, parameters);
				return createdId;
			}
		}
		public Task<SqlConnection> GetConnection()
		{
			return GetSqlConnectionAsync();

		}

		private async Task<SqlConnection> GetSqlConnectionAsync()
		{
			var stringConnection = await applicationSettingsManager.GetConnectionStringValuebyKey(Constans.CONNECTION_STRING_KEY);
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(stringConnection);
			var targetDataBase = builder.InitialCatalog;
			await CreateDataBaseIfNotExist(builder);
			builder.InitialCatalog = targetDataBase;
			SqlConnection Connection = new SqlConnection(builder.ConnectionString);
			return Connection;
		}

		private async Task CreateDataBaseIfNotExist(SqlConnectionStringBuilder builder)
		{
			var dataBaseName = builder.InitialCatalog;
			builder.InitialCatalog = Constans.DATABASE_MASTER;
			SqlConnection Connection = new SqlConnection(builder.ConnectionString);
			await Connection.ExecuteAsync(SqlStatementBase.CreateDataBase, new
			{
				dataBaseName = dataBaseName
			});
		}

		public async Task<IEnumerable<T>> GetByIdWithRelationsAsync<TRelation1>(string sql, Func<T, TRelation1, IDictionary<int, T>, T> mapperBody, string splitOn, object parameters = null)
		{
			IEnumerable<T> model = null;
			using (SqlConnection sqlConnection = await GetSqlConnectionAsync())
			{
				var modelRelations = new Dictionary<int, T>();
				var response = await sqlConnection.QueryAsync<T, TRelation1, T>(sql,
				   (pd, pp) =>
				   {
					   var modelRow = mapperBody(pd, pp, modelRelations);
					   return modelRow;
				   }, splitOn: splitOn, param: parameters);
				model = modelRelations.Values.AsEnumerable();
			}
			return model;
		}

	}
}
