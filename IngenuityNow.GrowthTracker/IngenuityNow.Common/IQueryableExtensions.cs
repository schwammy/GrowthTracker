using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace IngenuityNow.Common
{
    /// <summary>
    /// A set of extension methods to make working with Queryables easier.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Specifies related entities to include in the query results. The navigation property
        /// to be included is specified starting with the type of entity being queried (TEntity).
        /// Further navigation properties to be included can be appended, separated by the
        /// '.' character.
        /// </summary>
        /// <typeparam name="T">The type of entity being queried.</typeparam>
        /// <param name="query">The source query.</param>
        /// <param name="properties">A list of navigation properties to be included.</param>
        /// <returns>A new query with the related data included.</returns>
        public static IQueryable<T> Include<T>(this IQueryable<T> query, params string[] properties) where T : class
        {
            foreach (var property in properties)
                query = EntityFrameworkQueryableExtensions.Include(query, property);
            return query;
        }

        /// <summary>
        /// Use to filter a Queryable based on EffectiveDate and EndDate. It will return items that are effective on the current date.
        /// </summary>
        /// <typeparam name="T">class, IEffectiveDate</typeparam>
        /// <param name="query">The IQueryable that is being filtered</param>
        /// <returns>An IEnumerable of items effective for the current date</returns>
        public static IQueryable<T> IsEffective<T>(this IQueryable<T> query) where T : class, IEffectiveDate
        {
            return query.IsEffective(DateTime.Today);
        }

        /// <summary>
        /// Use to filter a Queryable based on EffectiveDate and EndDate. It will return items that are effective on the date supplied.
        /// </summary>
        /// <typeparam name="T">class, IEffectiveDate</typeparam>
        /// <param name="query">The IQueryable that is being filtered</param>
        /// <param name="date">The date to check</param>
        /// <returns>An IEnumerable of items effective for the current date</returns>
        public static IQueryable<T> IsEffective<T>(this IQueryable<T> query, DateTime date) where T : class, IEffectiveDate
        {
            return query.Where(a => a.EffectiveDate <= date && (a.EndDate == null || a.EndDate > date));
        }

        /// <summary>
        /// Sorts a Queryable by whatever property is provided
        /// </summary>
        /// <typeparam name="T">Any object</typeparam>
        /// <param name="source">The queryable to sort</param>
        /// <param name="property">A string representing the name of the property for sorting</param>
        /// <param name="descending">Boolean for descending or ascending. Default is descending</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, bool? descending = true)
        {
            string methodName = descending == false ? "OrderBy" : "OrderByDescending";
            return ApplyOrder(source, property, methodName);
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                var list = type.GetProperties();
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}