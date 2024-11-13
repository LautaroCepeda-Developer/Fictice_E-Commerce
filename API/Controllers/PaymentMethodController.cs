using DTOs.PaymentMethod;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/SaleAPI/[controller]")]
    [ApiController]
    public class PaymentMethodController(
        [FromKeyedServices("paymentMethodService")]ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO> paymentMethodService,
        IValidator<PaymentMethodInsertDTO> paymentMethodInsertValidator, IValidator<PaymentMethodUpdateDTO> paymentMethodUpdateValidator
        ) : ControllerBase
    {
        private readonly ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO> _paymentMethodServcie = paymentMethodService;
        private readonly IValidator<PaymentMethodInsertDTO> _paymentMethodInsertValidator = paymentMethodInsertValidator;
        private readonly IValidator<PaymentMethodUpdateDTO> _paymentMethodUpdateValidator = paymentMethodUpdateValidator;

        [HttpGet]
        public async Task<IEnumerable<PaymentMethodDTO>> Get() => await _paymentMethodServcie.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDTO>> GetById(int id)
        {
            var result = await _paymentMethodServcie.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentMethodDTO>> Add(PaymentMethodInsertDTO paymentMethodInsertDTO)
        {
            var validationResult = await _paymentMethodInsertValidator.ValidateAsync(paymentMethodInsertDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_paymentMethodServcie.Validate(paymentMethodInsertDTO)) return BadRequest(_paymentMethodServcie.Errors);

            var paymentMethodDTO = await _paymentMethodServcie.Add(paymentMethodInsertDTO);

            return CreatedAtAction(nameof(GetById), new { id = paymentMethodDTO.Id }, paymentMethodDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentMethodDTO>> Update(int id, PaymentMethodUpdateDTO paymentMethodUpdateDTO)
        {
            paymentMethodUpdateDTO.Id = id;

            var validationResult = await _paymentMethodUpdateValidator.ValidateAsync(paymentMethodUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            if (!_paymentMethodServcie.Validate(paymentMethodUpdateDTO)) return BadRequest(_paymentMethodServcie.Errors);

            var paymentMethodDTO = await _paymentMethodServcie.Update(id, paymentMethodUpdateDTO);

            return paymentMethodDTO is null ? NotFound() : Ok(paymentMethodDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentMethodDTO>> Delete(int id)
        {
            var result = await _paymentMethodServcie.Delete(id);

            return result is null ? NotFound() : Ok(result);
        }
    }
}
