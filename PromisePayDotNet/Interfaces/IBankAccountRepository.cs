using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface IBankAccountRepository
    {
        BankAccount GetBankAccount(string bankAccountId);

        BankAccount CreateBankAccount(BankAccount bankAccount);

        void DeleteBankAccount(string bankAccountId, string mobilePin);

        User GetUserForBankAccount(string bankAccountId);
    }
}
