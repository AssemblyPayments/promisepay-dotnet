using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class TokenRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                    PromisePayDotNet.Dynamic.Interfaces.ITokenRepository
    {
        public TokenRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string RequestToken()
        {
            var request = new RestRequest("/request_token", Method.GET);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, string>>(response.Content).Values.First();
        }

        public IDictionary<string, object> RequestSessionToken(IDictionary<string,object> token)
        {
            var request = new RestRequest("/request_session_token", Method.GET);
            request.AddParameter("current_user_id", token["current_user_id"]);
            request.AddParameter("current_user", token["current_user"]);
            request.AddParameter("item_name", token["item_name"]);
            request.AddParameter("amount", token["amount"]);
            request.AddParameter("seller_lastname", token["seller_lastname"]);
            request.AddParameter("seller_firstname", token["seller_firstname"]);
            request.AddParameter("seller_country", token["seller_country"]);
            request.AddParameter("buyer_lastname", token["buyer_lastname"]);
            request.AddParameter("buyer_firstname", token["buyer_firstname"]);
            request.AddParameter("buyer_country", token["buyer_country"]);
            request.AddParameter("seller_email", token["seller_email"]);
            request.AddParameter("buyer_email", token["buyer_email"]);
            request.AddParameter("external_item_id", token["external_item_id"]);
            request.AddParameter("external_seller_id", token["external_seller_id"]);
            request.AddParameter("external_buyer_id", token["external_buyer_id"]);
            request.AddParameter("fee_ids", token["fee_ids"]);
            request.AddParameter("payment_type_id", token["payment_type_id"]);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string,object> GetWidget(string sessionToken)
        {
            var request = new RestRequest("/widget", Method.GET);
            request.AddParameter("session_token", sessionToken);
            var response = SendRequest(Client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("widget"))
            {
                var itemCollection = dict["widget"];
                return JsonConvert.DeserializeObject<IDictionary<string,object>>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;
        }
    }
}
