using Newtonsoft.Json;

namespace PromisePayDotNet.DTO
{
    public class PayPal
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
