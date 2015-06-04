using PromisePayDotNet.DTO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> ListUsers(int limit = 10, int offset = 0);
        
        User GetUserById(string userId);

        User CreateUser(User user);

        User UpdateUser(User user);

        bool DeleteUser(string userId);

        void SendMobilePin(string userId);

        IEnumerable<Item> ListItemsForUser(string userId);

        IEnumerable<PayPalAccount> ListPayPalAccountsForUser(string userId);

        IEnumerable<CardAccount> ListCardAccountsForUser(string userId);

        IEnumerable<BankAccount> ListBankAccountsForUser(string userId);

        DisbursementAccount GetDisbursementAccount(string userId, string accountId, string mobilePin);
    }
}
