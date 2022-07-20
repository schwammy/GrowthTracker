using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Interface for repositories that support asynchronous read access to data via All or AllIncluding.
    /// </summary>
    /// <typeparam name="TEntity">Data type for the repo.</typeparam>
    public interface IGenericReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Gets all data from the repository.
        /// </summary>
        IQueryable<TEntity> All { get; }

        /// <summary>
        /// Gets all data from the repository including populating specified properties.
        /// </summary>
        /// <param name="properties">The properties to load.</param>
        /// <returns>An IQueryable<typeparamref name="TEntity"/>.</returns>
        IQueryable<TEntity> AllIncluding(params string[] properties);

        /// <summary>
        /// Gets all data from the repository including populating specified properties.
        /// </summary>
        /// <param name="propertySelector">An expression selecting the property to load.</param>
        /// <typeparam name="TProperty">The type of the property to load.</typeparam>
        /// <returns>An IQueryable<typeparamref name="TEntity"/>.</returns>
        IIncludableQueryable<TEntity, TProperty> AllIncluding<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector);

        /// <summary>
        /// Finds an entity in the repository using the given key values.
        /// </summary>
        /// <param name="keyValues">The key values of the entity to search for.</param>
        /// <param name="cancellationToken">(Optional) A cancellation token to cancel the search.</param>
        /// <returns>The entity</returns>
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds an entity in the repository using the given key values.
        /// </summary>
        /// <param name="keyValues">The key values of the entity to search for.</param>
        /// <returns>The entity</returns>
        Task<TEntity> FindAsync(params object[] keyValues);
    }
}
