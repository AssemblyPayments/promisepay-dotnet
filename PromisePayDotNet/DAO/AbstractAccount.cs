using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public abstract class AbstractAccount : AbstractDAO
    {
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
