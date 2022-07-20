using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Repository that supports executing stored procedures.
    /// </summary>
    /// <typeparam name="TDbContext">The type of DbContext for this repository.</typeparam>
    public class StoredProcedureRepository<TDbContext> : Repository<TDbContext>, IStoredProcedureRepository where TDbContext : class, IDbContext
    {
        /// <summary>
        /// Constructor for a StoredProcedureRepository
        /// </summary>
        /// <param name="dbContext">A DbContext.</param>
        public StoredProcedureRepository(TDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        /// Executes a stored procedure that returns a result set.
        /// </summary>
        /// <typeparam name="TResult">The type of the result set of Entities from the data source.</typeparam>
        /// <param name="storedProcName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An IEnumerable of SqlParameters to pass to the stored procedure.</param>
        /// <returns>The data set returned from the stored procedure.</returns>
        public List<TResult> ExecuteStoredProc<TResult>(string storedProcName, IEnumerable<SqlParameter> parameters) where TResult : new()
        {
            List<TResult> results = new List<TResult>();

            var entityConnection = DbContext.DbConnection;

            var initialState = entityConnection.State;

            if (initialState != ConnectionState.Open)
                entityConnection.Open();
            using (var cmd = entityConnection.CreateCommand())
            {
                cmd.CommandText = storedProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TResult tempObject = new TResult();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i) != DBNull.Value)
                        {
                            PropertyInfo propertyInfo = typeof(TResult).GetProperty(reader.GetName(i).Replace("-", "_").Replace(" ", ""), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            propertyInfo.SetValue(tempObject, reader.GetValue(i), null);
                        }
                    }

                    results.Add(tempObject);
                }

                if (reader.NextResult())
                    throw new Exception("Too many result sets returned from DB");
            }

            return results;
        }

        /// <summary>
        /// Executes a stored procedure that returns a scalar value.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return.</typeparam>
        /// <param name="storedProcName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An IEnumerable of SqlParameters to pass to the stored procedure.</param>
        /// <returns>A scalar value returned by the stored procedure of type <typeparamref name="TResult" />.</returns>
        public TResult ExecuteScalarStoredProc<TResult>(string storedProcName, IEnumerable<SqlParameter> parameters)
        {
            var entityConnection = DbContext.DbConnection;

            var initialState = entityConnection.State;

            if (initialState != ConnectionState.Open)
                entityConnection.Open();
            using var cmd = entityConnection.CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }

            var r = cmd.ExecuteScalar();

            return (TResult)r;
        }
    }
}
