using DTOs.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/SaleAPI/[controller]")]
    [ApiController]
    public class PaymentController (IPaymentService paymentService): ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;

        [HttpGet]
        public async Task<IEnumerable<PaymentDTO>> Get() => await _paymentService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetById(int id)
        {
            var result = await _paymentService.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("TransactionNumber/{transactionNumber}")]
        public async Task<ActionResult<PaymentDTO>> GetByTransactionNumber(string transactionNumber)
        {
            var result = await _paymentService.GetByTransactionNumber(transactionNumber);

            return result is null ? NotFound() : Ok(result);
        }
    }
}
