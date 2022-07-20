namespace IngenuityNow.Common
{
    /// <summary>
    /// An interface to specify that a class has a range with start and end values. Values can be int, double, date, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRange<T>
    {
        /// <summary>
        /// The starting value for the range.
        /// </summary>
        T RangeStart { get; set; }

        /// <summary>
        /// The ending value for the range.
        /// </summary>
        T RangeEnd { get; set; }
    }
}
