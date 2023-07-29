using System.Text.Json.Serialization;

namespace CakeCapitalCheckout.Models
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

        public AirwallexCreateIntentRequestContract(decimal Amount, string Currency, string MerchantOrderId, Guid RequestId)
        {
            this.Amount = Amount;
            this.Currency = Currency;
            this.MerchantOrderId = MerchantOrderId;
            this.RequestId = RequestId;
        }
    }
}
