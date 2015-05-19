using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class User
    {
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }


        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public String FullName { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}
