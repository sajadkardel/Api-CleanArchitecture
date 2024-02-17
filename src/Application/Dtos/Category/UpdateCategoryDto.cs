using FluentValidation;

namespace Application.Dtos.Category;

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
