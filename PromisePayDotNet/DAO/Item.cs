using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PromisePayDotNet.Enums;
using System;

namespace PromisePayDotNet.DAO
{
    public class Item : AbstractDAO
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "deposit_reference")]
        public string DepositReference { get; set; }

        [JsonProperty(PropertyName = "payment_type_id")]
        public PaymentType PaymentType { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "buyer_id")]
        public string BuyerId { get; set; }

        [JsonProperty(PropertyName = "buyer_name")]
        public string BuyerName { get; set; }

        [JsonProperty(PropertyName = "buyer_country")]
        public string BuyerCountry { get; set; }

        [JsonProperty(PropertyName = "buyer_email")]
        public string BuyerEmail { get; set; }

        [JsonProperty(PropertyName = "seller_id")]
        public string SellerId { get; set; }

        [JsonProperty(PropertyName = "seller_name")]
        public string SellerName { get; set; }

        [JsonProperty(PropertyName = "seller_country")]
        public string SellerCountry { get; set; }

        [JsonProperty(PropertyName = "seller_email")]
        public string SellerEmail { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonIgnore]
        public List<Fee> Fees { get; set; }

        [JsonProperty(PropertyName = "fee_ids")]
        public List<string> FeeIds {
            get
            {
                if (Fees == null || !Fees.Any())
                {
                    return null;
                }
                else
                {
                    return Fees.Select(x => x.Id).ToList();
                }
            }
        }

    }
}
