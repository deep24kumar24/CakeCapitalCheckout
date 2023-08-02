using CakeCapitalCheckout.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CakeCapitalCheckout.Controllers
{
    public class CheckoutController : Controller
    {
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("payment-view")]
        public async Task<IActionResult> GetPaymentView(string countryCode)
        {
            try
            {
                var response = await CreateIntent(500.0m,countryCode);
                return PartialView("_PaymentView", response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static async Task<AirwallexToken> GetToken()
        {
            var apiKey = "17fbc7b14e99dc74a3f1070c83b06a3c922cc687bbbf1f6ed77033cf6b3b333b0fe56812517201dbafd3af108a3188f8";
            var clientId = "fTJn6LvCQgGFham22cOmzA";
            var client = new RestClient("https://api-demo.airwallex.com/api/v1/authentication/login");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", apiKey);
            request.AddHeader("x-client-id", clientId);
            return await client.PostAsync<AirwallexToken>(request);
        }

        private static async Task<AirwallexPaymentIntent> CreateIntent(decimal amount, string countryCode)
        {
            var token = await GetToken();

            AirwallexCreateIntentRequestContract requestContract = new(amount, countryCode.ToUpper(), Guid.NewGuid().ToString(), Guid.NewGuid(), "https://localhost:7103/");

            var client = new RestClient("https://api-demo.airwallex.com/api/v1/pa/payment_intents/create");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token.Token}");
            request.AddJsonBody(requestContract);
            return await client.PostAsync<AirwallexPaymentIntent>(request);
        }
    }
}