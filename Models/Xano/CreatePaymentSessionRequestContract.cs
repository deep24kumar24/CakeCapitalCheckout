using Newtonsoft.Json;

namespace CakeCapitalCheckout.Models.Xano
{
    public class CreatePaymentSessionRequestContract
    {
        [JsonProperty("merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("cancel_url")]
        public string CancelUrl { get; set; }

        [JsonProperty("success_url")]
        public string SuccessUrl { get; set; }

    }
}
