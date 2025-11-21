using eCommerce.ProductService.BLL.DTOs;
using FluentValidation;

namespace eCommerce.ProductService.BLL.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Category is required");
        
        RuleFor(x => x.UnitPrice)
            .InclusiveBetween(0, Decimal.MaxValue).WithMessage("Unit price is required");
        
        RuleFor(x=>x.QuantityInStock)
            .InclusiveBetween(0, Int32.MaxValue).WithMessage("Quantity is required");
    }
}