using System;

namespace IngenuityNow.Common
{
    /// <summary>
    /// Interface for Parsing Enums - finding the enum value for a provided string
    /// </summary>
    public interface IEnumParser
    {
        /// <summary>
        /// Find the string value in an Enum
        /// </summary>
        /// <typeparam name="T">The Enum</typeparam>
        /// <param name="value">The string value to convert</param>
        /// <returns>The value from the enum</returns>
        /// <exception>ArgumentException</exception>
        T ParseEnum<T>(string value) where T : struct, IConvertible;
        /// <summary>
        /// Find the string value in an Enum
        /// </summary>
        /// <typeparam name="T">The Enum</typeparam>
        /// <param name="value">The string value to convert</param>
        /// <param name="result">The resulting Enum value</param>
        /// <returns>The value from the enum </returns>
        bool TryParseEnum<T>(string value, out T result) where T : struct, IConvertible;
    }

    /// <summary>
    /// Parsing Enums - finding the enum value for a provided string
    /// </summary>
    public class EnumParser : IEnumParser
    {
        /// <summary>
        /// Find the string value in an Enum
        /// </summary>
        /// <typeparam name="T">The Enum</typeparam>
        /// <param name="value">The string value to convert</param>
        /// <returns>The value from the enum</returns>
        /// <exception>ArgumentException</exception>
        public T ParseEnum<T>(string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Tries to find the string value in an Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string value to convert</param>
        /// <param name="result">The enum value if found</param>
        /// <returns>true if found, false if not</returns>
        public bool TryParseEnum<T>(string value, out T result) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            return Enum.TryParse(value, true, out result);
        }
    }
}
