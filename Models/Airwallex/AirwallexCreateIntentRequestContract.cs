using System.Text.Json.Serialization;

namespace CakeCapitalCheckout.Models.Airwallex
{
    public class AirwallexCreateIntentRequestContract
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; }

        [JsonPropertyName("merchant_order_id")]
        public string MerchantOrderId { get; init; }

        [JsonPropertyName("request_id")]
        public Guid RequestId { get; init; }

        [JsonPropertyName("return_url")]
        public string ReturnUrl { get; init; }

        public AirwallexCreateIntentRequestContract(decimal Amount, string Currency, string MerchantOrderId, Guid RequestId, string returnUrl)
        {
            this.Amount = Amount;
            this.Currency = Currency;
            this.MerchantOrderId = MerchantOrderId;
            this.RequestId = RequestId;
            ReturnUrl = returnUrl;
        }
    }
}
