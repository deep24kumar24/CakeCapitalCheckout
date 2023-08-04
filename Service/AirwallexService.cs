using CakeCapitalCheckout.Models.Airwallex;
using RestSharp;
using Sentry;

namespace CakeCapitalCheckout.Service
{
    public interface IAirwallexService
    {
        Task<AirwallexPaymentIntent> CreateIntentAsync(decimal amount, string countryCode, string returnUrl);
    }

    public class AirwallexService: IAirwallexService
    {
        private readonly string API_KEY = "17fbc7b14e99dc74a3f1070c83b06a3c922cc687bbbf1f6ed77033cf6b3b333b0fe56812517201dbafd3af108a3188f8";
        private readonly string CLIENT_ID = "fTJn6LvCQgGFham22cOmzA";

        private readonly string TOKEN_URI = "https://api-demo.airwallex.com/api/v1/authentication/login";
        private readonly string CREATE_INTENT_URI = "https://api-demo.airwallex.com/api/v1/pa/payment_intents/create";

        public async Task<AirwallexPaymentIntent> CreateIntentAsync(decimal amount, string countryCode, string returnUrl)
        {
            try
            {
                var token = await GetToken();

                AirwallexCreateIntentRequestContract requestContract = new(amount,
                                                                            countryCode.ToUpper(),
                                                                            Guid.NewGuid().ToString(),
                                                                            Guid.NewGuid(),
                                                                            returnUrl);

                var client = new RestClient(CREATE_INTENT_URI);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {token.Token}");
                request.AddJsonBody(requestContract);
                return await client.PostAsync<AirwallexPaymentIntent>(request);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return null;
            }

        }

        private async Task<AirwallexToken> GetToken()
        {
            var client = new RestClient(TOKEN_URI);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", API_KEY);
            request.AddHeader("x-client-id", CLIENT_ID);
            return await client.PostAsync<AirwallexToken>(request);
        }
    }
}
