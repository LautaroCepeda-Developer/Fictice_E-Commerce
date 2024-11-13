using DTOs.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Product
{
    public class ProductInsertValidator : AbstractValidator<ProductInsertDTO>
    {
        public ProductInsertValidator() 
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.Name).Length(12,96).WithMessage("The {PropertyName} length must be between 12 and 96 characters.");
            RuleFor(p => p.Price).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(1000m).WithMessage("The {PropertyName} must be greater or equal to 1000.");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("The {PropertyName} must be greater than 0.");
            RuleFor(p => p.SellerId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.SellerId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.CategoryId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
        }
    }
}
