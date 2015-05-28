using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class PayPalAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "paypal")]
        public PayPal PayPal { get; set; }
    }
}
