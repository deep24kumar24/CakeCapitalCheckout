using CakeCapitalCheckout.Service;
using CakeCapitalCheckout.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace CakeCapitalCheckout.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IAirwallexService _airwallexService;
        private readonly IXanoService _xanoService;
        private readonly string SESSION_KEY = "SessionId";

        public CheckoutController(IAirwallexService airwallexService, IXanoService xanoService) 
        {
            _airwallexService = airwallexService;
            _xanoService = xanoService;
        }

        [Route("/")]
        new public IActionResult NotFound() 
        {
            return View();
        }

        [Route("/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            try
            {
                var paymentResult = await _xanoService.GetPaymentSessionAsync(id);

                if (!paymentResult.IsSuccessStatusCode)
                {
                    throw new Exception("Xano Payment Session Failed with message:" + paymentResult.ErrorException.Message);
                }

                var paymentSession = paymentResult.Data;

                if (paymentResult.IsSuccessful && paymentSession != null && paymentSession.Status == Models.Xano.PaymentSessionStatus.Created)
                {
                    var model = new CheckoutViewModel()
                    {
                        PaymentSession = paymentResult.Data,
                    };

                    HttpContext.Session.SetString(SESSION_KEY, id);
                    return View(model);
                }

                return View("NotFound");
            }
            catch(Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return View("NotFound");
            }
        }

        [HttpGet("payment-view")]
        public async Task<IActionResult> GetPaymentView(string countryCode)
        {
            try
            {
                var sessionId = HttpContext.Session.GetString(SESSION_KEY);

                if(!string.IsNullOrEmpty(sessionId))
                {
                    var paymentSessionResult = await _xanoService.GetPaymentSessionAsync(sessionId);

                    if (paymentSessionResult.IsSuccessful && paymentSessionResult.Data != null)
                    {
                        var paymentSession = paymentSessionResult.Data;

                        var response = await _airwallexService.CreateIntentAsync(paymentSession, countryCode);

                        var model = new CheckoutViewModel()
                        {
                            PaymentIntent = response,
                            PaymentSession = paymentSession
                        };

                        return PartialView("_PaymentView", model);
                    }
                }
                
                return View("NotFound");
            }
            catch(Exception ex)
            {
                SentrySdk.CaptureException(ex);
                return View("NotFound");
            }
        }
    }
}