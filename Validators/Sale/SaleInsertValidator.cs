using DTOs.Sale;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Order
{
    public class SaleInsertValidator : AbstractValidator<SaleInsertDTO>
    {
        public SaleInsertValidator()
        {
            RuleFor(o => o.SaleValue).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(o => o.SaleValue).GreaterThanOrEqualTo(1000m).WithMessage("The {PropertyName} must be valid.");
            RuleFor(o => o.OrderNumber).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(o => o.BuyerId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(o => o.BuyerId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
        }
    }
}
