using Microsoft.EntityFrameworkCore;
using System;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// A helper class for converting <see cref="ObjectState"/> to <see cref="EntityState"/>.
    /// </summary>
    public static class StateHelper
    {
        /// <summary>
        /// Converts <see cref="ObjectState"/> to <see cref="EntityState"/>
        /// </summary>
        /// <param name="state">The state to convert.</param>
        /// <returns>The appropriate <see cref="EntityState"/>.</returns>
        public static EntityState ConvertState(ObjectState state)
        {
            return state switch
            {
                ObjectState.Added => EntityState.Added,
                ObjectState.Modified => EntityState.Modified,
                ObjectState.Deleted => EntityState.Deleted,
                _ => EntityState.Unchanged
            };
        }

        /// <summary>
        /// Converts <see cref="EntityState"/> to <see cref="ObjectState"/>
        /// </summary>
        /// <param name="state">The state to convert.</param>
        /// <returns>The appropriate <see cref="ObjectState"/>.</returns>
        public static ObjectState ConvertState(EntityState state)
        {
            return state switch
            {
                EntityState.Detached => ObjectState.Unchanged,
                EntityState.Unchanged => ObjectState.Unchanged,
                EntityState.Added => ObjectState.Added,
                EntityState.Deleted => ObjectState.Deleted,
                EntityState.Modified => ObjectState.Modified,
                _ => throw new ArgumentOutOfRangeException(nameof(state))
            };
        }
    }
}
