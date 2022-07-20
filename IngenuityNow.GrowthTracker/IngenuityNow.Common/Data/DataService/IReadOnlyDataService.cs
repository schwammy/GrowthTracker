using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IngenuityNow.Common.Data.DataService
{
    /// <summary>
    /// A simple data service for read-only data.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for the data service.</typeparam>
    public interface IReadOnlyDataService<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets an entity by its key values.
        /// </summary>
        /// <param name="keyValues">The key values of the entity to get.</param>
        /// <returns>A single entity.</returns>
        Task<TEntity> GetAsync(params object[] keyValues);

        /// <summary>
        /// Lists data, returns all items.
        /// </summary>
        /// <returns>a list of entities</returns>
        Task<List<TEntity>> ListAsync();
    }

    /// <summary>
    /// A simple data service for read-only data.
    /// </summary>
    /// <typeparam name="T">The entity type for the data service.</typeparam>
    public class ReadOnlyDataService<T> : IReadOnlyDataService<T> where T : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyDataService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository to use.</param>
        public ReadOnlyDataService(IGenericReadOnlyRepository<T> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// The repository for this data service.
        /// </summary>
        protected virtual IGenericReadOnlyRepository<T> Repository { get; private set; }

        /// <inheritdoc />
        public virtual async Task<T> GetAsync(params object[] keyValues)
        {
            return await Repository.FindAsync(keyValues);
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> ListAsync()
        {
            return await Repository.All.ToListAsync();
        }
    }
}
