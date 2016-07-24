using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface ITransactionRepository
    {
        IDictionary<string,object> ListTransactions(int limit = 10, int offset = 0);

        IDictionary<string, object> GetTransaction(string transactionId);

        IDictionary<string, object> GetUserForTransaction(string transactionId);

        IDictionary<string, object> GetFeeForTransaction(string transactionId);

        IDictionary<string, object> ShowTransactionWalletAccount(string transactionId);

        IDictionary<string, object> ShowTransactionBankAccount(string transactionId);

        IDictionary<string, object> ShowTransactionCardAccount(string transactionId);

        IDictionary<string, object> ShowTransactionPayPalAccount(string transactionId);
    }
}
