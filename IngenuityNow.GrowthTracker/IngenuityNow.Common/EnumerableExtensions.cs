using System;
using System.Collections.Generic;
using System.Linq;

namespace IngenuityNow.Common
{
    /// <summary>
    /// A set of extension methods to make Enumerables more useful.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Use to query an enumerable based on EffectiveDate and EndDate. It will return items that are effective for the date supplied.
        /// </summary>
        /// <typeparam name="T">class, IEffectiveDate</typeparam>
        /// <param name="query">the Enumerable that is being filtered</param>
        /// <param name="date">the date to check</param>
        /// <returns>An IEnumerable of items matching the effective date</returns>
        public static IEnumerable<T> IsEffective<T>(this IEnumerable<T> query, DateTime date) where T : class, IEffectiveDate
        {
            return query.Where(a => a.EffectiveDate <= date && (a.EndDate == null || a.EndDate > date));
        }

        /// <summary>
        /// For IEnumerable&lt;IRange&gt;, this will return items where the parameter is between the start and end of the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IEnumerable&lt;IRange&gt;</param>
        /// <param name="value">The value being searched for</param>
        /// <returns>IEnumerable&lt;IRange&gt; that matches the filter value</returns>
        public static IEnumerable<T> InRange<T>(this IEnumerable<T> query, int? value) where T : class, IRange<int?>
        {
            return query.Where(a => value >= a.RangeStart && (value <= a.RangeEnd || !a.RangeEnd.HasValue));
        }

        /// <summary>
        /// For IEnumerable&lt;IRange&gt;, this will return items where the parameter is between the start and end of the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IEnumerable&lt;IRange&gt;</param>
        /// <param name="value">The value being searched for</param>
        /// <returns>IEnumerable&lt;IRange&gt; that matches the filter value</returns>
        public static IEnumerable<T> InRange<T>(this IEnumerable<T> query, int value) where T : class, IRange<int>
        {
            return query.Where(a => value >= a.RangeStart && value <= a.RangeEnd);
        }
        /// <summary>
        /// For IEnumerable&lt;IRange&gt;, this will return items where the parameter is between the start and end of the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IEnumerable&lt;IRange&gt;</param>
        /// <param name="value">The value being searched for</param>
        /// <returns>IEnumerable&lt;IRange&gt; that matches the filter value</returns>
        public static IEnumerable<T> InRange<T>(this IEnumerable<T> query, double? value) where T : class, IRange<double?>
        {
            return query.Where(a => value >= a.RangeStart && (value <= a.RangeEnd || !a.RangeEnd.HasValue));
        }
        /// <summary>
        /// For IEnumerable&lt;IRange&gt;, this will return items where the parameter is between the start and end of the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IEnumerable&lt;IRange&gt;</param>
        /// <param name="value">The value being searched for</param>
        /// <returns>IEnumerable&lt;IRange&gt; that matches the filter value</returns>
        public static IEnumerable<T> InRange<T>(this IEnumerable<T> query, double value) where T : class, IRange<double>
        {
            return query.Where(a => value >= a.RangeStart && value <= a.RangeEnd);
        }

        /// <summary>
        /// Recursively selects the item resulting from the given selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// Selects the item resulting from the given selector.
        /// </returns>
        public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source,
                                                                    Func<TSource, IEnumerable<TSource>> selector)
        {
            foreach (var item in source)
            {
                yield return item;
                foreach (var child in selector(item).SelectRecursive(selector))
                {
                    yield return child;
                }
            }
        }
    }
}
