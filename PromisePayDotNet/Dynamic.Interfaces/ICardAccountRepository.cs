using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface ICardAccountRepository
    {
        IDictionary<string, object> GetCardAccountById(string cardAccountId);

        IDictionary<string, object> CreateCardAccount(IDictionary<string, object> cardAccount);

        bool DeleteCardAccount(string cardAccountId);

        IDictionary<string, object> GetUserForCardAccount(string cardAccountId);
    }
}
