using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace IngenuityNow.Common
{
    /// <summary>
    /// Extensions for working with <see cref="Type"/> classes.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Get the default value of an instance of the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The default value.</returns>
        /// <exception cref="ArgumentNullException" />
        public static object GetDefaultValue(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// Gets a list of all the properties of this type that are decorated with a <see cref="KeyAttribute"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The key properties.</returns>
        /// <exception cref="ArgumentNullException" />
        public static IEnumerable<PropertyInfo> GetKeyProperties(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetProperties().Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)));
        }

        /// <summary>
        /// Returns the first instance of <typeparamref name="TAttribute"/> on this member.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type to check for.</typeparam>
        /// <param name="member">The member to check.</param>
        /// <returns>The attribute.</returns>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo member)
        {
            return member.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// Returns a value that indicates whether the given member is decorated with a <typeparamref name="TAttribute"/>.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type to check for.</typeparam>
        /// <param name="member">The member to check.</param>
        /// <returns>Whether the given member is decorated with a <typeparamref name="TAttribute"/>.</returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo member)
        {
            return member.GetCustomAttributes(typeof(TAttribute), true).Any();
        }

        /// <summary>
        /// Returns a value that indicates whether the type implements the given interface.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="interfaceType">The interface to check for.</param>
        /// <returns>Whether the type implements the given interface.</returns>
        public static bool Implements(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                       .Any(i => i == interfaceType ||
                                 i.IsGenericType && interfaceType.IsGenericType && i.GetGenericTypeDefinition() == interfaceType.GetGenericTypeDefinition());
        }

        /// <summary>
        /// Returns a value that indicates whether the type implements <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to check for.</typeparam>
        /// <param name="type">The type to check.</param>
        /// <returns>Whether the type implements <typeparamref name="T"/>.</returns>
        public static bool Implements<T>(this Type type)
        {
            return type.Implements(typeof(T));
        }

        /// <summary>
        /// Get the generic arguments for the interface of the given type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="interfaceBaseType">The type to get generic arguments for.</param>
        /// <returns>The generic arguments for the interface of the given type.</returns>
        public static Type[] GetGenericInterfaceArguments(this Type type, Type interfaceBaseType)
        {
            return type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceBaseType).GetGenericArguments();
        }

        /// <summary>
        /// Generate a delegate type for this method.
        /// </summary>
        /// <param name="method">The method to generate a delegate for.</param>
        /// <param name="target">The target to bind to.</param>
        /// <returns>The delegate for the method.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Delegate ToDelegate(this MethodInfo method, object target)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            Type delegateType;
            var typeArgs = method.GetParameters()
                .Select(p => p.ParameterType)
                .ToList();

            if (method.ReturnType == typeof(void))
            {
                delegateType = Expression.GetActionType(typeArgs.ToArray());
            }
            else
            {
                typeArgs.Add(method.ReturnType);
                delegateType = Expression.GetFuncType(typeArgs.ToArray());
            }

            var result = target == null
                ? Delegate.CreateDelegate(delegateType, method)
                : Delegate.CreateDelegate(delegateType, target, method);

            return result;
        }

        /// <summary>
        /// Generate a delegate type for this method.
        /// </summary>
        /// <param name="method">The method to generate a delegate for.</param>
        /// <returns>The delegate for the method.</returns>
        public static Delegate ToDelegate(this MethodInfo method)
        {
            return method.ToDelegate(null);
        }
    }
}
