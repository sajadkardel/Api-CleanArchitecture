using Infrastructure.Api;

namespace Api.Models.Product
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
