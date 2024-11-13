using DTOs.PaymentMethod;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.PaymentMethod
{
    public class PaymentMethodUpdateValidator : AbstractValidator<PaymentMethodUpdateDTO>
    {
        public PaymentMethodUpdateValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
        }
    }
}
