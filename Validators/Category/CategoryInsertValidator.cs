using DTOs.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Category
{
    public class CategoryInsertValidator : AbstractValidator<CategoryInsertDTO>
    {
        public CategoryInsertValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("The {PropertyName} couldn't be empty.");
        }
    }
}
