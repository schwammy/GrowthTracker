using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Interface for repositories that support executing stored procedures.
    /// </summary>
    public interface IStoredProcedureRepository
    {
        /// <summary>
        /// Executes a stored procedure that returns a result set.
        /// </summary>
        /// <typeparam name="TResult">The type of the result set of Entities from the data source.</typeparam>
        /// <param name="storedProcName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An IEnumerable of SqlParameters to pass to the stored procedure.</param>
        /// <returns>The data set returned from the stored procedure.</returns>
        List<TResult> ExecuteStoredProc<TResult>(string storedProcName, IEnumerable<SqlParameter> parameters)
            where TResult : new();

        /// <summary>
        /// Executes a stored procedure that returns a scalar value.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return.</typeparam>
        /// <param name="storedProcName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An IEnumerable of SqlParameters to pass to the stored procedure.</param>
        /// <returns>A scalar value returned by the stored procedure of type <typeparamref name="TResult"/></returns>
        TResult ExecuteScalarStoredProc<TResult>(string storedProcName, IEnumerable<SqlParameter> parameters);
    }
}
