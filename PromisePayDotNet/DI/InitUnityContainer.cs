using Microsoft.Practices.Unity;
using PromisePayDotNet.Implementations;
using PromisePayDotNet.Interfaces;
using RestSharp;

namespace PromisePayDotNet.DI
{
    public class InitUnityContainer
    {
        public static void Init(IUnityContainer container)
        {
            container.RegisterType<IRestClient,RestClient>(new InjectionConstructor());

            //Old style repositories
            container.RegisterType<IAddressRepository, AddressRepository>();
            container.RegisterType<IBankAccountRepository, BankAccountRepository>();
            container.RegisterType<ICardAccountRepository, CardAccountRepository>();
            container.RegisterType<ICompanyRepository, CompanyRepository>();
            container.RegisterType<IFeeRepository, FeeRepository>();
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IPayPalAccountRepository, PayPalAccountRepository>();
            container.RegisterType<ITokenRepository, TokenRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<IUploadRepository, UploadRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            //Dynamic repositories
            container.RegisterType<Dynamic.Interfaces.IAddressRepository, Dynamic.Implementations.AddressRepository>();
            container.RegisterType<Dynamic.Interfaces.IBankAccountRepository, Dynamic.Implementations.BankAccountRepository>();
            container.RegisterType<Dynamic.Interfaces.IBatchTransactionRepository, Dynamic.Implementations.BatchTransactionRepository>();
            container.RegisterType<Dynamic.Interfaces.ICardAccountRepository, Dynamic.Implementations.CardAccountRepository>();
            container.RegisterType<Dynamic.Interfaces.IChargeRepository, Dynamic.Implementations.ChargeRepository>();
            container.RegisterType<Dynamic.Interfaces.ICompanyRepository, Dynamic.Implementations.CompanyRepository>();
            container.RegisterType<Dynamic.Interfaces.IConfigurationRepository, Dynamic.Implementations.ConfigurationRepository>();
            container.RegisterType<Dynamic.Interfaces.IDirectDebitAuthorityRepository, Dynamic.Implementations.DirectDebitAuthorityRepository>();
            container.RegisterType<Dynamic.Interfaces.IFeeRepository, Dynamic.Implementations.FeeRepository>();
            container.RegisterType<Dynamic.Interfaces.IItemRepository, Dynamic.Implementations.ItemRepository>();
            container.RegisterType<Dynamic.Interfaces.IMarketplaceRepository, Dynamic.Implementations.MarketplaceRepository>();
            container.RegisterType<Dynamic.Interfaces.IPayPalAccountRepository, Dynamic.Implementations.PayPalAccountRepository>();
            container.RegisterType<Dynamic.Interfaces.IRestrictionRepository, Dynamic.Implementations.RestrictionRepository>();
            container.RegisterType<Dynamic.Interfaces.ITokenRepository, Dynamic.Implementations.TokenRepository>();
            container.RegisterType<Dynamic.Interfaces.IToolRepository, Dynamic.Implementations.ToolRepository>();
            container.RegisterType<Dynamic.Interfaces.ITransactionRepository, Dynamic.Implementations.TransactionRepository>();
            container.RegisterType<Dynamic.Interfaces.IUploadRepository, Dynamic.Implementations.UploadRepository>();
            container.RegisterType<Dynamic.Interfaces.IUserRepository, Dynamic.Implementations.UserRepository>();
            container.RegisterType<Dynamic.Interfaces.IWalletRepository, Dynamic.Implementations.WalletRepository>();
        }

    }
}
