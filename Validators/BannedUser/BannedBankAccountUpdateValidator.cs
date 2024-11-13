using DTOs.BannedUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.BannedUser
{
    public class BannedBankAccountUpdateValidator : AbstractValidator<BannedBankAccountUpdateDTO>
    {
        public BannedBankAccountUpdateValidator()
        {
            RuleFor(b => b.BanMotive).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(b => b.BanDateTimeEnd).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(b => b.BanDateTimeEnd).GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(1)).WithMessage("The {PropertyName} must be greater or equal to one day.");
        }
    }
}
