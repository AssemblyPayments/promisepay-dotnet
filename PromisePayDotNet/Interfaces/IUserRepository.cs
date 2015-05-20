using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> ListUsers(int limit = 10, int offset = 0);
        
        User GetUserById(string userId);

        User CreateUser(User user);

        void DeleteUser(string userId);

        void SendMobilePin(string userId);

        IEnumerable<Item> ListItemsForUser(string userId);

        IEnumerable<PayPalAccount> ListPayPalAccountsForUser(string userId);

        IEnumerable<CardAccount> ListCardAccountsForUser(string userId);

        IEnumerable<BankAccount> ListBankAccountsForUser(string userId);

        DisbursementAccount ListDisbursementAccounts(string userId, string accountId, string mobilePin);


    }
}
