using busnisslogic.interfaces;
using domain_and_repo.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        [Authorize]
    public class PaymentController : ControllerBase
        {
            private readonly IPaymentProcessor _paymentProcessor;

            public PaymentController(IPaymentProcessor paymentProcessor)
            {
                _paymentProcessor = paymentProcessor;
            }

            
            [HttpPost]
            public async Task<IActionResult> ProcessPayment([FromBody] Paymentrequest paymentrequest)
            {
                if (paymentrequest == null || paymentrequest.Amount <= 0 || paymentrequest.StudentId <= 0)
                {
                    return BadRequest("Invalid payment request.");
                }

                try
                {
                    await _paymentProcessor.ProcessPayment(paymentrequest.StudentId, paymentrequest.Amount);
                    return Ok(" processed successfully");
                }
                catch (Exception ex)
                {
                    
                    return BadRequest(ex.Message);
                }
            }
        }

    }

