using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PromisePayDotNet.DTO
{
    public abstract class AbstractDTO
    {
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalData { get; set; }

        [JsonProperty(PropertyName = "links")]
        public IDictionary<string, string> Links { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
