using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class MarketplaceRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.IMarketplaceRepository
    {
        public MarketplaceRepository(IRestClient client)
            : base(client)
        {
        }

        public IDictionary<string, object> ShowMarketplace() 
        {
            var request = new RestRequest("/marketplace", Method.GET);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }
    }
}
