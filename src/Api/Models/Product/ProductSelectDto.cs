using Infrastructure.Api;

namespace Api.Models.Product
{
    public class ProductSelectDto
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }
    }
}
