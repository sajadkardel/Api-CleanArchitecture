using FluentValidation;

namespace Domain.Dtos.Product;

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