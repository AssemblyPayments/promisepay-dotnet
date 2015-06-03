using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Implementations
{
    public class ItemRepository : AbstractRepository, IItemRepository
    {
        public IEnumerable<Item> ListItems(int limit = 10, int offset = 0)
        {
            AssertListParamsCorrect(limit, offset);

            var client = GetRestClient();
            var request = new RestRequest("/items", Method.GET);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);

            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var userCollection = dict["items"];
                return JsonConvert.DeserializeObject<List<Item>>(JsonConvert.SerializeObject(userCollection));
            }
            return new List<Item>();
        }

        public Item GetItemById(string itemId)
        {
            AssertIdNotNull(itemId);

            var client = GetRestClient();
            var request = new RestRequest("/items/{id}", Method.GET);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Item>>(response.Content).Values.First();
        }

        public Item CreateItem(Item item)
        {
            var client = GetRestClient();
            var request = new RestRequest("/items", Method.POST);
            request.AddParameter("id", item.Id);
            request.AddParameter("name", item.Name);
            request.AddParameter("amount", item.Amount);
            request.AddParameter("payment_type", (int)item.PaymentType);
            request.AddParameter("buyer_id", item.BuyerId);
            request.AddParameter("seller_id", item.SellerId);
            request.AddParameter("fee_ids", item.FeeIds);
            request.AddParameter("description", item.Description);
             var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Item>>(response.Content).Values.First();
        }

        public bool DeleteItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}", Method.DELETE);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public Item UpdateItem(Item item)
        {
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}", Method.PATCH);
            request.AddUrlSegment("id", item.Id);

            request.AddParameter("amount", item.Amount);
            request.AddParameter("name", item.Name);
            request.AddParameter("description", item.Description);
            request.AddParameter("buyer_id", item.BuyerId);
            request.AddParameter("seller_id", item.SellerId);
            request.AddParameter("fee_ids", item.FeeIds);

            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Item>>(response.Content).Values.First();
        }

        public IEnumerable<Transaction> ListTransactionsForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/transactions", Method.GET);
            request.AddUrlSegment("id", itemId);
            IRestResponse response;
            try
            {
                response = SendRequest(client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no transaction found")
                {
                    return new List<Transaction>();
                }
                throw;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("transactions"))
            {
                var itemCollection = dict["transactions"];
                return JsonConvert.DeserializeObject<List<Transaction>>(JsonConvert.SerializeObject(itemCollection));
            }
            return new List<Transaction>();
        }

        public ItemStatus GetStatusForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/status", Method.GET);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                return JsonConvert.DeserializeObject<ItemStatus>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;
        }

        public IEnumerable<Fee> ListFeesForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/fees", Method.GET);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("fees"))
            {
                var itemCollection = dict["fees"];
                return JsonConvert.DeserializeObject<List<Fee>>(JsonConvert.SerializeObject(itemCollection));
            }
            return new List<Fee>();
        }

        public User GetBuyerForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/buyers", Method.GET);
            request.AddUrlSegment("id", itemId);
            IRestResponse response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var itemCollection = dict["users"];
                return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;
        }

        public User GetSellerForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/sellers", Method.GET);
            request.AddUrlSegment("id", itemId);
            IRestResponse response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var itemCollection = dict["users"];
                return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;
        }

        public WireDetails GetWireDetailsForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/wire_details", Method.GET);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var details =  JsonConvert.DeserializeObject<DetailsContainer>(JsonConvert.SerializeObject(itemCollection));
                return details.WireDetails;
            }
            return null;
        }

        public BPayDetails GetBPayDetailsForItem(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/bpay_details", Method.GET);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var details = JsonConvert.DeserializeObject<DetailsContainer>(JsonConvert.SerializeObject(itemCollection));
                return details.BPayDetails;
            }
            return null;
        }

        public Item MakePayment(string itemId, string accountId, string userId)
        {
            AssertIdNotNull(itemId);
            AssertIdNotNull(accountId);
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/make_payment", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("account_id", accountId);
            request.AddParameter("user_id", userId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item RequestPayment(string itemId, string sellerId)
        {
            AssertIdNotNull(itemId);
            AssertIdNotNull(sellerId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/request_payment", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", sellerId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item ReleasePayment(string itemId, string buyerId, int releaseAmount)
        {
            AssertIdNotNull(itemId);
            AssertIdNotNull(buyerId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/release_payment", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", buyerId);
            request.AddParameter("release_amount", releaseAmount);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item RequestRelease(string itemId, string sellerId, int releaseAmount)
        {
            AssertIdNotNull(itemId);
            AssertIdNotNull(sellerId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/request_release", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", sellerId);
            request.AddParameter("release_amount", releaseAmount);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item Cancel(string itemId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/cancel", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item AcknowledgeWire(string itemId, string buyerId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/acknowledge_wire", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", buyerId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item AcknowledgePayPal(string itemId, string buyerId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/acknowledge_paypal", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", buyerId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item RevertWire(string itemId, string buyerId)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/revert_wire", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", buyerId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item RequestRefund(string itemId, string buyerId, string refundAmount, string refundMessage)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/request_refund", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", buyerId);
            request.AddParameter("refund_amount", refundAmount);
            request.AddParameter("refund_message", refundMessage);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }

        public Item Refund(string itemId, string sellerId, string refundAmount, string refundMessage)
        {
            AssertIdNotNull(itemId);
            var client = GetRestClient();
            var request = new RestRequest("/items/:id/refund", Method.PATCH);
            request.AddUrlSegment("id", itemId);
            request.AddParameter("user_id", sellerId);
            request.AddParameter("refund_amount", refundAmount);
            request.AddParameter("refund_message", refundMessage);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                var item = JsonConvert.DeserializeObject<Item>(JsonConvert.SerializeObject(itemCollection));
                return item;
            }
            return null;
        }
    }
}
