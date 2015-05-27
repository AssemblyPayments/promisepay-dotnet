using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class PayPal
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
