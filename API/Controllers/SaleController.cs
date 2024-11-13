using DTOs.Sale;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("Fictice_E-Commerce/SaleAPI/[controller]")]
    [ApiController]
    public class SaleController(
        ISaleService saleService,
        IOrderNumberService orderNumberService,
        ITransactionNumberService transactionNumberService,
        IValidator<SaleInsertDTO> saleInsertValidator
        ) : ControllerBase
    {
        private readonly ISaleService _saleService = saleService;
        private readonly IOrderNumberService _orderNumberService = orderNumberService;
        private readonly ITransactionNumberService _transactionNumberService = transactionNumberService;
        private readonly IValidator<SaleInsertDTO> _saleInsertValidator = saleInsertValidator;


        [HttpGet]
        public async Task<IEnumerable<SaleDTO>> Get() => await _saleService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetById(int id)
        {
            var result = await _saleService.GetById(id);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("OrderNumber/{orderNumber}")]
        public async Task<ActionResult<SaleDTO>> GetByOrderNumber(string orderNumber)
        {
            var result = await _saleService.GetByOrderNumber(orderNumber);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("{paymentMethodId}")]
        public async Task<ActionResult<SaleDTO>> Add(SaleCompleteRequestDTO saleCompleteRequestDTO, int paymentMethodId)
        {
            var saleInsertDTO = saleCompleteRequestDTO.saleInsertDTO;
            var productList = saleCompleteRequestDTO.Products;

            saleInsertDTO.OrderNumber = _orderNumberService.GenerateOrderNumber();

            // Validating the insert DTO
            var validationResult = await _saleInsertValidator.ValidateAsync(saleInsertDTO);

            // Checking if the validation is ok
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            // Returns SaleDTO object or String
            var saleResult = await _saleService.Add(saleInsertDTO, productList, paymentMethodId);

            // Returning the object if the sale creation is OK
            if (saleResult is SaleDTO saleDTO)
            {
                return CreatedAtAction(nameof(GetById), new { saleDTO.Id }, saleDTO);
            }

            // Returning the errors if an error ocurred during the sale creation
            return BadRequest(saleResult);
        }
    }
}
