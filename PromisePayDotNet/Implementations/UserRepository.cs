using System;
using System.Collections.Generic;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.Implementations
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> ListUsers(int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void SendMobilePin(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> ListItemsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PayPalAccount> ListPayPalAccountsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardAccount> ListCardAccountsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankAccount> ListBankAccountsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public DisbursementAccount ListDisbursementAccounts(string userId, string accountId, string mobilePin)
        {
            throw new NotImplementedException();
        }
    }
}
