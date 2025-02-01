

using eCommerce.BusinessLogicLayer.DTO;
using FluentValidation;
using FluentValidation.Validators;

namespace eCommerce.BusinessLogicLayer.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(x=>x.ProductID).NotEmpty().WithMessage("Product ID can't be blamk");

        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name can't be blank");

        RuleFor(x => x.Category).IsInEnum().WithMessage("Provided Categoty is not in the list");

        RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Unit proce can't be blank");

        RuleFor(x => x.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock in between 0 to {int.MaxValue}");
    }
}
