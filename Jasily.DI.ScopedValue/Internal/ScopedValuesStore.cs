using System;
using System.Collections.Generic;

namespace Jasily.DI.ScopedValue.Internal
{
    internal class ScopedValuesStore
    {
        private readonly Dictionary<Type, object> _store = new Dictionary<Type, object>();

        internal void SetValue(Type type, object value) => this._store[type] = value;

        internal object? GetValue(Type type) => this._store.TryGetValue(type, out var v) ? v : null;
    }
}
