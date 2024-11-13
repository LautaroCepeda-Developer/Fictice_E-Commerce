using DTOs.Payment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Payment
{
    public class PaymentInsertValidator : AbstractValidator<PaymentInsertDTO>
    {
        public PaymentInsertValidator()
        {
            RuleFor(p => p.TransactionValue).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.TransactionValue).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
            RuleFor(p => p.Tax).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.Tax).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
            RuleFor(p => p.TransactionNumber).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.SellerBankAccount).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            // Using CreditCard validation because it's a project for learning purposes
            RuleFor(p => p.SellerBankAccount).CreditCard().WithMessage("The {PropertyName} must be valid.");
            RuleFor(p => p.PaymentMethodId).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(p => p.PaymentMethodId).GreaterThan(0).WithMessage("The {PropertyName} must be valid.");
        }
    }
}
