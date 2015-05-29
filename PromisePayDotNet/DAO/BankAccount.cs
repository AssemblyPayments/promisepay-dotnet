using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class BankAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "bank")]
        public Bank Bank { get; set; }
    }
}
