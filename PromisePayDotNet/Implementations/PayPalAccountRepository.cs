using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.Implementations
{
    public class PayPalAccountRepository : AbstractRepository, IPayPalAccountRepository
    {
        public PayPalAccount GetPayPalAccount(string paypalAccountId)
        {
            throw new System.NotImplementedException();
        }

        public PayPalAccount CreateBankAccount(PayPalAccount paypalAccount)
        {
            throw new System.NotImplementedException();
        }

        public void DeletePayPalAccount(string paypalAccountId, string mobilePin)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserForPayPalAccount(string paypalAccountId, string mobilePin)
        {
            throw new System.NotImplementedException();
        }
    }
}
