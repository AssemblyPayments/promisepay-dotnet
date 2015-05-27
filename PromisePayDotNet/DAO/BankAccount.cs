using Newtonsoft.Json;
using System;

namespace PromisePayDotNet.DAO
{
    public class BankAccount : AbstractDAO
    {
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "bank")]
        public Bank Bank { get; set; }
    }
}
