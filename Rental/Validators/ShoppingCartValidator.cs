using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Rental.Models;

namespace Rental.Validators
{
    public class ShoppingCartValidator: AbstractValidator<IEnumerable<ShoppingCartItem>>
    {
        public ShoppingCartValidator()
        {
            RuleFor(x => x).NotNull()
                .WithMessage("Shopping cart can't be null");

            RuleFor(x => x).NotEmpty()
                .WithMessage("Shopping cart can't be empty");

            RuleFor(x => x)
                .Must(x => x.Any(y => y.Days > 0))
                .WithMessage("Days have to be grater than 0");
        }
    }
}
