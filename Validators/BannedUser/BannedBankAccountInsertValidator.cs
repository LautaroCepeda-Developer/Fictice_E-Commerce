using DTOs.BannedUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.BannedUser
{
    public class BannedBankAccountInsertValidator : AbstractValidator<BannedBankAccountInsertDTO>
    {
        public BannedBankAccountInsertValidator()
        {
            RuleFor(b => b.BanMotive).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(b => b.AccountNumber).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            // Using CreditCard validation because it's a project for learning purposes
            RuleFor(b => b.AccountNumber).CreditCard().WithMessage("The {PropertyName} must be valid.");
            RuleFor(b => b.BanDateTimeEnd).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(b => b.BanDateTimeEnd).GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(7)).WithMessage("The {PropertyName} must be greater or equal to one week.");       
        }
    }
}
