using Microsoft.AspNetCore.Mvc;

namespace CakeCapitalCheckout.Controllers
{
    public class PrivacyController: Controller
    {
        [Route("privacy")]
        [Route("privacy-policy")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
