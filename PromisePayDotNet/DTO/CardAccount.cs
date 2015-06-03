using Newtonsoft.Json;

namespace PromisePayDotNet.DTO
{
    public class CardAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "card")]
        public Card Card { get; set; }
    }
}
