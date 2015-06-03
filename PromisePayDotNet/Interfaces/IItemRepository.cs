using PromisePayDotNet.DTO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface IItemRepository
    {

        IEnumerable<Item> ListItems(int limit = 10, int offset = 0);

        Item GetItemById(string itemId);

        Item CreateItem(Item item);

        bool DeleteItem(string itemId);

        Item UpdateItem(Item item);

        IEnumerable<Transaction> ListTransactionsForItem(string itemId);

        ItemStatus GetStatusForItem(string itemId);

        IEnumerable<Fee> ListFeesForItem(string itemId);

        User GetBuyerForItem(string itemId);

        User GetSellerForItem(string itemId);

        WireDetails GetWireDetailsForItem(string itemId);

        BPayDetails GetBPayDetailsForItem(string itemId);

        Item MakePayment(string itemId, string accountId, string userId);

        Item RequestPayment(string itemId, string sellerId);

        Item ReleasePayment(string itemId, string buyerId, int releaseAmount);

        Item RequestRelease(string itemId, string sellerId, int releaseAmount);

        Item Cancel(string itemId);

        Item AcknowledgeWire(string itemId, string buyerId);

        Item AcknowledgePayPal(string itemId, string buyerId);

        Item RevertWire(string itemId, string buyerId);

        Item RequestRefund(string itemId, string buyerId, string refundAmount, string refundMessage);

        Item Refund(string itemId, string sellerId, string refundAmount, string refundMessage);

    }
}
