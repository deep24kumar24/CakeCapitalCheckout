using CakeCapitalCheckout.Models.Airwallex;
using CakeCapitalCheckout.Models.Xano;

namespace CakeCapitalCheckout.ViewModels
{
    public class CheckoutViewModel
    {
        public AirwallexPaymentIntent PaymentIntent { get; set; }

        public PaymentSession PaymentSession { get; set; }
    }
}
