using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Interfaces;
using RestSharp;

namespace PromisePayDotNet.Implementations
{
    public class AddressRepository : AbstractRepository, IAddressRepository
    {
        public Address GetAddressById(string addressId)
        {
            AssertIdNotNull(addressId);
            var client = GetRestClient();
            var request = new RestRequest("/addresses/{id}", Method.GET);
            request.AddUrlSegment("id", addressId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Address>>(response.Content).Values.First(); 
        }
    }
}
