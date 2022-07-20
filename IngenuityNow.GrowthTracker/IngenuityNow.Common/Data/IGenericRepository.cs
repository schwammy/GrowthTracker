using System.Threading;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Interface for repositories that have asynchronous read/write access to data.
    /// </summary>
    /// <typeparam name="TEntity">Data type for the repository.</typeparam>
    public interface IGenericRepository<TEntity> : IGenericReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Adds an entity to a repository or updated it, if it already exists.
        /// </summary>
        /// <param name="entity">The entity to add or update.</param>
        /// <param name="forceSave">(Optional) Will save changes to the DataContext immediately, without the need to call SaveChanges().</param>
        void InsertOrUpdate(TEntity entity, bool forceSave = false);

        /// <summary>
        /// Deletes an entity matching the given key values.
        /// </summary>
        /// <param name="keyValues">The key values of the entity to delete.</param>
        /// <param name="cancellationToken">(Optional) Token used for cancellation.</param>
        /// <returns>A boolean value, true if the item is deleted, false if it is not.</returns>
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity matching the given key values.
        /// </summary>
        /// <param name="keyValues">The key values of the entity to delete.</param>
        /// <returns>A boolean value, true if the item is deleted, false if it is not.</returns>
        Task<bool> DeleteAsync(params object[] keyValues);
    }
}
