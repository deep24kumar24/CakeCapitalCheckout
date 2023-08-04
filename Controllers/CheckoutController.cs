using CakeCapitalCheckout.Service;
using CakeCapitalCheckout.ViewModels;
using Microsoft.AspNetCore.Mvc;

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


        [Route("/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var paymentResult =  await _xanoService.GetPaymentSessionAsync(id);
            var paymentSession = paymentResult.Data;

            if(paymentResult.IsSuccessful && paymentSession != null && paymentSession.Status == Models.Xano.PaymentSessionStatus.Created)
            {
                var model = new CheckoutViewModel()
                {
                    PaymentSession = paymentResult.Data,
                };

                var merchantResult = await _xanoService.GetMerchantAsync(model.PaymentSession.MerchantId);
                
                if(merchantResult.IsSuccessful && merchantResult.Data != null)
                {
                    model.Merchant = merchantResult.Data;
                    HttpContext.Session.SetString(SESSION_KEY, id);

                    return View(model);
                }
            }

            return View("NotFound");
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

                        var response = await _airwallexService.CreateIntentAsync(paymentSession.Amount, countryCode, paymentSession.SuccessUrl);

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
                BadRequest(ex.Message);
                return View("NotFound");
            }
        }
    }
}