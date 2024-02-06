using Infrastructure.Api;

namespace Api.Models.Product
{
    public class CategorySelectDto : BaseDto<CategorySelectDto, Domain.Entities.Product.Category, int>
    {
        public string Name { get; set; }
    }
}
