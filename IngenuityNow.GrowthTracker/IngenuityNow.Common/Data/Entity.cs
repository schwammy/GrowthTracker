using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Interface for entities.
    /// </summary>
    public interface IEntity : IObjectState
    {
        /// <summary>
        /// Get the key values for this entity.
        /// </summary>
        object[] GetKeyValues();
    }

    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        protected Entity()
        {
            ObjectState = ObjectState.Unchanged;
        }

        /// <inheritdoc />
        public object[] GetKeyValues() => GetType().GetKeyProperties().Select(key => key.GetValue(this)).ToArray();

        /// <inheritdoc />
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

    /// <summary>
    /// Base class for entities using an Id property as their key.
    /// </summary>
    /// <typeparam name="TKey">The type of key this entity uses.</typeparam>
    public abstract class IdEntity<TKey> : Entity
    {
        /// <summary>
        /// The ID of this entity.
        /// </summary>
        [Key] public TKey Id { get; set; }
    }

    /// <summary>
    /// Base class for entities using an integer Id property as their key.
    /// </summary>
    public abstract class IntegerIdEntity : IdEntity<int>
    { }
}
