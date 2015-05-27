using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface IPayPalAccountRepository
    {

        PayPalAccount GetPayPalAccount(string paypalAccountId);

        PayPalAccount CreateBankAccount(PayPalAccount paypalAccount);

        void DeletePayPalAccount(string paypalAccountId, string mobilePin);

        User GetUserForPayPalAccount(string paypalAccountId, string mobilePin);

    }
}
