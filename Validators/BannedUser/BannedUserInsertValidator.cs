using DTOs.BannedUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.BannedUser
{
    public class BannedUserInsertValidator : AbstractValidator<BannedUserInsertDTO>
    {
        public BannedUserInsertValidator() 
        {
            RuleFor(x => x.Nickname).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Nickname).Length(3, 24).WithMessage("The {PropertyName} must have between 3 and 24 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("The {PropertyName} is invalid.");
            RuleFor(x => x.NationalIdentification).NotNull().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.BanMotive).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Infractions).GreaterThan(0).WithMessage("The user needs at least one infraction.");
            RuleFor(x => x.BanDateTimeEnd).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.BanDateTimeEnd).GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(7)).WithMessage("The {PropertyName} must be greater or equal to one week.");
        }
    }
}
