using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Threading; // test
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// An interface for DbContext.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Gets the Set<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">a class</typeparam>
        /// <returns>DbSet<typeparamref name="T"/></returns>
        DbSet<T> Set<T>() where T : class;

        /// <summary>
        /// The DbConnection used by the DbContext.
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// The Database used by the DbContext.
        /// </summary>
        DatabaseFacade Database { get; }
    }

    /// <summary>
    /// Interface for DbContext that supports write (add, update, delete).
    /// </summary>
    public interface IWriteableDbContext : IDbContext
    {
        /// <summary>
        /// Save all changes to the DbContext.
        /// </summary>
        /// <returns>The number of records affected.</returns>
        int SaveChanges();

        /// <summary>
        /// Save all changes to the DbContext asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token used for cancellation.</param>
        /// <returns>The number of records affected.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
