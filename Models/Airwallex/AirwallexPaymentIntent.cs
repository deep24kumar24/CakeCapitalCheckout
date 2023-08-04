using System.Text.Json.Serialization;

namespace CakeCapitalCheckout.Models.Airwallex
{
    public class AirwallexPaymentIntent
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("available_payment_method_types")]
        public List<string> AvailablePaymentMethodTypes { get; set; }
    }
}
