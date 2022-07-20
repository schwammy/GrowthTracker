namespace IngenuityNow.Common
{
    /// <summary>
    /// A set of extension methods to make using strings easier.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// A helper method that wraps string.IsNullOrWhiteSpace but easier to use. Instead of using String.IsNullOrEmpty("some string");
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <returns>A boolean indicating having a value vs null/empty</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// A helper method that wraps string.IsNullOrWhiteSpace and inverts it but easier to use. Instead of using !String.IsNullOrEmpty("some string");
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <returns>A boolean indicating having a null/empty vs a value</returns>
        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
