using Newtonsoft.Json;
using System;

namespace PromisePayDotNet.DAO
{
    public class BankAccount : AbstractAccount
    {
        [JsonProperty(PropertyName = "bank")]
        public Bank Bank { get; set; }

        public string UserId { get; set; }
    }
}
