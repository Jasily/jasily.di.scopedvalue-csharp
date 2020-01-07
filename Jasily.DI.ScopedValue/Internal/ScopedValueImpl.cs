using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jasily.DI.ScopedValue.Internal
{
    internal class ScopedValueImpl<T> : IScopedValue<T> where T : class
    {
        private readonly IServiceProvider _serviceProvider;

        public ScopedValueImpl(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public T? GetValue() => this._serviceProvider.GetRequiredService<ScopedValuesStore>().GetValue(typeof(T)) as T;
    }
}
