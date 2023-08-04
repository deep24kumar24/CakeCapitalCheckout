using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace CakeCapitalCheckout.Models.Xano
{
    public class PaymentSession
    {
        [JsonProperty("session_id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        public PaymentSessionStatus Status { get; set; }

        [JsonProperty ("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty("cancel_url")]
        public string CancelUrl { get; set; }

        [JsonProperty("success_url")]
        public string SuccessUrl { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAtTimestamp { get; set; }
    }

    public enum PaymentSessionStatus {
        Created,
        Expired,
        Completed
    }
}
