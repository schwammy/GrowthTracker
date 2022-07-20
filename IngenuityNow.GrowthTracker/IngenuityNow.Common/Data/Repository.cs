using System;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Base class for a simple repository that has a DbContext.
    /// </summary>
    /// <typeparam name="TDbContext">The type of DbContext this repository uses.</typeparam>
    public class Repository<TDbContext> where TDbContext : class, IDbContext
    {
        /// <summary>
        /// The DbContext for the repo, it is pulled from the UnitOfWork.
        /// </summary>
        protected readonly TDbContext DbContext;

        /// <summary>
        ///Initializes a new instance of the <see cref="Repository{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">A DbContext.</param>
        public Repository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
