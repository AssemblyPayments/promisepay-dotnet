using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface ICardAccountRepository
    {

        CardAccount GetCardAccount(string cardAccountId, string mobilePin);

        CardAccount CreateCardAccount(CardAccount cardAccount);

        bool DeleteCardAccount(string cardAccountId, string mobilePin);

        User GetUserForCardAccount(string cardAccountId);

    }
}
