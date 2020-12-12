using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CsvImporter.DataAccess.Base.Definitions
{
	public interface IDapperBase<T> where T : class
	{
		Task<int> DeleteAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null);

		Task<int> EditAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null);

		Task<int> ExecuteCommandAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null);

		Task<int> ExecuteEscalarAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null);
		Task<IEnumerable<T>> GetAllAsync(string sql, object parameters = null, SqlTransaction transaction = null, int? commandTimeOut = null);

		Task<T> GetByIdAsync(string sql, object parameters = null);

		Task<int> SaveAsync(string sql, object parameters = null);

		Task<IEnumerable<T>> GetByIdWithRelationsAsync<TRelation1>(string sql, Func<T, TRelation1, IDictionary<int, T>, T> mapperBody, string splitOn, object parameters = null);
		Task<SqlConnection> GetConnection();
	}
}
