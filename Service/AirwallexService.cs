using CakeCapitalCheckout.Models.Airwallex;
using CakeCapitalCheckout.Models.Xano;
using RestSharp;
using Sentry;

namespace CakeCapitalCheckout.Service
{
    public interface IAirwallexService
    {
        Task<AirwallexPaymentIntent> CreateIntentAsync(PaymentSession session, string countryCode);
    }

    public class AirwallexConfig
    {
        public string ApiKey { get; set; }
        public string ClientId { get; set; }
        public string GetTokenUrl { get; set; }
        public string CreateIntentUrl { get; set; }

    }

    public class AirwallexService: IAirwallexService
    {
        private readonly IConfiguration _configuration;

        public AirwallexService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AirwallexPaymentIntent> CreateIntentAsync(PaymentSession session, string countryCode)
        {
            try
            {
                var airwallexConfig = _configuration.GetSection("Airwallex").Get<AirwallexConfig>() ?? throw new Exception("Configuration Error.");
                var token = await GetToken(airwallexConfig);

                AirwallexCreateIntentRequestContract requestContract = new(session.Amount,
                                                                            countryCode.ToUpper(),
                                                                            session.OrderId,
                                                                            Guid.NewGuid(),
                                                                            session.SuccessUrl);

                var client = new RestClient(airwallexConfig.CreateIntentUrl);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {token.Token}");
                request.AddJsonBody(requestContract);
                return await client.PostAsync<AirwallexPaymentIntent>(request);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                throw ex;
            }

        }

        private async Task<AirwallexToken> GetToken(AirwallexConfig config)
        {
            try
            {
                var client = new RestClient(config.GetTokenUrl);
                var request = new RestRequest("", Method.Post);
                request.AddHeader("x-api-key", config.ApiKey);
                request.AddHeader("x-client-id", config.ClientId);
                return await client.PostAsync<AirwallexToken>(request);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                throw ex;
            }
        }
    }
}
