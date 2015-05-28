using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface ICardAccountRepository
    {

        CardAccount GetCardAccountById(string cardAccountId);

        CardAccount CreateCardAccount(CardAccount cardAccount, string mobilePin = "");

        bool DeleteCardAccount(string cardAccountId, string mobilePin = "");

        User GetUserForCardAccount(string cardAccountId);

    }
}
