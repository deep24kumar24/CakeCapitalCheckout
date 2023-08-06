using Newtonsoft.Json;

namespace CakeCapitalCheckout.Models.Xano
{
    public class Merchant
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("legal_name")]
        public string LegalName { get; set; }

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
