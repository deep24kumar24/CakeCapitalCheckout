using CakeCapitalCheckout.Models.Xano;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Sentry;

namespace CakeCapitalCheckout.Service
{
    public interface IXanoService
    {
        Task<RestResponse<PaymentSession>> GetPaymentSessionAsync(string id);
        Task<string> CreatePaymentSessionAsync(CreatePaymentSessionRequestContract requestContract);
    }

    public class XanoService : IXanoService
    {
        private readonly RestClient restClient;
        private readonly IConfiguration _configuration;

        public XanoService(IConfiguration configuration) 
        {
            _configuration = configuration;
            var apiUrl = _configuration.GetValue<string>("Xano:ApiUrl");
            
            if (apiUrl != null)
            {
                restClient = new RestClient(apiUrl, configureSerialization: s => s.UseNewtonsoftJson());
            }
            else
            {
                throw new Exception("Xano Api Url is not configured properly");
            }
        }

        public async Task<RestResponse<PaymentSession>> GetPaymentSessionAsync(string paymentId)
        {
            try
            {
                var DATA_SOURCE_HEADER = _configuration.GetValue<string>("Xano:DataSourceHeader");

                var request = new RestRequest($"/payment/session/id?id={paymentId}", Method.Get);
                request.AddHeader("X-Data-Source", DATA_SOURCE_HEADER!);
                var response = await restClient.ExecuteGetAsync<PaymentSession>(request);
                return response;
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                throw ex;
            }
        }

        public async Task<string> CreatePaymentSessionAsync(CreatePaymentSessionRequestContract requestContract)
        {
            try
            {
                var DATA_SOURCE_HEADER = _configuration.GetValue<string>("Xano:DataSourceHeader");

                var request = new RestRequest($"/payment/session", Method.Post);
                request.AddHeader("X-Data-Source", DATA_SOURCE_HEADER!);
                request.AddJsonBody(requestContract);

                var response = await restClient.ExecutePostAsync<CreatePaymentSessionResponseContract>(request);

                if (response != null && response.Data != null && response.Data.Status == 201)
                    return response.Data.Message;
                else
                    throw new Exception("XanoService: Something Went wroong.");
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
                throw ex;
            }
        }
    }
}
