using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface IBankAccountRepository
    {
        BankAccount GetBankAccountById(string bankAccountId);

        BankAccount CreateBankAccount(BankAccount bankAccount, string mobilePin = "");

        bool DeleteBankAccount(string bankAccountId, string mobilePin="");

        User GetUserForBankAccount(string bankAccountId);
    }
}
