using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class AddressRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                     PromisePayDotNet.Dynamic.Interfaces.IAddressRepository
    {
        public AddressRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string,object> GetAddressById(string addressId)
        {
            AssertIdNotNull(addressId);
            var request = new RestRequest("/addresses/{id}", Method.GET);
            request.AddUrlSegment("id", addressId);
            var response = SendRequest(Client, request);
            var address = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(address));
        }
    }
}
