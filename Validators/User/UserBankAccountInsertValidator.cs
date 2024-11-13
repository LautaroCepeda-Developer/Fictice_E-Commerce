using DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.User
{
    public class UserBankAccountInsertValidator : AbstractValidator<UserBankAccountInsertDTO>
    {
        public UserBankAccountInsertValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(u => u.Name).Length(3, 24).WithMessage("The {PropertyName} must have between 3 and 24 characters.");
            RuleFor(u => u.AccountNumber).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            // Using CreditCard validation because it's a project for learning purposes
            RuleFor(u => u.AccountNumber).CreditCard().WithMessage("The {PropertyName} must be valid.");
            RuleFor(u => u.UserId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(u => u.UserId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
        }
    }
}
