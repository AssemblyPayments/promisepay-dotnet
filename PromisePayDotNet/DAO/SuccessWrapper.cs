using Newtonsoft.Json;

namespace PromisePayDotNet.DAO
{
    public class SuccessWrapper<T,name> where T:class
    {
        [JsonProperty(PropertyName = "123")]
        public T Data { get; set; }
    }
}
