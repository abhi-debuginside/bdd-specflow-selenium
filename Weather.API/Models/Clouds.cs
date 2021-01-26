using Newtonsoft.Json;

namespace Weather.API.Models
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }


}
