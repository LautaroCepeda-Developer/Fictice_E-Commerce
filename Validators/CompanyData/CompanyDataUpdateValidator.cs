using DTOs.CompanyData;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.CompanyData
{
    public class CompanyDataUpdateValidator : AbstractValidator<CompanyDataUpdateDTO>
    {
        public CompanyDataUpdateValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(c => c.EmpressBankAccount).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            // Using CreditCard validation because it's a project for learning purposes
            RuleFor(c => c.EmpressBankAccount).CreditCard().WithMessage("The {PropertyName} must be valid.");
            RuleFor(c => c.CompanyLegalAddres).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(c => c.Tax).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(c => c.Tax).GreaterThanOrEqualTo(3.0m).WithMessage("The {PropertyName} must be greater or equal to 3.0%");
        }
    }
}
