using IngenuityNow.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// A generic repository that supports reading and writing data.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity stored in the repository.</typeparam>
    /// <typeparam name="TDbContext">The type of DbContext used by the repository.</typeparam>
    public class GenericRepository<TEntity, TDbContext> : GenericReadOnlyRepository<TEntity, TDbContext>, IGenericRepository<TEntity>
          where TEntity : class, IEntity
          where TDbContext : class, IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity, TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The DbContext for this repository.</param>
        public GenericRepository(TDbContext dbContext) : base(dbContext)
        { }

        /// <inheritdoc/>
        public void InsertOrUpdate(TEntity entity, bool forceSave = false)
        {
            if (entity.ObjectState == ObjectState.New || entity.GetKeyValues().All(key => key.Equals(key.GetType().GetDefaultValue())))
            {
                entity.ObjectState = ObjectState.Added;
                DbSet.Add(entity);
            }
            else
            {
                entity.ObjectState = ObjectState.Modified;
                DbSet.Attach(entity);
            }

            if (forceSave && DbContext is IWriteableDbContext writeableDbContext)
                writeableDbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var itemToRemove = await DbSet.FindAsync(keyValues, cancellationToken).ConfigureAwait(false);

            if (itemToRemove == null) return false;

            DbSet.Attach(itemToRemove);
            DbSet.Remove(itemToRemove);
            return true;
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(params object[] keyValues)
        {
            return DeleteAsync(keyValues, default);
        }
    }
}
