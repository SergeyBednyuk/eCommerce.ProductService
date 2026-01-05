using eCommerce.ProductService.BLL.DTOs;
using FluentValidation;

namespace eCommerce.ProductService.BLL.Validators;

public class GetProductsByIdsRequestValidator : AbstractValidator<GetProductsByIdsRequest>
{
    public GetProductsByIdsRequestValidator()
    {
        RuleFor(x => x.Ids)
            .NotNull().WithMessage("ids is required")
            .Must(ids => ids is not null && ids.Any()).WithMessage("The request must contain at leas one ID");
    }
}