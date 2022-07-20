using IngenuityNow.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// A generic repository that is read-only.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity stored in the repository.</typeparam>
    /// <typeparam name="TDbContext">The type of DbContext used by the repository.</typeparam>
    public class GenericReadOnlyRepository<TEntity, TDbContext> : Repository<TDbContext>, IGenericReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : class, IDbContext
    {
        /// <summary>
        /// The DbSet that the repository is based on.
        /// </summary>
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericReadOnlyRepository{TEntity, TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The DbContext for this repository.</param>
        public GenericReadOnlyRepository(TDbContext dbContext) : base(dbContext)
        {
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> All
        {
            get { return DbSet; }
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> AllIncluding(params string[] properties)
        {
            return DbSet.Include(properties);
        }

        /// <inheritdoc/>
        public IIncludableQueryable<TEntity, TProperty> AllIncluding<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            return DbSet.Include(propertySelector);
        }

        /// <inheritdoc/>
        public async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(keyValues, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return FindAsync(keyValues, default);
        }
    }
}