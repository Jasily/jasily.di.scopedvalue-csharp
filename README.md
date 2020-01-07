# Jasily.DI.ScopedValue-csharp

Allow dynamic add readonly values when you create scope.

``` cs
var serviceProvider = new ServiceCollection()
    .AddScopedValue<string>()
    .BuildServiceProvider();

using (var scope = serviceProvider.CreateScopeWithValues((typeof(string), "1")))
{
    scope.ServiceProvider.GetRequiredService<string>(); // "1"
    // OR
    scope.ServiceProvider.GetRequiredService<IScopedValue<string>>().GetValue(); // "1"
}
```
