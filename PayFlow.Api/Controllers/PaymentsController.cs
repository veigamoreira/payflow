using Microsoft.AspNetCore.Mvc;
using PayFlow.Application.Services;

namespace PayFlow.API.Controllers;

[ApiController]
[Route("payments")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentRequest paymentRequest)
    {
        var result = await _paymentService.ExecuteAsync(paymentRequest);
        return Ok(result);
    }
}
