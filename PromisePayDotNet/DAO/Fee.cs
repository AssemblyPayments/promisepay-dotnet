using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class Fee : AbstractDAO
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fee_type_id")]
        public int FeeTypeId { get; set; }

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
