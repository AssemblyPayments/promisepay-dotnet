using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IItemRepository
    {

        IEnumerable<IDictionary<string,object>> ListItems(int limit = 10, int offset = 0);

        IDictionary<string, object> GetItemById(string itemId);

        IDictionary<string, object> CreateItem(IDictionary<string, object> item);

        bool DeleteItem(string itemId);

        IDictionary<string, object> UpdateItem(IDictionary<string, object> item);

        IEnumerable<IDictionary<string, object>> ListTransactionsForItem(string itemId);

        IDictionary<string, object> GetStatusForItem(string itemId);

        IEnumerable<IDictionary<string, object>> ListFeesForItem(string itemId);

        IDictionary<string, object> GetBuyerForItem(string itemId);

        IDictionary<string, object> GetSellerForItem(string itemId);

        IDictionary<string, object> GetWireDetailsForItem(string itemId);

        IDictionary<string, object> GetBPayDetailsForItem(string itemId);

        //actions methods start here

        IDictionary<string, object> MakePayment(string itemId, string accountId);

        IDictionary<string, object> RequestPayment(string itemId);

        IDictionary<string, object> ReleasePayment(string itemId, int releaseAmount);

        IDictionary<string, object> RequestRelease(string itemId, int releaseAmount);

        IDictionary<string, object> Cancel(string itemId);

        IDictionary<string, object> AcknowledgeWire(string itemId);

        IDictionary<string, object> AcknowledgePayPal(string itemId);

        IDictionary<string, object> RevertWire(string itemId);

        IDictionary<string, object> RequestRefund(string itemId, string refundAmount, string refundMessage);

        IDictionary<string, object> Refund(string itemId, string refundAmount, string refundMessage);

    }
}
