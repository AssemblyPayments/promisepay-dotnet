using Microsoft.Practices.Unity;
using PromisePayDotNet.Implementations;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.DI
{
    public class InitUnityContainer
    {
        public static void Init(IUnityContainer container)
        {
            container.RegisterType<IAddressRepository, AddressRepository>();
            container.RegisterType<IBankAccountRepository, BankAccountRepository>();
            container.RegisterType<ICardAccountRepository, CardAccountRepository>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IFeeRepository, FeeRepository>();
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IPayPalAccountRepository, PayPalAccountRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<IUploadRepository, UploadRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
        }
    }
}
