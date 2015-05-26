using System.Net;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class ItemRepository : AbstractRepository, IItemRepository
    {
        public IEnumerable<Item> ListItems(int limit = 10, int offset = 0)
        {
            if (limit < 0 || offset < 0)
            {
                throw new ArgumentException("limit and offset values should be nonnegative!");
            }

            if (limit > EntityListLimit)
            {
                throw new ArgumentException("Max value for limit parameter is 200!");
            }

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
            else
            {
                return new List<Item>();
            }
        }

        public Item GetItemById(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("id cannot be empty!");
            }

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
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("id cannot be empty!");
            }
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}", Method.DELETE);
            request.AddUrlSegment("id", itemId);
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
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("id cannot be empty!");
            }
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
                throw e;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("transactions"))
            {
                var itemCollection = dict["transactions"];
                return JsonConvert.DeserializeObject<List<Transaction>>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return new List<Transaction>();
            }
        }

        public ItemStatus GetStatusForItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("id cannot be empty!");
            }
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/status", Method.GET);
            request.AddUrlSegment("id", itemId);
            IRestResponse response;
            response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                return JsonConvert.DeserializeObject<ItemStatus>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Fee> ListFeesForItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentException("id cannot be empty!");
            }
            var client = GetRestClient();
            var request = new RestRequest("/items/{id}/fees", Method.GET);
            request.AddUrlSegment("id", itemId);
            IRestResponse response;
            response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("fees"))
            {
                var itemCollection = dict["fees"];
                return JsonConvert.DeserializeObject<List<Fee>>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return new List<Fee>();
            }
        }

        public IEnumerable<User> ListBuyersForItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> ListSellersForItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public WireDetails GetWireDetailsForItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public BPayDetails GetBPayDetailsForItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public Item MakePayment(string itemId, string accountId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Item RequestPayment(string itemId, string sellerId)
        {
            throw new System.NotImplementedException();
        }

        public Item ReleasePayment(string itemId, string buyerId, int releaseAmount)
        {
            throw new System.NotImplementedException();
        }

        public Item RequestRelease(string itemId, string sellerId, int releaseAmount)
        {
            throw new System.NotImplementedException();
        }

        public Item Cancel(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public Item AcknowledgeWire(string itemId, string buyerId)
        {
            throw new System.NotImplementedException();
        }

        public Item AcknowledgePayPal(string itemId, string buyerId)
        {
            throw new System.NotImplementedException();
        }

        public Item RevertWire(string itemId, string buyerId)
        {
            throw new System.NotImplementedException();
        }

        public Item RequestRefund(string itemId, string buyerId, string refundAmount, string refundMessage)
        {
            throw new System.NotImplementedException();
        }

        public Item Refund(string itemId, string sellerId, string refundAmount, string refundMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
