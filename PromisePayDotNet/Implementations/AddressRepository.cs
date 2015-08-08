using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class AddressRepository : AbstractRepository, IAddressRepository
    {
        public AddressRepository(IRestClient client) : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Address GetAddressById(string addressId)
        {
            AssertIdNotNull(addressId);
            var request = new RestRequest("/addresses/{id}", Method.GET);
            request.AddUrlSegment("id", addressId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Address>>(response.Content).Values.First(); 
        }
    }
}
