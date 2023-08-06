using CakeCapitalCheckout.Models.Xano;
using CakeCapitalCheckout.Service;
using Microsoft.AspNetCore.Mvc;

namespace CakeCapitalCheckout.Controllers
{
    public class CakeSessionController : Controller
    {
        private readonly IXanoService _xanoService;

        public CakeSessionController(IXanoService xanoService)
        {
            _xanoService = xanoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] CreatePaymentSessionRequestContract requestContract)
        {
            try
            {
                var validationError = ValidateRequest(requestContract);
                if (validationError != null)
                    return BadRequest(validationError);

                var result = await _xanoService.CreatePaymentSessionAsync(requestContract);
                return Ok(result);
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

            if (!isValidUrl(requestContract.SuccessUrl))
                return "Success Url must be a valid http or https url";

            if (!isValidUrl(requestContract.CancelUrl))
                return "Cancel Url must be a valid http or https url";

            return null;
        }

        private static bool isValidUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
