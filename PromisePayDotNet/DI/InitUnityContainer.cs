using Microsoft.Practices.Unity;
using PromisePayDotNet.Implementations;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.DI
{
    public class InitUnityContainer
    {
        public static void Init(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
        }
    }
}
