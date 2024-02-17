using Domain.Dtos.Category;
using FluentValidation;

namespace Domain.Dtos.Product;

public class UpdateCategoryDto
{
    public string Name { get; set; }
}

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(customer => customer.Name).NotNull();
    }
}
