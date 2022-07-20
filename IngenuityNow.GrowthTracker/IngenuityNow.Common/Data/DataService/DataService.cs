using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data.DataService
{
    /// <summary>
    /// A simple data service for reading and writing entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type for the data service.</typeparam>
    public interface IDataService<TEntity> : IReadOnlyDataService<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="entity">The item to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="keyValues">The key values of the item to delete.</param>
        /// <returns>Whether the deletion succeeded.</returns>
        Task<bool> DeleteAsync(params object[] keyValues);
    }

    /// <summary>
    /// A simple data service for reading and writing entities.
    /// </summary>
    /// <typeparam name="T">The entity type for the data service.</typeparam>
    public class DataService<T> : ReadOnlyDataService<T>, IDataService<T> where T : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataService{T}"/> class.
        /// </summary>
        /// <param name="repository">The repository to use.</param>
        public DataService(IGenericRepository<T> repository)
            : base(repository)
        { }

        /// <summary>
        /// The repository for this data service.
        /// </summary>
        // TODO: Use covariant return type and override base property when moving to C# 9.0 or higher
        protected new IGenericRepository<T> Repository => (IGenericRepository<T>)base.Repository;

        /// <inheritdoc />
        public virtual void Add(T user)
        {
            Repository.InsertOrUpdate(user);
        }

        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await Repository.DeleteAsync(keyValues);
        }
    }
}
