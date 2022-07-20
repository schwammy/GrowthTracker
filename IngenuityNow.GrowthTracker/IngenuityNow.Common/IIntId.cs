namespace IngenuityNow.Common
{
    /// <summary>
    /// An interface for classes that have an int property named "Id", represending the Id of the object.
    /// </summary>
    public interface IIntId
    {
        /// <summary>
        /// The Id of the object.
        /// </summary>
        int Id { get; set; }
    }
}
