using DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.User
{
    public class UserInsertValidator : AbstractValidator<UserInsertDTO>
    {
        public UserInsertValidator()
        {
            RuleFor(x => x.Nickname).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Nickname).Length(3, 24).WithMessage("The {PropertyName} must have between 3 and 24 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("The {PropertyName} is invalid.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.FullName).MaximumLength(42).WithMessage("The {PropertyName} can be up to a maximum of 45 characters.");
            RuleFor(x => x.BirthDate).LessThan(DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-17)).WithMessage("The user is a minor.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Address).Length(3, 64).WithMessage("The {PropertyName} must have between 3 and 64 characters");
            RuleFor(x => x.NationalIdentification).NotNull().WithMessage("The {PropertyName} couldn't be empty.");
        }
    }
}
