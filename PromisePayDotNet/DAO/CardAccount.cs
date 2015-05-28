using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class CardAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "card")]
        public Card Card { get; set; }
    }
}
