using DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.User
{
    public class UserPersonalInformationUpdateValidator : AbstractValidator<UserPersonalInformationUpdateDTO>
    {
        public UserPersonalInformationUpdateValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.FullName).MaximumLength(42).WithMessage("The {PropertyName} can be up to a maximum of 45 characters.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Address).Length(3, 64).WithMessage("The {PropertyName} must have between 3 and 64 characters");
            RuleFor(x => x.NationalIdentification).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.NationalIdentification).MaximumLength(32).WithMessage("The {PropertyName} can be up to a maximum of 32 characters.");
        }

    }
}
