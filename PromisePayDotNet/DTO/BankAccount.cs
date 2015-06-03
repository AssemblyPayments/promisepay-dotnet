using Newtonsoft.Json;

namespace PromisePayDotNet.DTO
{
    public class BankAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "bank")]
        public Bank Bank { get; set; }
    }
}
