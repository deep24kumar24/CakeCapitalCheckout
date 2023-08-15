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

        [JsonProperty("cancel_url")]
        public string CancelUrl { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("success_url")]
        public string SuccessUrl { get; set; }

        [JsonProperty("completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty("countries")]
        public List<Country> Countries { get; set; }

        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; } 
    }

    public enum PaymentSessionStatus {
        Created,
        Expired,
        Completed
    }
}
