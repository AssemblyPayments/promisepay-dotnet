using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Implementations
{
    public class CardAccountRepository : AbstractRepository, ICardAccountRepository
    {
        public CardAccount GetCardAccountById(string cardAccountId)
        {
            AssertIdNotNull(cardAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/card_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", cardAccountId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, CardAccount>>(response.Content).Values.First();
        }

        public CardAccount CreateCardAccount(CardAccount cardAccount)
        {
            var client = GetRestClient();
            var request = new RestRequest("/card_accounts", Method.POST);
            request.AddParameter("user_id", cardAccount.UserId);
            request.AddParameter("full_name", cardAccount.Card.FullName);
            request.AddParameter("number", cardAccount.Card.Number);
            request.AddParameter("expiry_month", cardAccount.Card.ExpiryMonth);
            request.AddParameter("expiry_year", cardAccount.Card.ExpiryYear);
            request.AddParameter("cvv", cardAccount.Card.CVV);

            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, CardAccount>>(response.Content).Values.First();
        }

        public bool DeleteCardAccount(string cardAccountId)
        {
            AssertIdNotNull(cardAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/card_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", cardAccountId);
            var response = SendRequest(client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User GetUserForCardAccount(string cardAccountId)
        {
            AssertIdNotNull(cardAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/card_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", cardAccountId);
            IRestResponse response;
            response = SendRequest(client, request);

            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var item = dict["users"];
                return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(item));
            }
            else
            {
                return null;
            }        
        }
    }
}
