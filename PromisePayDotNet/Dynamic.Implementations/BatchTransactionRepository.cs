using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class BatchTransactionRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                              PromisePayDotNet.Dynamic.Interfaces.IBatchTransactionRepository
    {
        public BatchTransactionRepository(IRestClient client)
            : base(client)
        {

        }

        public IDictionary<string, object> List(IDictionary<string, object> filter = null, int limit = 10, int offset = 0)
        {
            var request = new RestRequest("/batch_transactions", Method.GET);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);
            if (filter != null) {
                foreach (var key in filter.Keys)
                {
                    request.AddParameter(key, filter[key]);
                }
            }

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> Show(string id)
        {
            var request = new RestRequest("/batch_transactions/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }
    }
}
