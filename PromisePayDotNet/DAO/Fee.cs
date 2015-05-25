using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class Fee
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
