using Infrastructure.Api;

namespace Api.Models.Product
{
    public class CategoryDto : BaseDto<CategoryDto, Domain.Entities.Product.Category, int>
    {
        public string Name { get; set; }
    }
}
