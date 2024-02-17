using FluentValidation;

namespace Domain.Dtos.Product;

public class CreateProductDto
{
    public string Name { get; set; }
}

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(customer => customer.Name).NotNull();
    }
}