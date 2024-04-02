using FluentValidation;

namespace Application.Dtos.Product;

public class UpdateProductDto
{
    public string Name { get; set; }
}

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(customer => customer.Name).NotNull();
    }
}