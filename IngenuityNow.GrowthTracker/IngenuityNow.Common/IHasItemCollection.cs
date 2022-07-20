using System.Collections;

namespace IngenuityNow.Common
{
    /// <summary>
    /// Base interface for any object that has an item collection.
    /// </summary>
    public interface IHasItemCollection
    {
        /// <summary>
        /// The collection of items for this object.
        /// </summary>
        IDictionary Items { get; }
    }
}
