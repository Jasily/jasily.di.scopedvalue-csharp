using System;

namespace Jasily.DI.ScopedValue
{
    public interface IScopedValue<T> where T : class
    {
        T? GetValue();

        T GetRequiredValue() => this.GetValue() ?? throw new InvalidOperationException();
    }
}
