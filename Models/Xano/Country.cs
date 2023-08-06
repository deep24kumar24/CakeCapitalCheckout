using Newtonsoft.Json;

namespace CakeCapitalCheckout.Models.Xano
{
    public class Country
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

    }
}
