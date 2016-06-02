﻿using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<IDictionary<string,object>> ListUsers(int limit = 10, int offset = 0);

        IDictionary<string, object> GetUserById(string userId);

        IDictionary<string, object> CreateUser(IDictionary<string, object> user);

        IDictionary<string, object> UpdateUser(IDictionary<string, object> user);

        bool DeleteUser(string userId);

        IEnumerable<IDictionary<string, object>> ListItemsForUser(string userId);

        IDictionary<string, object> GetPayPalAccountForUser(string userId);

        IDictionary<string, object> GetCardAccountForUser(string userId);
        
        IDictionary<string, object> GetBankAccountForUser(string userId);

        bool SetDisbursementAccount(string userId, string accountId);
    }
}
