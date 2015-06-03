using Newtonsoft.Json;

namespace PromisePayDotNet.DTO
{
    public class PayPalAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "paypal")]
        public PayPal PayPal { get; set; }
    }
}
