using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jasily.DI.ScopedValue.Internal
{
    internal class ScopedValueImpl
    {
        public static object? GetScopedValue(IServiceProvider serviceProvider, Type type) =>
            serviceProvider.GetRequiredService<ScopedValuesStore>().GetValue(type);
    }

    internal class ScopedValueImpl<T> : IScopedValue<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public ScopedValueImpl(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public T? GetValue() => ScopedValueImpl.GetScopedValue(this._serviceProvider, typeof(T)) as T;
    }
}
