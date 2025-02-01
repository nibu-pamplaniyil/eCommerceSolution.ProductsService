

using eCommerce.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.BusinessLogicLayer.Validators;
public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(x=>x.ProductName).NotEmpty().WithMessage("Product Name can't be blank");

        RuleFor(x => x.Category).IsInEnum().WithMessage("Provided Categoty is not in the list");

        RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Unit proce can't be blank");

        RuleFor(x=>x.QuantityInStock).InclusiveBetween(0,int.MaxValue).WithMessage($"Quantity in stock in between 0 to {int.MaxValue}");
    }
}
