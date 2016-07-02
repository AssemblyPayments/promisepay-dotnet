using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IBankAccountRepository
    {
        IDictionary<string, object> CreateBankAccount(IDictionary<string, object> bankAccount);
        
        IDictionary<string, object> GetBankAccountById(string bankAccountId);

        bool DeleteBankAccount(string bankAccountId);

        IDictionary<string, object> GetUserForBankAccount(string bankAccountId);

        IDictionary<string, object> ValidateRoutingNumber(string routingNumber);
    }
}
