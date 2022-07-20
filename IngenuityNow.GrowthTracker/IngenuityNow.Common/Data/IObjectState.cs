namespace IngenuityNow.Common.Data
{
    /// <summary>
    /// Interface for objects that have an ObjectState property, used with Repository to track Added, Modified, Unchanged, Deleted.
    /// </summary>
    public interface IObjectState
    {
        /// <summary>
        /// The state of the object according to Entity Framework.
        /// </summary>
        ObjectState ObjectState { get; set; }
    }
}
