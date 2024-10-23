using Newtonsoft.Json;

namespace TM.WebServer.Entities
{
    public class ResponseDTO<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrorText { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }
    }
}
