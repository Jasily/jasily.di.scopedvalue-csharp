using Jasily.DI.ScopedValue.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasily.DI.ScopedValue
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceScope CreateScopeWithValues(this IServiceProvider serviceProvider,
            params (Type, object)[] values)
        {
            return CreateScopeWithValues(serviceProvider, values.Select(z => new KeyValuePair<Type, object>(z.Item1, z.Item2)));
        }

        public static IServiceScope CreateScopeWithValues(this IServiceProvider serviceProvider, 
            IEnumerable<KeyValuePair<Type, object>> values)
        {
            if (serviceProvider is null)
                throw new ArgumentNullException(nameof(serviceProvider));

            var scope = serviceProvider.CreateScope();
            var store = scope.ServiceProvider.GetRequiredService<ScopedValuesStore>();
            foreach (var item in values)
            {
                if (item.Value is null)
                    throw new ArgumentNullException("");

                store.SetValue(item.Key, item.Value);
            }
            return scope;
        }

        public static IServiceCollection AddScopedValue<T>(this IServiceCollection serviceCollection) where T : class
        {
            if (serviceCollection is null)
                throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.TryAddScoped<ScopedValuesStore>();
            serviceCollection.TryAddScoped(typeof(IScopedValue<>), typeof(ScopedValueImpl<>));
            serviceCollection.TryAddScoped(typeof(T), p => p.GetRequiredService<IScopedValue<T>>().GetValue());

            return serviceCollection;
        }
    }
}
