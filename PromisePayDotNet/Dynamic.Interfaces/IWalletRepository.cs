using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IWalletRepository
    {
        IDictionary<string, object> ShowWalletAccount(string id);

        IDictionary<string, object> WithdrawFunds(string id);

        IDictionary<string, object> DepositFunds(string id);

        IDictionary<string, object> ShowWalletAccountUser(string id);
    }
}
