using CakeCapitalCheckout.Models.Airwallex;
using CakeCapitalCheckout.Models.Xano;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;
using Sentry;

namespace CakeCapitalCheckout.Service
{
    public interface IXanoService
    {
        Task<RestResponse<PaymentSession>> GetPaymentSessionAsync(string id);
        Task<RestResponse<Merchant>> GetMerchantAsync(int id);

        Task<RestResponse<string>> CreateSessionAsync(CreatePaymentSessionRequestContract requestContract);
    }

    public class XanoService : IXanoService
    {
        private readonly string XANO_API_URI = "https://x8ki-letl-twmt.n7.xano.io/api:AUehKOwf";
        private readonly RestClient restClient;

        public XanoService() 
        {
            restClient = new RestClient(XANO_API_URI, configureSerialization: s => s.UseNewtonsoftJson());
        }

        public async Task<RestResponse<PaymentSession>> GetPaymentSessionAsync(string paymentId)
        {
            try
            {
                var request = new RestRequest($"/session/{paymentId}", Method.Get);
                var response = await restClient.ExecuteGetAsync<PaymentSession>(request);
                return response;
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return null;
            }
        }

        public async Task<RestResponse<Merchant>> GetMerchantAsync(int merchantId)
        {
            try
            {
                var request = new RestRequest($"/merchant/{merchantId}", Method.Get);
                var response = await restClient.ExecuteGetAsync<Merchant>(request);
                return response;
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return null;
            }
        }

        public async Task<RestResponse<string>> CreateSessionAsync(CreatePaymentSessionRequestContract requestContract)
        {
            try
            {
                var request = new RestRequest($"/session", Method.Post);
                request.AddJsonBody(requestContract);
                var response = await restClient.ExecutePostAsync<string>(request);
                return response;
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return null;
            }
        }
    }
}
