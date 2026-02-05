using ECommerce.Core.DTOs;
using FluentValidation;

namespace ECommerce.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            // Name rules
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required") // Null olamaz
                .NotEmpty().WithMessage("{PropertyName} cannot be empty"); // Boş olamaz

            // Price rules
            RuleFor(x => x.Price)
                .InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater than 0");

            // Stock rules
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");

            // Category rules
            RuleFor(x => x.CategoryId)
                .InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} is required");
        }
    }
}