using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class FeeRepository : AbstractRepository, IFeeRepository
    {
        public IEnumerable<Fee> ListFees()
        {
            var client = GetRestClient();
            var request = new RestRequest("/fees", Method.GET);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("fees"))
            {
                var userCollection = dict["fees"];
                return JsonConvert.DeserializeObject<List<Fee>>(JsonConvert.SerializeObject(userCollection));
            }
            else
            {
                return new List<Fee>();
            }
        }

        public Fee GetFeeById(string feeId)
        {
            AssertIdNotNull(feeId);
            var client = GetRestClient();
            var request = new RestRequest("/fees/{id}", Method.GET);
            request.AddUrlSegment("id", feeId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Fee>>(response.Content).Values.First();
        }

        public Fee CreateFee(Fee fee)
        {
            if (!possibleTos.Contains(fee.To))
            {
                throw new ValidationException("To should have value of \"buyer\", \"seller\", \"cc\", \"int_wire\", \"paypal_payout\"");
            }

            var client = GetRestClient();
            var request = new RestRequest("/fees", Method.POST);
            request.AddParameter("name", fee.Name);
            request.AddParameter("fee_type_id", (int)fee.FeeType);
            request.AddParameter("amount", fee.Amount);
            request.AddParameter("cap", fee.Cap);
            request.AddParameter("min", fee.Min);
            request.AddParameter("max", fee.Max);
            request.AddParameter("to", fee.To);

            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Fee>>(response.Content).Values.First();
        }

        private List<string> possibleTos = new List<string>() {"buyer", "seller", "cc", "int_wire", "paypal_payout"};
    }
}
