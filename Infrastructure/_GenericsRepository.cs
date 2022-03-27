using Dapper;
using Infrastructure.DataConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class _GenericsRepository<T> : _IGenericsRepository<T> where T : class
    {
        private readonly SqlConnection db = new SqlConnection();
        private CommandType commandType;
        private readonly int TimeoutTime = 9999999;
        public _GenericsRepository()
        {
            db = Connection.DbConnection();
        }

        /// <summary>
        ///    Query Record
        /// </summary>
        /// <param name="param">Values need to be pass in sql</param>
        /// <param name="queryText">Query Text or Stored Procedure name</param>
        /// <param name="isSp">if using store procedure please make true of this.</param>
        /// <returns>return set of records </returns>
        public async Task<IEnumerable<T>> Query(object param, string queryText, bool isSp = false)
        {
            try
            {
                commandType = isSp ? CommandType.StoredProcedure : CommandType.Text;
                return await db.QueryAsync<T>(queryText, param, commandType: commandType, commandTimeout: TimeoutTime);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///    Query Single
        /// </summary>
        /// <param name="param">Values need to be pass in sql</param>
        /// <param name="queryText">Query Text or Stored Procedure name</param>
        /// <param name="isSp">if using store procedure please make true of this.</param>
        /// <returns>return single records </returns>
        public async Task<T> QuerySingle(object param, string queryText, bool isSp = false)
        {
            try
            {
                commandType = isSp ? CommandType.StoredProcedure : CommandType.Text;
                return await db.QueryFirstAsync<T>(queryText, param, commandType: commandType, commandTimeout: TimeoutTime);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///   Execute
        /// </summary>
        /// <param name="param">Values need to be pass in sql</param>
        /// <param name="queryText">Query Text or Stored Procedure name</param>
        /// <param name="isSp">if using store procedure please make true of this.</param>
        /// <returns></returns>
        public async Task Execute(object param, string queryText, bool isSp = false)
        {
            try
            {
                commandType = isSp ? CommandType.StoredProcedure : CommandType.Text;
                await db.ExecuteAsync(queryText, param, commandType: commandType, commandTimeout: TimeoutTime);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
