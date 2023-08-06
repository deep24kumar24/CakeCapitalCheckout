using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CakeCapitalCheckout.Models.Xano
{
    public class CreatePaymentSessionRequestContract
    {
        [JsonProperty("merchant_token")]
        [JsonPropertyName("merchant_token")]
        public string MerchantToken { get; set; }

        [JsonProperty("amount")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("cancel_url")]
        [JsonPropertyName("cancel_url")]
        public string CancelUrl { get; set; }

        [JsonProperty("success_url")]
        [JsonPropertyName("success_url")]
        public string SuccessUrl { get; set; }

        [JsonProperty("order_id")]
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

    }
}
