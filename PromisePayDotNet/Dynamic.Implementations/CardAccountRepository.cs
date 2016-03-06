using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class CardAccountRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.ICardAccountRepository
    {
        public CardAccountRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string, object> GetCardAccountById(string cardAccountId)
        {
            AssertIdNotNull(cardAccountId);
            var request = new RestRequest("/card_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", cardAccountId);
            var response = SendRequest(Client, request);
            var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string,object>>(JsonConvert.SerializeObject(result));
        }

        public IDictionary<string, object> CreateCardAccount(IDictionary<string, object> cardAccount)
        {
            var request = new RestRequest("/card_accounts", Method.POST);
            request.AddParameter("user_id", (string)cardAccount["user_id"]);

            var card = (IDictionary<string, object>)(cardAccount["card"]);
            request.AddParameter("full_name", (string)card["full_name"]);
            request.AddParameter("number", (string)card["number"]);
            request.AddParameter("expiry_month", (string)card["expiry_month"]);
            request.AddParameter("expiry_year", (string)card["expiry_year"]);
            request.AddParameter("cvv", (string)card["cvv"]);

            var response = SendRequest(Client, request);
            var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(result));
        }

        public bool DeleteCardAccount(string cardAccountId)
        {
            AssertIdNotNull(cardAccountId);
            var request = new RestRequest("/card_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", cardAccountId);
            var response = SendRequest(Client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public IDictionary<string, object> GetUserForCardAccount(string cardAccountId)
        {
            AssertIdNotNull(cardAccountId);
            var request = new RestRequest("/card_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", cardAccountId);
            IRestResponse response = SendRequest(Client, request);

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
