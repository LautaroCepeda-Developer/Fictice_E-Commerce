using DTOs.BannedUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.BannedUser
{
    public class BannedUserUpdateValidator : AbstractValidator<BannedUserUpdateDTO>
    {
        public BannedUserUpdateValidator()
        {
            RuleFor(x => x.BanMotive).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.BanDateTimeEnd).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.BanDateTimeEnd).GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(1)).WithMessage("The {PropertyName} must be greater or equal to one day.");
        }
    }
}
