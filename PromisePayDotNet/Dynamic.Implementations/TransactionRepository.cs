using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class TransactionRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.ITransactionRepository
    {
        public TransactionRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string,object> ListTransactions(int limit = 10, int offset = 0)
        {
            AssertListParamsCorrect(limit, offset);

            var request = new RestRequest("/transactions", Method.GET);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> GetTransaction(string transactionId)
        {
            AssertIdNotNull(transactionId);
            var request = new RestRequest("/transactions/{id}", Method.GET);
            request.AddUrlSegment("id", transactionId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> GetUserForTransaction(string transactionId)
        {
            AssertIdNotNull(transactionId);
            var request = new RestRequest("/transactions/{id}/users", Method.GET);
            request.AddUrlSegment("id", transactionId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> GetFeeForTransaction(string transactionId)
        {
            AssertIdNotNull(transactionId);
            var request = new RestRequest("/transactions/{id}/fees", Method.GET);
            request.AddUrlSegment("id", transactionId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }
    }
}
