using Newtonsoft.Json;
using System.Collections.Generic;

namespace PromisePayDotNet.DAO
{
    public class Company : AbstractDAO
    {
        [JsonProperty(PropertyName = "legal_name")]
        public string LegalName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "related")]
        public IDictionary<string, string> Related { get; set; }
    }
}
