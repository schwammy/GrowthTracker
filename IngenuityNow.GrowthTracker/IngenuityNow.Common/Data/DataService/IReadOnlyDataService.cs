using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        IIncludableQueryable<TEntity, TProperty> ListIncluding<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector);
    }

    /// <summary>
    /// A simple data service for read-only data.
    /// </summary>
    /// <typeparam name="T">The entity type for the data service.</typeparam>
    public class ReadOnlyDataService<TEntity> : IReadOnlyDataService<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyDataService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository to use.</param>
        public ReadOnlyDataService(IGenericReadOnlyRepository<TEntity> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// The repository for this data service.
        /// </summary>
        protected virtual IGenericReadOnlyRepository<TEntity> Repository { get; private set; }

        /// <inheritdoc />
        public virtual async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await Repository.FindAsync(keyValues);
        }

        /// <inheritdoc />
        public virtual async Task<List<TEntity>> ListAsync()
        {
            return await Repository.All.ToListAsync();
        }

        public virtual IIncludableQueryable<TEntity, TProperty> ListIncluding<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            if (propertySelector == null)
                throw new ArgumentNullException(nameof(propertySelector));
            return Repository.AllIncluding(propertySelector);
        }
    }
}
