using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class Upload : AbstractDAO
    {
        [JsonProperty(PropertyName = "total_lines")]
        public int? TotalLines { get; set; }

        [JsonProperty(PropertyName = "processed_lines")]
        public int? ProcessedLines { get; set; }

        [JsonProperty(PropertyName = "update_lines")]
        public int? UpdateLines { get; set; }

        [JsonProperty(PropertyName = "error_lines")]
        public int? ErrorLines { get; set; }

    }
}
