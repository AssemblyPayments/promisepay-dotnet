using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface ITransactionRepository
    {
        IDictionary<string,object> ListTransactions(int limit = 10, int offset = 0);

        IDictionary<string, object> GetTransaction(string transactionId);

        IDictionary<string, object> GetUserForTransaction(string transactionId);

        IDictionary<string, object> GetFeeForTransaction(string transactionId);
    }
}
