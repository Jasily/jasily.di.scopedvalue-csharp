using Jasily.DI.ScopedValue.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasily.DI.ScopedValue
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IServiceScope"/> with give <paramref name="values"/>.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IServiceScope CreateScopeWithValues(this IServiceProvider serviceProvider,
            params (Type, object)[] values)
        {
            return CreateScopeWithValues(serviceProvider, values.Select(z => new KeyValuePair<Type, object>(z.Item1, z.Item2)));
        }

        /// <summary>
        /// Creates a new <see cref="IServiceScope"/> with give <paramref name="values"/>.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IServiceScope CreateScopeWithValues(this IServiceProvider serviceProvider, 
            IEnumerable<KeyValuePair<Type, object>> values)
        {
            if (serviceProvider is null)
                throw new ArgumentNullException(nameof(serviceProvider));

            var scope = serviceProvider.CreateScope();
            var store = scope.ServiceProvider.GetRequiredService<ScopedValuesStore>();
            foreach (var item in values)
            {
                if (item.Key is null || item.Value is null)
                    throw new ArgumentNullException($"items of {nameof(values)} cannot be null.");

                store.SetValue(item.Key, item.Value);
            }
            return scope;
        }

        /// <summary>
        /// Try add a <see cref="IScopedValue{T}"/> service to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddScopedValue<T>(this IServiceCollection serviceCollection) where T : class =>
            AddScopedValue(serviceCollection, typeof(T));

        /// <summary>
        /// Try add a <see cref="IScopedValue{T}"/> service to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static IServiceCollection AddScopedValue(this IServiceCollection serviceCollection, Type serviceType)
        {
            if (serviceCollection is null)
                throw new ArgumentNullException(nameof(serviceCollection));
            if (serviceType is null)
                throw new ArgumentNullException(nameof(serviceType));

            serviceCollection.TryAddScoped<ScopedValuesStore>();
            serviceCollection.TryAddScoped(typeof(IScopedValue<>), typeof(ScopedValueImpl<>));
            serviceCollection.TryAddScoped(serviceType, p => ScopedValueImpl.GetScopedValue(p, serviceType));

            return serviceCollection;
        }
    }
}
