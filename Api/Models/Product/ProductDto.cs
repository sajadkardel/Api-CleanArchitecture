using WebFramework.Api;

namespace Api.Models.Product
{
    public class ProductDto : BaseDto<ProductDto, Entities.Product.Product, int>
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
