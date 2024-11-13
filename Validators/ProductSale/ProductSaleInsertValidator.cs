using DTOs.ProductSale;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ProductOrder
{
    public class ProductSaleInsertValidator : AbstractValidator<ProductSaleInsertDTO>
    {
        public ProductSaleInsertValidator() 
        {
            RuleFor(p => p.SaleId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.SaleId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.ProductId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
            RuleFor(p => p.ProductQuantity).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.ProductQuantity).GreaterThan(0).WithMessage("The {PropertyName} must be greater than 0.");
            RuleFor(p => p.PricePerUnit).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.PricePerUnit).GreaterThanOrEqualTo(1000m).WithMessage("The {PropertyName} must be greater or equal to 1000.");
        }
    }
}
