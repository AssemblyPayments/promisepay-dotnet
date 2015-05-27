using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using System;

namespace PromisePayDotNet.Implementations
{
    public class BankAccountRepository : AbstractRepository, IBankAccountRepository
    {
        public BankAccount GetBankAccount(string bankAccountId)
        {
            throw new NotImplementedException();
        }

        public BankAccount CreateBankAccount(BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }

        public void DeleteBankAccount(string bankAccountId, string mobilePin)
        {
            throw new NotImplementedException();
        }

        public User GetUserForBankAccount(string bankAccountId)
        {
            throw new NotImplementedException();
        }
    }
}
