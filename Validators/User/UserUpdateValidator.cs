using DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.User
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Nickname).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Nickname).Length(3, 24).WithMessage("The {PropertyName} must have between 3 and 24 characters.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("The {PropertyName} is invalid.");
        }
    }
}
