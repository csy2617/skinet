using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IConfiguration configuration) : ControllerBase
    {
        private readonly string _stripeSecretKey = configuration.GetValue<string>("Stripe:SecretKey");

    [HttpPost("create-payment-intent")]
    public ActionResult CreatePaymentIntent([FromBody] PaymentRequest request)
    {
        StripeConfiguration.ApiKey = _stripeSecretKey;

        var options = new PaymentIntentCreateOptions
        {
            Amount = request.Amount,
            Currency = "aud",
            PaymentMethodTypes = new List<string> { "card" },
        };

        var service = new PaymentIntentService();
        PaymentIntent intent = service.Create(options);

        return Ok(new { clientSecret = intent.ClientSecret });
    }
    }

    public class PaymentRequest
{
    public int Amount { get; set; }
}
}
