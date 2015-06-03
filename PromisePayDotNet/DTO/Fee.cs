using Newtonsoft.Json;
using PromisePayDotNet.Enums;

namespace PromisePayDotNet.DTO
{
    public class Fee : AbstractDTO
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fee_type_id")]
        public FeeType FeeType { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "cap")]
        public string Cap { get; set; }

        [JsonProperty(PropertyName = "min")]
        public string Min { get; set; }

        [JsonProperty(PropertyName = "max")]
        public string Max { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }
    }
}
