using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class PayPalAccountRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.IPayPalAccountRepository
    {
        public PayPalAccountRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string, object> GetPayPalAccountById(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var request = new RestRequest("/paypal_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(Client, request);
            var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(result));
        }

        public IDictionary<string, object> CreatePayPalAccount(IDictionary<string, object> paypalAccount)
        {
            var request = new RestRequest("/paypal_accounts", Method.POST);
            request.AddParameter("user_id", (string)paypalAccount["user_id"]);
            var paypal = (IDictionary<string, object>)(paypalAccount["paypal"]);
            request.AddParameter("paypal_email", (string)paypal["email"]);

            var response = SendRequest(Client, request);
            var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(result));
        }

        public bool DeletePayPalAccount(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var request = new RestRequest("/paypal_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(Client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public IDictionary<string, object> GetUserForPayPalAccount(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var request = new RestRequest("/paypal_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(Client, request);

            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var item = dict["users"];
                return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(item));
            }
            return null;
        }

    }
}
