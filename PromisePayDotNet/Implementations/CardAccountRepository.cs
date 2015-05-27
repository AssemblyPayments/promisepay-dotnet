using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using System;

namespace PromisePayDotNet.Implementations
{
    public class CardAccountRepository : AbstractRepository, ICardAccountRepository
    {
        public CardAccount GetCardAccount(string cardAccountId, string mobilePin)
        {
            throw new NotImplementedException();
        }

        public CardAccount CreateCardAccount(CardAccount cardAccount)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCardAccount(string cardAccountId, string mobilePin)
        {
            throw new NotImplementedException();
        }

        public User GetUserForCardAccount(string cardAccountId)
        {
            throw new NotImplementedException();
        }
    }
}
