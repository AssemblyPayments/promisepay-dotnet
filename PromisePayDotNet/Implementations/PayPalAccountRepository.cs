using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Implementations
{
    public class PayPalAccountRepository : AbstractRepository, IPayPalAccountRepository
    {
        public PayPalAccount GetPayPalAccountById(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/paypal_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, PayPalAccount>>(response.Content).Values.First();
        }

        public PayPalAccount CreatePayPalAccount(PayPalAccount paypalAccount)
        {
            var client = GetRestClient();
            var request = new RestRequest("/paypal_accounts", Method.POST);
            request.AddParameter("user_id", paypalAccount.UserId);
            request.AddParameter("paypal_email", paypalAccount.PayPal.Email);

            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, PayPalAccount>>(response.Content).Values.First();
        }

        public bool DeletePayPalAccount(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/paypal_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public User GetUserForPayPalAccount(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/paypal_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(client, request);

            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var item = dict["users"];
                return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(item));
            }
            return null;
        }
    }
}
