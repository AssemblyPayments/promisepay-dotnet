using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IBankAccountRepository
    {
        IDictionary<string, object> GetBankAccountById(string bankAccountId);

        IDictionary<string, object> CreateBankAccount(IDictionary<string, object> bankAccount);

        bool DeleteBankAccount(string bankAccountId);

        IDictionary<string, object> GetUserForBankAccount(string bankAccountId);
    }
}
