using Api.Models.Product;
using AutoMapper;
using Data.Contracts;
using Entities.Product;
using WebFramework.Api;

namespace Api.Controllers.v1
{
    public class ProductController : CrudController<ProductDto, ProductSelectDto, Product, int>
    {
        public ProductController(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
