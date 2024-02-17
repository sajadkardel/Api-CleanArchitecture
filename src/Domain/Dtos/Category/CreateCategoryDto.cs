using FluentValidation;

namespace Domain.Dtos.Category;

public class CreateCategoryDto
{
    public string Name { get; set; }
}

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(customer => customer.Name).NotNull();
    }
}
