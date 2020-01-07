using System;

namespace Jasily.DI.ScopedValue
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IScopedValue<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T? GetValue();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T GetRequiredValue() => this.GetValue() ?? throw new InvalidOperationException();
    }
}
