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
                //var token = await GetToken();

                //AirwallexCreateIntentRequestContract requestContract = new(500.0m, countryCode.ToUpper(), Guid.NewGuid().ToString(), Guid.NewGuid());

                //var client = new RestClient("https://api-demo.airwallex.com/api/v1/pa/payment_intents/create");
                //var request = new RestRequest("", Method.Post);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Authorization", $"Bearer {token.Token}");
                //request.AddJsonBody(requestContract);
                //var response = await client.PostAsync<AirwallexPaymentIntent>(request);

                var model = new AirwallexPaymentIntent()
                {
                    Id = "int_hkdmhxg82gn9oib5fgf",
                    Amount = 500.0m,
                    ClientSecret = "eyJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE2OTA2MDM0NjEsImV4cCI6MTY5MDYwNzA2MSwidHlwZSI6ImNsaWVudC1zZWNyZXQiLCJwYWRjIjoiSEsiLCJhY2NvdW50X2lkIjoiZjNiMGFhNzYtY2ZjMS00YTM5LThmNGMtZWMyMGFmM2ExM2JmIiwiaW50ZW50X2lkIjoiaW50X2hrZG1oeGc4MmduOW9pYjVmZ2YiLCJidXNpbmVzc19uYW1lIjoiQ2FrZSBDYXBpdGFsIFBBIFRlc3QifQ.M4L_66pskzJDhBpNwMuGQrIWX8X3GPrR9VfgHnfdsms",
                    Currency = "CAD",
                    AvailablePaymentMethodTypes = new List<string> { "applepay", "googlepay", "card" }
                };


                return PartialView("_PaymentView", model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        private static async Task<AirwallexToken> GetToken()
        {
            var client = new RestClient("https://api-demo.airwallex.com/api/v1/authentication/login");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("x-api-key", "17fbc7b14e99dc74a3f1070c83b06a3c922cc687bbbf1f6ed77033cf6b3b333b0fe56812517201dbafd3af108a3188f8");
            request.AddHeader("x-client-id", "fTJn6LvCQgGFham22cOmzA");
            return await client.PostAsync<AirwallexToken>(request);
        }
    }
}