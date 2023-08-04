using Newtonsoft.Json;

namespace CakeCapitalCheckout.Models.Xano
{
    public class Merchant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("logo")]
        public Image Logo { get; set;}
        
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
