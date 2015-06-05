using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromisePayDotNet.DI;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class DITest
    {
        [TestMethod]
        public void TestDIContainer()
        {
            var container = new UnityContainer();
            InitUnityContainer.Init(container);
            var userService = container.Resolve<IUserRepository>();
            Assert.IsNotNull(userService);
        }
    }
}
