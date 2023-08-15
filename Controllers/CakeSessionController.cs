using CakeCapitalCheckout.Models.Xano;
using CakeCapitalCheckout.Service;
using Microsoft.AspNetCore.Mvc;

namespace CakeCapitalCheckout.Controllers
{
    public class CakeSessionController : Controller
    {
        private readonly IXanoService _xanoService;
        private readonly IConfiguration _configuration;

        public CakeSessionController(IXanoService xanoService, IConfiguration configuration)
        {
            _xanoService = xanoService;
            _configuration = configuration;
        }

        [Route("/cakesession/createSession")]
        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] CreatePaymentSessionRequestContract requestContract)
        {
            try
            {
                var validationError = ValidateRequest(requestContract);
                if (validationError != null)
                    return BadRequest(validationError);

                var result = await _xanoService.CreatePaymentSessionAsync(requestContract);

                var host = _configuration.GetValue<string>("HostUrl");

                return Ok($"{host}/{result}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static string? ValidateRequest(CreatePaymentSessionRequestContract requestContract)
        {
            if (requestContract == null)
                return "Request cannot be null";

            if (string.IsNullOrEmpty(requestContract.MerchantToken))
                return "Merchant Token is required. You can get this token from your Cake Capital Portal.";

            if (requestContract.Amount <= 0)
                return "Amount should be greater than 0.";

            if (string.IsNullOrEmpty(requestContract.OrderId) || !requestContract.OrderId.Any())
                return "Order Id is required.";

            if (!IsValidUrl(requestContract.SuccessUrl))
                return "Success Url must be a valid http or https url";

            if (!IsValidUrl(requestContract.CancelUrl))
                return "Cancel Url must be a valid http or https url";

            return null;
        }

        private static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
