using System.Threading;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Base interface for a unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">The type of DbContext for this unit of work.</typeparam>
    public interface IUnitOfWork<out TDbContext>
    {
        /// <summary>
        /// Saves all changes in this unit of work.
        /// </summary>
        /// <param name="cancellationToken">(Optional) Token used for cancellation.</param>
        /// <returns>Nothing.</returns>
        Task SaveAllAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Base class for a unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">The type of DbContext for this unit of work.</typeparam>
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : IDbContext

    {
        private readonly TDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The DbContext for this unit of work.</param>
        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task SaveAllAsync(CancellationToken cancellationToken = default)
        {
            if (_dbContext is IWriteableDbContext writableDataContext)
                await writableDataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
