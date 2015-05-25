using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class ItemRepository : AbstractRepository, IItemRepository
    {
        public IEnumerable<Item> ListItems(int limit = 10, int offset = 0)
        {
            throw new System.NotImplementedException();
        }

        public Item GetItemById(string itemId)
        {
            throw new System.NotImplementedException();
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

        public void DeleteItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Transaction> ListTransactionsForItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public ItemStatus GetStatusForItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Fee> ListFeesForItem(string itemId)
        {
            throw new System.NotImplementedException();
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
