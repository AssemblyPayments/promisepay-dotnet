using Newtonsoft.Json;
using PromisePayDotNet.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class FeeRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.IFeeRepository
    {
        public FeeRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<IDictionary<string,object>> ListFees()
        {
            var request = new RestRequest("/fees", Method.GET);
            var response = SendRequest(Client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("fees"))
            {
                var userCollection = dict["fees"];
                return JsonConvert.DeserializeObject<List<IDictionary<string,object>>>(JsonConvert.SerializeObject(userCollection));
            }
            return new List<IDictionary<string,object>>();
        }

        public IDictionary<string,object> GetFeeById(string feeId)
        {
            AssertIdNotNull(feeId);
            var request = new RestRequest("/fees/{id}", Method.GET);
            request.AddUrlSegment("id", feeId);
            var response = SendRequest(Client, request);
            var fee = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(fee));
        }

        public IDictionary<string,object> CreateFee(IDictionary<string, object> fee)
        {
            VailidateFee(fee);
            var request = new RestRequest("/fees", Method.POST);

            foreach (var key in fee.Keys) {
                request.AddParameter(key, (string)fee[key]);            
            }

            var response = SendRequest(Client, request);
            var returnedFee = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(returnedFee));
        }

        private void VailidateFee(IDictionary<string, object> fee)
        {
            if (fee == null) throw new ArgumentNullException("fee");
            if (!_possibleTos.Contains((string)fee["to"]))
            {
                throw new ValidationException(
                    "To should have value of \"buyer\", \"seller\", \"cc\", \"int_wire\", \"paypal_payout\"");
            }
        }

        private readonly List<string> _possibleTos = new List<string> { "buyer", "seller", "cc", "int_wire", "paypal_payout" };

    }
}
