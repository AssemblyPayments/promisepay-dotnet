using Microsoft.Practices.Unity;
using NUnit.Framework;
using PromisePayDotNet.DI;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.Tests
{
    public class DITest
    {
        [Test]
        public void TestDIContainer()
        {
            var container = new UnityContainer();
            InitUnityContainer.Init(container);
            var userService = container.Resolve<IUserRepository>();
            Assert.IsNotNull(userService);
        }
    }
}
