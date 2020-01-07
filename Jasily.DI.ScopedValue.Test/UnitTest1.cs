using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jasily.DI.ScopedValue.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var serviceProvider = new ServiceCollection()
                .AddScopedValue<string>()
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScopeWithValues((typeof(string), "1")))
            {
                Assert.AreEqual("1", scope.ServiceProvider.GetRequiredService<string>());
                Assert.AreEqual("1", scope.ServiceProvider.GetRequiredService<IScopedValue<string>>().GetValue());
            }

            using (var scope = serviceProvider.CreateScopeWithValues((typeof(string), "2")))
            {
                Assert.AreEqual("2", scope.ServiceProvider.GetRequiredService<string>());
                Assert.AreEqual("2", scope.ServiceProvider.GetRequiredService<IScopedValue<string>>().GetValue());
            }

            using (var scope = serviceProvider.CreateScope())
            {
                Assert.AreEqual(null, scope.ServiceProvider.GetService<string>());
            }
        }
    }
}
