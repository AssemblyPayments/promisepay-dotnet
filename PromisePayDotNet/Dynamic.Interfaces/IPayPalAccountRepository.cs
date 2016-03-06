using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IPayPalAccountRepository
    {
        IDictionary<string, object> GetPayPalAccountById(string paypalAccountId);

        IDictionary<string, object> CreatePayPalAccount(IDictionary<string, object> paypalAccount);

        bool DeletePayPalAccount(string paypalAccountId);

        IDictionary<string, object> GetUserForPayPalAccount(string paypalAccountId);
    }
}
